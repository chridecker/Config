using DataAccess.Model;
using ConfigApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess;

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

        public async Task<string> BuildToken(Guid id)
        {
            var service = await this.FindService(id);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, service.Name),
                new(ClaimTypes.NameIdentifier, service.Id.ToString()),
                new(ClaimTypes.GivenName, service.Name),
                new(ClaimTypes.DateOfBirth, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._optionsMonitor.CurrentValue.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(this._optionsMonitor.CurrentValue.Issuer, this._optionsMonitor.CurrentValue.Issuer, claims, expires: DateTime.MaxValue, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<Service> ValidateToken(string jwt, CancellationToken token = default)
        {
            var decrypt = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            var id = decrypt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ?? throw new Exception($"No Claim with Type {nameof(ClaimTypes.NameIdentifier)}");
            return await this.FindService(Guid.Parse(id.Value));
        }

        private async Task<Service> FindService(Guid id)
            => await this._context.Services.Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("Konnte Service nicht in der Datenbank finden");
    }

    public interface IJwtTokenService
    {
        Task<string> BuildToken(Guid id);
        Task<Service> ValidateToken(string jwt, CancellationToken token = default);
    }
}
