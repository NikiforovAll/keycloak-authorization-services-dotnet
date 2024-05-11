namespace Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

/// <summary>
/// A convention that applies protected resource attributes to controllers and actions.
/// </summary>
internal sealed class ProtectedResourceModelConvention : IApplicationModelConvention
{
    /// <summary>
    /// Applies protected resource attributes to controllers and actions in the application model.
    /// </summary>
    /// <param name="application">The <see cref="ApplicationModel"/> to apply the convention to.</param>
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var controllerAttributes = controller.Attributes.OfType<IProtectedResourceData>();

            foreach (var attribute in controllerAttributes)
            {
                foreach (var selector in controller.Selectors)
                {
                    selector.EndpointMetadata.Add(attribute);
                }
            }

            foreach (var action in controller.Actions)
            {
                var actionAttributes = action.Attributes.OfType<IProtectedResourceData>();

                foreach (var attribute in actionAttributes)
                {
                    foreach (var selector in action.Selectors)
                    {
                        selector.EndpointMetadata.Add(attribute);
                    }
                }
            }
        }
    }
}
