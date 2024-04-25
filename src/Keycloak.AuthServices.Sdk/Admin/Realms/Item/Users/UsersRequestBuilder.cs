// <auto-generated/>
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Users.Count;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Users.Item;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Users.Profile;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Users {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\users
    /// </summary>
    public class UsersRequestBuilder : BaseRequestBuilder {
        /// <summary>The count property</summary>
        public CountRequestBuilder Count { get =>
            new CountRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The profile property</summary>
        public ProfileRequestBuilder Profile { get =>
            new ProfileRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the Keycloak.AuthServices.Sdk.Admin.admin.realms.item.users.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="UserItemRequestBuilder"/></returns>
        public UserItemRequestBuilder this[string position] { get {
            var urlTplParams = new Dictionary<string, object>(PathParameters);
            urlTplParams.Add("user%2Did", position);
            return new UserItemRequestBuilder(urlTplParams, RequestAdapter);
        } }
        /// <summary>
        /// Instantiates a new <see cref="UsersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UsersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users{?briefRepresentation*,email*,emailVerified*,enabled*,exact*,first*,firstName*,idpAlias*,idpUserId*,lastName*,max*,q*,search*,username*}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="UsersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UsersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users{?briefRepresentation*,email*,emailVerified*,enabled*,exact*,first*,firstName*,idpAlias*,idpUserId*,lastName*,max*,q*,search*,username*}", rawUrl) {
        }
        /// <summary>
        /// Get users Returns a stream of users, filtered according to query parameters.
        /// </summary>
        /// <returns>A List&lt;UserRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<UserRepresentation>?> GetAsync(Action<RequestConfiguration<UsersRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<List<UserRepresentation>> GetAsync(Action<RequestConfiguration<UsersRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<UserRepresentation>(requestInfo, UserRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <summary>
        /// Create a new user Username must be unique.
        /// </summary>
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Stream?> PostAsync(UserRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<Stream> PostAsync(UserRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get users Returns a stream of users, filtered according to query parameters.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<UsersRequestBuilderGetQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<UsersRequestBuilderGetQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Create a new user Username must be unique.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(UserRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(UserRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default) {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, "{+baseurl}/admin/realms/{realm}/users", PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="UsersRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public UsersRequestBuilder WithUrl(string rawUrl) {
            return new UsersRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get users Returns a stream of users, filtered according to query parameters.
        /// </summary>
        public class UsersRequestBuilderGetQueryParameters {
            /// <summary>Boolean which defines whether brief representations are returned (default: false)</summary>
            [QueryParameter("briefRepresentation")]
            public bool? BriefRepresentation { get; set; }
            /// <summary>A String contained in email, or the complete email, if param &quot;exact&quot; is true</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("email")]
            public string? Email { get; set; }
#nullable restore
#else
            [QueryParameter("email")]
            public string Email { get; set; }
#endif
            /// <summary>whether the email has been verified</summary>
            [QueryParameter("emailVerified")]
            public bool? EmailVerified { get; set; }
            /// <summary>Boolean representing if user is enabled or not</summary>
            [QueryParameter("enabled")]
            public bool? Enabled { get; set; }
            /// <summary>Boolean which defines whether the params &quot;last&quot;, &quot;first&quot;, &quot;email&quot; and &quot;username&quot; must match exactly</summary>
            [QueryParameter("exact")]
            public bool? Exact { get; set; }
            /// <summary>Pagination offset</summary>
            [QueryParameter("first")]
            public int? First { get; set; }
            /// <summary>A String contained in firstName, or the complete firstName, if param &quot;exact&quot; is true</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("firstName")]
            public string? FirstName { get; set; }
#nullable restore
#else
            [QueryParameter("firstName")]
            public string FirstName { get; set; }
#endif
            /// <summary>The alias of an Identity Provider linked to the user</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("idpAlias")]
            public string? IdpAlias { get; set; }
#nullable restore
#else
            [QueryParameter("idpAlias")]
            public string IdpAlias { get; set; }
#endif
            /// <summary>The userId at an Identity Provider linked to the user</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("idpUserId")]
            public string? IdpUserId { get; set; }
#nullable restore
#else
            [QueryParameter("idpUserId")]
            public string IdpUserId { get; set; }
#endif
            /// <summary>A String contained in lastName, or the complete lastName, if param &quot;exact&quot; is true</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("lastName")]
            public string? LastName { get; set; }
#nullable restore
#else
            [QueryParameter("lastName")]
            public string LastName { get; set; }
#endif
            /// <summary>Maximum results size (defaults to 100)</summary>
            [QueryParameter("max")]
            public int? Max { get; set; }
            /// <summary>A query to search for custom attributes, in the format &apos;key1:value2 key2:value2&apos;</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("q")]
            public string? Q { get; set; }
#nullable restore
#else
            [QueryParameter("q")]
            public string Q { get; set; }
#endif
            /// <summary>A String contained in username, first or last name, or email. Default search behavior is prefix-based (e.g., foo or foo*). Use *foo* for infix search and &quot;foo&quot; for exact search.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("search")]
            public string? Search { get; set; }
#nullable restore
#else
            [QueryParameter("search")]
            public string Search { get; set; }
#endif
            /// <summary>A String contained in username, or the complete username, if param &quot;exact&quot; is true</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("username")]
            public string? Username { get; set; }
#nullable restore
#else
            [QueryParameter("username")]
            public string Username { get; set; }
#endif
        }
    }
}
