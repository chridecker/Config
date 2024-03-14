using DataAccess.Model;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using UI.Components.Base;
using UI.Services;
using DataAccess.Enums;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Create
{
    public partial class CreateServiceVersionComponent : BaseEntityComponent<Service>
    {
        [CascadingParameter] private BlazoredModalInstance _modal { get; set; } = default!;

        protected override Func<DbSet<Service>, IIncludableQueryable<Service, object>>? Include => enitities => enitities.Include(x => x.Versions).Include(x => x.Settings);

        private string _error;

        private string _version;
        private Setting? _setting;
        private EServiceConfiguration _configuration;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync();

                this._version = VersionHelper.CreateNewVersion(this._entity.LatestVersion?.Version);
                this._setting = this._entity.LatestSetting;
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
                if (this._configuration == EServiceConfiguration.None) { throw new Exception("Konfiguration ungültig"); }

                var entity = new ServiceVersion
                {
                    ServiceId = this._entity.Id,
                    Version = this._version,
                    ServiceConfiguration = this._configuration,
                };

                if (this._setting is not null)
                {
                    entity.SettingId = this._setting.Id;
                }

                await this._context.Versions.AddAsync(entity);
                await this._context.SaveChangesAsync();

                await this._modal.CloseAsync(ModalResult.Ok(entity));
            }
            catch (Exception ex)
            {
                this._error = ex.Message;
            }
        }
    }
}