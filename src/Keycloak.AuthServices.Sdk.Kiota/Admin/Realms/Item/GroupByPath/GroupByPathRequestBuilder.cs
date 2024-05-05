// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.GroupByPath.Item;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.GroupByPath {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\group-by-path
    /// </summary>
    public class GroupByPathRequestBuilder : BaseRequestBuilder {
        /// <summary>Gets an item from the Keycloak.AuthServices.Sdk.Kiota.Admin.admin.realms.item.groupByPath.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="WithPathItemRequestBuilder"/></returns>
        public WithPathItemRequestBuilder this[string position] { get {
            var urlTplParams = new Dictionary<string, object>(PathParameters);
            urlTplParams.Add("path", position);
            return new WithPathItemRequestBuilder(urlTplParams, RequestAdapter);
        } }
        /// <summary>
        /// Instantiates a new <see cref="GroupByPathRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupByPathRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/group-by-path", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="GroupByPathRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupByPathRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/group-by-path", rawUrl) {
        }
    }
}
