namespace Keycloak.AuthServices.Sdk.Utils;

using System.Buffers;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Primitives;
#pragma warning disable IDE0251 // Make member 'readonly'

/// <summary>
/// Allows constructing a query string.
/// </summary>
public class QueryBuilder : IEnumerable<KeyValuePair<string, string>>
{
    private readonly IList<KeyValuePair<string, string>> @params;

    /// <summary>
    /// Initializes a new instance of <see cref="QueryBuilder"/>.
    /// </summary>
    public QueryBuilder() => this.@params = new List<KeyValuePair<string, string>>();

    /// <summary>
    /// Initializes a new instance of <see cref="QueryBuilder"/>.
    /// </summary>
    /// <param name="parameters">The parameters to initialize the instance with.</param>
    public QueryBuilder(IEnumerable<KeyValuePair<string, string>> parameters) =>
        this.@params = new List<KeyValuePair<string, string>>(parameters);

    /// <summary>
    /// Initializes a new instance of <see cref="QueryBuilder"/>.
    /// </summary>
    /// <param name="parameters">The parameters to initialize the instance with.</param>
    public QueryBuilder(IEnumerable<KeyValuePair<string, StringValues>> parameters)
        : this(
            parameters.SelectMany(
                kvp => kvp.Value,
                (kvp, v) => KeyValuePair.Create(kvp.Key, v ?? string.Empty)
            )
        ) { }

    /// <summary>
    /// Adds a query string token to the instance.
    /// </summary>
    /// <param name="key">The query key.</param>
    /// <param name="value">The query value.</param>
    public QueryBuilder Add(string key, string value)
    {
        this.@params.Add(new KeyValuePair<string, string>(key, value));

        return this;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        using var builder = new ValueStringBuilder();
        var first = true;
        for (var i = 0; i < this.@params.Count; i++)
        {
            var pair = this.@params[i];
            builder.Append(first ? '?' : '&');
            first = false;
            builder.Append(UrlEncoder.Default.Encode(pair.Key));
            builder.Append('=');
            builder.Append(UrlEncoder.Default.Encode(pair.Value));
        }

        return builder.ToString()!;
    }

    /// <summary>
    /// Constructs a <see cref="QueryString"/> from this <see cref="QueryBuilder"/>.
    /// </summary>
    /// <returns>The <see cref="QueryString"/>.</returns>
    public QueryString ToQueryString() => new(this.ToString());

    /// <inheritdoc/>
    public override int GetHashCode() => this.ToQueryString().GetHashCode();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => this.ToQueryString().Equals(obj);

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() =>
        this.@params.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.@params.GetEnumerator();
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


/// <summary>
/// Provides correct handling for QueryString value when needed to reconstruct a request or redirect URI string
/// </summary>
public readonly struct QueryString : IEquatable<QueryString>
{
    /// <summary>
    /// Represents the empty query string. This field is read-only.
    /// </summary>
    public static readonly QueryString Empty = new(string.Empty);

    /// <summary>
    /// Initialize the query string with a given value. This value must be in escaped and delimited format with
    /// a leading '?' character.
    /// </summary>
    /// <param name="value">The query string to be assigned to the Value property.</param>
    public QueryString(string? value)
    {
        if (!string.IsNullOrEmpty(value) && value[0] != '?')
        {
            throw new ArgumentException(
                "The leading '?' must be included for a non-empty query.",
                nameof(value)
            );
        }
        this.Value = value;
    }

    /// <summary>
    /// The escaped query string with the leading '?' character
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// True if the query string is not empty
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value))]
    public bool HasValue => !string.IsNullOrEmpty(this.Value);

    /// <summary>
    /// Provides the query string escaped in a way which is correct for combining into the URI representation.
    /// A leading '?' character will be included unless the Value is null or empty. Characters which are potentially
    /// dangerous are escaped.
    /// </summary>
    /// <returns>The query string value</returns>
    public override string ToString() => this.ToUriComponent();

    /// <summary>
    /// Provides the query string escaped in a way which is correct for combining into the URI representation.
    /// A leading '?' character will be included unless the Value is null or empty. Characters which are potentially
    /// dangerous are escaped.
    /// </summary>
    /// <returns>The query string value</returns>
    public string ToUriComponent() =>
        // Escape things properly so System.Uri doesn't mis-interpret the data.
        this.HasValue
            ? this.Value.Replace("#", "%23")
            : string.Empty;

    /// <summary>
    /// Returns an QueryString given the query as it is escaped in the URI format. The string MUST NOT contain any
    /// value that is not a query.
    /// </summary>
    /// <param name="uriComponent">The escaped query as it appears in the URI format.</param>
    /// <returns>The resulting QueryString</returns>
    public static QueryString FromUriComponent(string uriComponent)
    {
        if (string.IsNullOrEmpty(uriComponent))
        {
            return new QueryString(string.Empty);
        }
        return new QueryString(uriComponent);
    }

    /// <summary>
    /// Returns an QueryString given the query as from a Uri object. Relative Uri objects are not supported.
    /// </summary>
    /// <param name="uri">The Uri object</param>
    /// <returns>The resulting QueryString</returns>
    public static QueryString FromUriComponent(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);

        var queryValue = uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped);
        if (!string.IsNullOrEmpty(queryValue))
        {
            queryValue = "?" + queryValue;
        }
        return new QueryString(queryValue);
    }

    /// <summary>
    /// Create a query string with a single given parameter name and value.
    /// </summary>
    /// <param name="name">The un-encoded parameter name</param>
    /// <param name="value">The un-encoded parameter value</param>
    /// <returns>The resulting QueryString</returns>
    public static QueryString Create(string name, string value)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (!string.IsNullOrEmpty(value))
        {
            value = UrlEncoder.Default.Encode(value);
        }
        return new QueryString($"?{UrlEncoder.Default.Encode(name)}={value}");
    }

    /// <summary>
    /// Creates a query string composed from the given name value pairs.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>The resulting QueryString</returns>
    public static QueryString Create(IEnumerable<KeyValuePair<string, string?>> parameters)
    {
        var builder = new StringBuilder();
        var first = true;
        foreach (var pair in parameters)
        {
            AppendKeyValuePair(builder, pair.Key, pair.Value, first);
            first = false;
        }

        return new QueryString(builder.ToString());
    }

    /// <summary>
    /// Creates a query string composed from the given name value pairs.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>The resulting QueryString</returns>
    public static QueryString Create(IEnumerable<KeyValuePair<string, StringValues>> parameters)
    {
        var builder = new StringBuilder();
        var first = true;

        foreach (var pair in parameters)
        {
            // If nothing in this pair.Values, append null value and continue
            if (StringValues.IsNullOrEmpty(pair.Value))
            {
                AppendKeyValuePair(builder, pair.Key, null, first);
                first = false;
                continue;
            }
            // Otherwise, loop through values in pair.Value
            foreach (var value in pair.Value)
            {
                AppendKeyValuePair(builder, pair.Key, value, first);
                first = false;
            }
        }

        return new QueryString(builder.ToString());
    }

    /// <summary>
    /// Concatenates <paramref name="other"/> to the current query string.
    /// </summary>
    /// <param name="other">The <see cref="QueryString"/> to concatenate.</param>
    /// <returns>The concatenated <see cref="QueryString"/>.</returns>
    public QueryString Add(QueryString other)
    {
        if (!this.HasValue || this.Value.Equals("?", StringComparison.Ordinal))
        {
            return other;
        }
        if (!other.HasValue || other.Value.Equals("?", StringComparison.Ordinal))
        {
            return this;
        }

        // ?name1=value1 Add ?name2=value2 returns ?name1=value1&name2=value2
        return new QueryString(string.Concat(this.Value, "&", other.Value.AsSpan(1)));
    }

    /// <summary>
    /// Concatenates a query string with <paramref name="name"/> and <paramref name="value"/>
    /// to the current query string.
    /// </summary>
    /// <param name="name">The name of the query string to concatenate.</param>
    /// <param name="value">The value of the query string to concatenate.</param>
    /// <returns>The concatenated <see cref="QueryString"/>.</returns>
    public QueryString Add(string name, string value)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (!this.HasValue || this.Value.Equals("?", StringComparison.Ordinal))
        {
            return Create(name, value);
        }

        var builder = new StringBuilder(this.Value);
        AppendKeyValuePair(builder, name, value, first: false);
        return new QueryString(builder.ToString());
    }

    /// <summary>
    /// Evalutes if the current query string is equal to <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The <see cref="QueryString"/> to compare.</param>
    /// <returns><see langword="true"/> if the query strings are equal.</returns>
    public bool Equals(QueryString other)
    {
        if (!this.HasValue && !other.HasValue)
        {
            return true;
        }
        return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Evaluates if the current query string is equal to an object <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">An object to compare.</param>
    /// <returns><see langword="true" /> if the query strings are equal.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return !this.HasValue;
        }
        return obj is QueryString @string && this.Equals(@string);
    }

    /// <summary>
    /// Gets a hash code for the value.
    /// </summary>
    /// <returns>The hash code as an <see cref="int"/>.</returns>
    public override int GetHashCode() => this.HasValue ? this.Value.GetHashCode() : 0;

    /// <summary>
    /// Evaluates if one query string is equal to another.
    /// </summary>
    /// <param name="left">A <see cref="QueryString"/> instance.</param>
    /// <param name="right">A <see cref="QueryString"/> instance.</param>
    /// <returns><see langword="true" /> if the query strings are equal.</returns>
    public static bool operator ==(QueryString left, QueryString right) => left.Equals(right);

    /// <summary>
    /// Evaluates if one query string is not equal to another.
    /// </summary>
    /// <param name="left">A <see cref="QueryString"/> instance.</param>
    /// <param name="right">A <see cref="QueryString"/> instance.</param>
    /// <returns><see langword="true" /> if the query strings are not equal.</returns>
    public static bool operator !=(QueryString left, QueryString right) => !left.Equals(right);

    /// <summary>
    /// Concatenates <paramref name="left"/> and <paramref name="right"/> into a single query string.
    /// </summary>
    /// <param name="left">A <see cref="QueryString"/> instance.</param>
    /// <param name="right">A <see cref="QueryString"/> instance.</param>
    /// <returns>The concatenated <see cref="QueryString"/>.</returns>
    public static QueryString operator +(QueryString left, QueryString right) => left.Add(right);

    private static void AppendKeyValuePair(
        StringBuilder builder,
        string key,
        string? value,
        bool first
    )
    {
        builder.Append(first ? '?' : '&');
        builder.Append(UrlEncoder.Default.Encode(key));
        builder.Append('=');
        if (!string.IsNullOrEmpty(value))
        {
            builder.Append(UrlEncoder.Default.Encode(value));
        }
    }
}

internal ref partial struct ValueStringBuilder
{
    private char[]? arrayToReturnToPool;
    private int position;

    public ValueStringBuilder(Span<char> initialBuffer)
    {
        this.arrayToReturnToPool = null;
        this.RawChars = initialBuffer;
        this.position = 0;
    }

    public ValueStringBuilder(int initialCapacity)
    {
        this.arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialCapacity);
        this.RawChars = this.arrayToReturnToPool;
        this.position = 0;
    }

    public int Length
    {
        get => this.position;
        set
        {
            Debug.Assert(value >= 0);
            Debug.Assert(value <= this.RawChars.Length);
            this.position = value;
        }
    }

    public int Capacity => this.RawChars.Length;

    public void EnsureCapacity(int capacity)
    {
        // This is not expected to be called this with negative capacity
        Debug.Assert(capacity >= 0);

        // If the caller has a bug and calls this with negative capacity, make sure to call Grow to throw an exception.
        if ((uint)capacity > (uint)this.RawChars.Length)
        {
            this.Grow(capacity - this.position);
        }
    }

    /// <summary>
    /// Get a pinnable reference to the builder.
    /// Does not ensure there is a null char after <see cref="Length"/>
    /// This overload is pattern matched in the C# 7.3+ compiler so you can omit
    /// the explicit method call, and write eg "fixed (char* c = builder)"
    /// </summary>
    public ref char GetPinnableReference() => ref MemoryMarshal.GetReference(this.RawChars);

    /// <summary>
    /// Get a pinnable reference to the builder.
    /// </summary>
    /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length"/></param>
    public ref char GetPinnableReference(bool terminate)
    {
        if (terminate)
        {
            this.EnsureCapacity(this.Length + 1);
            this.RawChars[this.Length] = '\0';
        }
        return ref MemoryMarshal.GetReference(this.RawChars);
    }

    public ref char this[int index]
    {
        get
        {
            Debug.Assert(index < this.position);
            return ref this.RawChars[index];
        }
    }

    public override string ToString()
    {
        var s = this.RawChars[..this.position].ToString();
        this.Dispose();
        return s;
    }

    /// <summary>Returns the underlying storage of the builder.</summary>
    public Span<char> RawChars { get; private set; }

    /// <summary>
    /// Returns a span around the contents of the builder.
    /// </summary>
    /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length"/></param>
    public ReadOnlySpan<char> AsSpan(bool terminate)
    {
        if (terminate)
        {
            this.EnsureCapacity(this.Length + 1);
            this.RawChars[this.Length] = '\0';
        }
        return this.RawChars[..this.position];
    }

    public ReadOnlySpan<char> AsSpan() => this.RawChars[..this.position];

    public ReadOnlySpan<char> AsSpan(int start) => this.RawChars[start..this.position];

    public ReadOnlySpan<char> AsSpan(int start, int length) => this.RawChars.Slice(start, length);

    public bool TryCopyTo(Span<char> destination, out int charsWritten)
    {
        if (this.RawChars[..this.position].TryCopyTo(destination))
        {
            charsWritten = this.position;
            this.Dispose();
            return true;
        }
        else
        {
            charsWritten = 0;
            this.Dispose();
            return false;
        }
    }

    public void Insert(int index, char value, int count)
    {
        if (this.position > this.RawChars.Length - count)
        {
            this.Grow(count);
        }

        var remaining = this.position - index;
        this.RawChars.Slice(index, remaining).CopyTo(this.RawChars[(index + count)..]);
        this.RawChars.Slice(index, count).Fill(value);
        this.position += count;
    }

    public void Insert(int index, string? s)
    {
        if (s == null)
        {
            return;
        }

        var count = s.Length;

        if (this.position > this.RawChars.Length - count)
        {
            this.Grow(count);
        }

        var remaining = this.position - index;
        this.RawChars.Slice(index, remaining).CopyTo(this.RawChars[(index + count)..]);
        s.AsSpan().CopyTo(this.RawChars[index..]);
        this.position += count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char c)
    {
        var pos = this.position;
        if ((uint)pos < (uint)this.RawChars.Length)
        {
            this.RawChars[pos] = c;
            this.position = pos + 1;
        }
        else
        {
            this.GrowAndAppend(c);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string? s)
    {
        if (s == null)
        {
            return;
        }

        var pos = this.position;
        if (s.Length == 1 && (uint)pos < (uint)this.RawChars.Length) // very common case, e.g. appending strings from NumberFormatInfo like separators, percent symbols, etc.
        {
            this.RawChars[pos] = s[0];
            this.position = pos + 1;
        }
        else
        {
            this.AppendSlow(s);
        }
    }

    private void AppendSlow(string s)
    {
        var pos = this.position;
        if (pos > this.RawChars.Length - s.Length)
        {
            this.Grow(s.Length);
        }

        s.AsSpan().CopyTo(this.RawChars[pos..]);
        this.position += s.Length;
    }

    public void Append(char c, int count)
    {
        if (this.position > this.RawChars.Length - count)
        {
            this.Grow(count);
        }

        var dst = this.RawChars.Slice(this.position, count);
        for (var i = 0; i < dst.Length; i++)
        {
            dst[i] = c;
        }
        this.position += count;
    }

    public void Append(ReadOnlySpan<char> value)
    {
        var pos = this.position;
        if (pos > this.RawChars.Length - value.Length)
        {
            this.Grow(value.Length);
        }

        value.CopyTo(this.RawChars[this.position..]);
        this.position += value.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<char> AppendSpan(int length)
    {
        var origPos = this.position;
        if (origPos > this.RawChars.Length - length)
        {
            this.Grow(length);
        }

        this.position = origPos + length;
        return this.RawChars.Slice(origPos, length);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void GrowAndAppend(char c)
    {
        this.Grow(1);
        this.Append(c);
    }

    /// <summary>
    /// Resize the internal buffer either by doubling current buffer size or
    /// by adding <paramref name="additionalCapacityBeyondPos"/> to
    /// <see cref="position"/> whichever is greater.
    /// </summary>
    /// <param name="additionalCapacityBeyondPos">
    /// Number of chars requested beyond current position.
    /// </param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int additionalCapacityBeyondPos)
    {
        Debug.Assert(additionalCapacityBeyondPos > 0);
        Debug.Assert(
            this.position > this.RawChars.Length - additionalCapacityBeyondPos,
            "Grow called incorrectly, no resize is needed."
        );

        // Make sure to let Rent throw an exception if the caller has a bug and the desired capacity is negative
        var poolArray = ArrayPool<char>.Shared.Rent(
            (int)
                Math.Max(
                    (uint)(this.position + additionalCapacityBeyondPos),
                    (uint)this.RawChars.Length * 2
                )
        );

        this.RawChars[..this.position].CopyTo(poolArray);

        var toReturn = this.arrayToReturnToPool;
        this.RawChars = this.arrayToReturnToPool = poolArray;
        if (toReturn != null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        var toReturn = this.arrayToReturnToPool;
        this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
        if (toReturn != null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }
}
#pragma warning restore IDE0251 // Make member 'readonly'
