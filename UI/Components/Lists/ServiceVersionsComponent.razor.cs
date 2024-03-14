using DataAccess.Model;
using Microsoft.AspNetCore.Components;
using UI.Services;

namespace UI.Components.Lists
{
    public partial class ServiceVersionsComponent
    {
        [Inject] private NavigationHandler _navigationHandler { get; set; }
        [Parameter] public ICollection<ServiceVersion> ServiceVersions { get; set; }

        private void OpenDetail(ServiceVersion entity) => this._navigationHandler.NavigateToEntity(entity);
    }
}