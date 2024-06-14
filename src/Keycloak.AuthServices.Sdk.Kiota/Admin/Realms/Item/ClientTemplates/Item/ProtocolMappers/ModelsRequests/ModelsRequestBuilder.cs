// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.Item;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\client-templates\{client-scope-id}\protocol-mappers\models
    /// </summary>
    public class ModelsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the Keycloak.AuthServices.Sdk.Kiota.Admin.admin.realms.item.clientTemplates.item.protocolMappers.modelsRequests.item collection</summary>
        /// <param name="position">Mapper id</param>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.Item.ModelsItemRequestBuilder"/></returns>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.Item.ModelsItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.Item.ModelsItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.ModelsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ModelsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-templates/{client%2Dscope%2Did}/protocol-mappers/models", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.ModelsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ModelsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-templates/{client%2Dscope%2Did}/protocol-mappers/models", rawUrl)
        {
        }
        /// <summary>
        /// Get mappers
        /// </summary>
        /// <returns>A List&lt;Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>(requestInfo, Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <summary>
        /// Create a mapper
        /// </summary>
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Stream?> PostAsync(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<Stream> PostAsync(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get mappers
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Create a mapper
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.ModelsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.ModelsRequestBuilder WithUrl(string rawUrl)
        {
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ProtocolMappers.ModelsRequests.ModelsRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
