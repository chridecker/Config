using DataAccess.Model;
using DataAccess;
using Microsoft.AspNetCore.Components;
using UI.Services;

namespace UI.Components.Pages
{
    public partial class Services
    {
        [Inject] private Context _context { get; set; }
        [Inject] private NavigationHandler _navigationHandler { get; set; }

        private void OpenDetail(Service service) => this._navigationHandler.NavigateToEntity(service);

        private async Task CreateService()
        {

        }
    }
}