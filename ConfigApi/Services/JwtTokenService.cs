using ConfigApi.Model;
using ConfigApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConfigApi.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IOptionsMonitor<JwtSettings> _optionsMonitor;
        private readonly Context _context;

        public JwtTokenService(IOptionsMonitor<JwtSettings> optionsMonitor, Context context)
        {
            this._optionsMonitor = optionsMonitor;
            this._context = context;
        }

        public async Task<string> BuildToken(Guid id, string version)
        {
            var service = await this.FindService(id, version);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, service.Name),
                new(ClaimTypes.NameIdentifier, service.Id.ToString()),
                new(ClaimTypes.GivenName, service.Name),
                new(ClaimTypes.Version, version),
             };

            var expiryDate = DateTime.Now.Add(this._optionsMonitor.CurrentValue?.TokenLifeTime ?? TimeSpan.FromMinutes(1));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._optionsMonitor.CurrentValue.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(this._optionsMonitor.CurrentValue.Issuer, this._optionsMonitor.CurrentValue.Issuer, claims,
                expires: expiryDate, signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<Service> ValidateToken(string jwt, CancellationToken token = default)
        {
            var decrypt = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            var id = decrypt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ?? throw new Exception($"No Claim with Type {nameof(ClaimTypes.NameIdentifier)}");
            var version = decrypt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Version) ?? throw new Exception($"No Claim with Type {nameof(ClaimTypes.Version)}");

            return await this.FindService(Guid.Parse(id.Value), version.Value);
        }

        private async Task<Service> FindService(Guid id, string version)
            => await this._context.Services.FirstOrDefaultAsync(x => x.Id == id && x.Version == version) ?? throw new Exception("Konnte Service nicht in der Datenbank finden");
    }

    public interface IJwtTokenService
    {
        Task<string> BuildToken(Guid id, string version);
        Task<Service> ValidateToken(string jwt, CancellationToken token = default);
    }
}
