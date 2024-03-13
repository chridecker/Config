using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Configuration
{
    public class ApiConfigurationProvider : JsonStreamConfigurationProvider
    {
        private PeriodicTimer _periodicTimer;
        private DateTime? _loadedTime;
        private readonly int? _reloadSeconds;

        public IServiceProvider Services { get; set; }
        public CancellationToken Token { get; set; }

        public Func<IHttpClientFactory, CancellationToken, Task<Stream>> ReloadStream { get; set; }

        public DateTime? LoadTime => this._loadedTime;

        public ApiConfigurationProvider(JsonStreamConfigurationSource source, int? reloadSeconds) : base(source)
        {
            this._reloadSeconds = reloadSeconds;
        }

        public override void Load(Stream stream)
        {
            this._loadedTime = DateTime.Now;
            base.Load(stream);
            if (this._reloadSeconds.HasValue)
            {
                this._periodicTimer = new(TimeSpan.FromSeconds(this._reloadSeconds.Value));
                this.ReloadTimer();
            }
        }

        private void ReloadTimer() => Task.Run(async () =>
        {
            while (await this._periodicTimer.WaitForNextTickAsync())
            {
                await this.Reload(this.Token);
            }
        }, this.Token);

        public async Task Reload(CancellationToken cancellationToken = default)
        {
            using var stream = await this.ReloadStream(this.Services.GetRequiredService<IHttpClientFactory>(), cancellationToken);
            base.Data.Clear();
            base.Load(stream);
            this._loadedTime = DateTime.Now;
            this.OnReload();
        }
    }
}
