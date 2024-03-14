using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UI.Components.Base;

namespace UI.Components.Pages
{
    public partial class ServicePage : BaseEntityComponent<Service>
    {
        private ServiceVersion? _latest => this._entity.LatestVersion;

        protected override Func<DbSet<Service>, IIncludableQueryable<Service, object>>? Include => enitities => enitities.Include(x => x.Versions).ThenInclude(x => x.SettingObj);
    }
}