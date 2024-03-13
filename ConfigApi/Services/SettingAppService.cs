using ConfigApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConfigApi.Services
{
    public class SettingAppService : ISettingAppService
    {
        private readonly Context _context;

        public SettingAppService(Context context)
        {
            this._context = context;
        }

        public async Task<Setting> LoadServiceSetting(Guid id, Version version, CancellationToken token)
        {
            var svc = await this._context.Services.Include(s => s.Versions).ThenInclude(s => s.SettingObj).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Kein Service gefunden");
            var serviceVersion = svc.Versions.FirstOrDefault(x => x.Version == version.ToString()) ?? throw new Exception($"Version {version} von Service {svc.Name} existiert nicht");
            return serviceVersion.SettingObj;
        }
    }
    public interface ISettingAppService
    {
        Task<Setting> LoadServiceSetting(Guid id, Version version, CancellationToken token);
    }
}
