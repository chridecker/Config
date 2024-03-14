using DataAccess.Model;
using Microsoft.AspNetCore.Components;
using UI.Constants;

namespace UI.Services
{
    public class NavigationHandler
    {
        private readonly NavigationManager _navigationManager;

        public NavigationHandler(NavigationManager navigationManager)
        {
            this._navigationManager = navigationManager;
        }

        public void NavigateToEntity<T>(T entity) where T : BaseEntity
        {
            if (entity is null) { return; }

            var route = entity.GetType() switch
            {
                _ when entity.GetType() == typeof(Service) => RouteConstants.Service,
                _ when entity.GetType() == typeof(ServiceVersion) => RouteConstants.ServiceVersion,
                _ when entity.GetType() == typeof(Setting) => RouteConstants.ServiceVersion,
                _ => null
            };

            if (route is null) { return; }

            route = route.Replace(RouteParameterConstants.Id, entity.Id.ToString());

            this._navigationManager.NavigateTo(route);
        }
    }
}
