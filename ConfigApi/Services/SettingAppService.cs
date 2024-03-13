using ConfigApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConfigApi.Services
{
    public class SettingAppService : ISettingAppService
    {
        private readonly Context _context;

        public SettingAppService(Context context)
        {
            context.Database.EnsureCreated();
            this._context = context;
        }

        public async Task<Setting> LoadServiceSetting(Guid id, Version version, CancellationToken token)
        {
            var svc = await this._context.Services.FindAsync(id) ?? throw new Exception("Kein Service gefunden");
            if (svc.Version != version.ToString()) { throw new Exception("Falsche Version"); }
            return await this._context.Settings.Where(x => x.ServiceId == svc.Id).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
        }
    }
    public interface ISettingAppService
    {
        Task<Setting> LoadServiceSetting(Guid id, Version version, CancellationToken token);
    }
}
