// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\users\{user-id}\execute-actions-email
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ExecuteActionsEmailRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExecuteActionsEmailRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/execute-actions-email{?client_id*,lifespan*,redirect_uri*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExecuteActionsEmailRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/execute-actions-email{?client_id*,lifespan*,redirect_uri*}", rawUrl)
        {
        }
        /// <summary>
        /// An email contains a link the user can click to perform a set of required actions. The redirectUri and clientId parameters are optional. If no redirect is given, then there will be no link back to click after actions have completed. Redirect uri must be a valid uri for the particular clientId.
        /// </summary>
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Stream?> PutAsync(List<string> body, Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder.ExecuteActionsEmailRequestBuilderPutQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<Stream> PutAsync(List<string> body, Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder.ExecuteActionsEmailRequestBuilderPutQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPutRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// An email contains a link the user can click to perform a set of required actions. The redirectUri and clientId parameters are optional. If no redirect is given, then there will be no link back to click after actions have completed. Redirect uri must be a valid uri for the particular clientId.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPutRequestInformation(List<string> body, Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder.ExecuteActionsEmailRequestBuilderPutQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPutRequestInformation(List<string> body, Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder.ExecuteActionsEmailRequestBuilderPutQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PUT, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.SetContentFromScalarCollection(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.ExecuteActionsEmail.ExecuteActionsEmailRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// An email contains a link the user can click to perform a set of required actions. The redirectUri and clientId parameters are optional. If no redirect is given, then there will be no link back to click after actions have completed. Redirect uri must be a valid uri for the particular clientId.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ExecuteActionsEmailRequestBuilderPutQueryParameters 
        {
            /// <summary>Client id</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("client_id")]
            public string? ClientId { get; set; }
#nullable restore
#else
            [QueryParameter("client_id")]
            public string ClientId { get; set; }
#endif
            /// <summary>Number of seconds after which the generated token expires</summary>
            [QueryParameter("lifespan")]
            public int? Lifespan { get; set; }
            /// <summary>Redirect uri</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("redirect_uri")]
            public string? RedirectUri { get; set; }
#nullable restore
#else
            [QueryParameter("redirect_uri")]
            public string RedirectUri { get; set; }
#endif
        }
    }
}
#pragma warning restore CS0618
