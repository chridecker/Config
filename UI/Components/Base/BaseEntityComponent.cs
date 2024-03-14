using DataAccess.Model;
using DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace UI.Components.Base
{
    public abstract class BaseEntityComponent<T> : ComponentBase where T : BaseEntity
    {
        [Inject] protected Context _context { get; set; }

        [Parameter] public Guid Id { get; set; }

        protected T _entity;

        protected virtual Func<DbSet<T>, IIncludableQueryable<T, object>>? Include => null;

        protected override async Task OnParametersSetAsync()
        {
            if (this.Include is null)
            {
                this._entity = await this._context.Set<T>()
                    .FirstOrDefaultAsync(x => x.Id == this.Id) ?? throw new Exception($"Konnte Service mit ID [{this.Id}] nicht finden");
            }
            else
            {
                this._entity = await this.Include(this._context.Set<T>())
                    .FirstOrDefaultAsync(x => x.Id == this.Id) ?? throw new Exception($"Konnte Service mit ID [{this.Id}] nicht finden");
            }

            await base.OnParametersSetAsync();
        }
    }
}
