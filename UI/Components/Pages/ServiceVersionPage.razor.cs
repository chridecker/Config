using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using UI.Components.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace UI.Components.Pages
{
    public partial class ServiceVersionPage : BaseEntityComponent<ServiceVersion>
    {
        protected override Func<DbSet<ServiceVersion>, IIncludableQueryable<ServiceVersion, object>>? Include => entities => entities.Include(x => x.ServiceObj).Include(x => x.SettingObj);
    }
}