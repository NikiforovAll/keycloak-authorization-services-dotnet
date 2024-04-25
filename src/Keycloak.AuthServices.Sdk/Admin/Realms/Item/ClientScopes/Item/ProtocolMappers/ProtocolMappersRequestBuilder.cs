// <auto-generated/>
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.ClientScopes.Item.ProtocolMappers.AddModels;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.ClientScopes.Item.ProtocolMappers.ModelsRequests;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.ClientScopes.Item.ProtocolMappers.Protocol;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.ClientScopes.Item.ProtocolMappers {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\client-scopes\{client-scope-id}\protocol-mappers
    /// </summary>
    public class ProtocolMappersRequestBuilder : BaseRequestBuilder {
        /// <summary>The addModels property</summary>
        public AddModelsRequestBuilder AddModels { get =>
            new AddModelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The models property</summary>
        public ModelsRequestBuilder Models { get =>
            new ModelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The protocol property</summary>
        public ProtocolRequestBuilder Protocol { get =>
            new ProtocolRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="ProtocolMappersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ProtocolMappersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-scopes/{client%2Dscope%2Did}/protocol-mappers", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="ProtocolMappersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ProtocolMappersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-scopes/{client%2Dscope%2Did}/protocol-mappers", rawUrl) {
        }
    }
}
