using DataAccess.Model;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using UI.Components.Base;
using UI.Services;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using UI.Components.Json;

namespace UI.Components.Create
{
    public partial class CreateSettingComponent : BaseEntityComponent<Service>
    {
        [Inject] private IModalService _modalService { get; set; }

        [CascadingParameter] private BlazoredModalInstance _modal { get; set; } = default!;

        protected override Func<DbSet<Service>, IIncludableQueryable<Service, object>>? Include => enitities => enitities.Include(x => x.Versions).Include(x => x.Settings);

        private string _error;

        private string _version;
        private string _value;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();

                this._version = VersionHelper.CreateNewVersion(this._entity.LatestSetting?.Version);
                this._value = this._entity.LatestSetting?.Value;
            }
            catch (Exception ex)
            {
                this._error = ex.Message;
            }
        }

        private async Task Submit()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this._value)) { throw new Exception("Einstellung darf nicht leer sein"); }

                var entity = new Setting
                {
                    ServiceId = this._entity.Id,
                    Version = this._version,
                    Value = this._value,
                };

                await this._context.AddAsync(entity);
                await this._context.SaveChangesAsync();

                await this._modal.CloseAsync(ModalResult.Ok(entity));
            }
            catch (Exception ex)
            {
                this._error = ex.Message;
            }
        }

        private async Task OpenViewer()
        {
            var param = new ModalParameters();
            param.Add(nameof(JsonViewer.Value), this._value);

            var modal = this._modalService.Show<JsonViewer>("Einstellung", param);
            await modal.Result;
        }
    }
}