using Blazored.Modal;
using Blazored.Modal.Services;
using DataAccess.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UI.Components.Base;
using UI.Components.Create;

namespace UI.Components.Pages
{
    public partial class ServicePage : BaseEntityComponent<Service>
    {
        [Inject] private IModalService _modal { get; set; }

        private ServiceVersion? _latest => this._entity.LatestVersion;

        protected override Func<DbSet<Service>, IIncludableQueryable<Service, object>>? Include => enitities => enitities.Include(x => x.Versions).ThenInclude(x => x.SettingObj).Include(x => x.Settings);

        private async Task CreateVersion()
        {
            var param = new ModalParameters();
            param.Add(nameof(Id), this.Id);

            var modal = this._modal.Show<CreateServiceVersionComponent>("Neue Version für " + this._entity.Name, param);
            var modalResult = await modal.Result;

            if (!modalResult.Cancelled && modalResult.Data is ServiceVersion version)
            {
                await this.InvokeAsync(StateHasChanged);
            }
        }

        private async Task CreateSetting()
        {

        }
    }
}