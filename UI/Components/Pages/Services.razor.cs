using DataAccess.Model;
using DataAccess;
using Microsoft.AspNetCore.Components;
using UI.Services;
using Blazored.Modal;
using UI.Components.Create;
using Blazored.Modal.Services;

namespace UI.Components.Pages
{
    public partial class Services
    {
        [Inject] private IModalService _modal { get; set; }

        [Inject] private Context _context { get; set; }
        [Inject] private NavigationHandler _navigationHandler { get; set; }

        private void OpenDetail(Service service) => this._navigationHandler.NavigateToEntity(service);

        private async Task CreateService()
        {
            var modal = this._modal.Show<CreateServiceComponent>("Neuer Service");
            var modalResult = await modal.Result;

            if (!modalResult.Cancelled && modalResult.Data is Service service)
            {
                await this.InvokeAsync(StateHasChanged);
            }
        }
    }
}