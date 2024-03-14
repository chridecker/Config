using Blazored.Modal.Services;
using Blazored.Modal;
using DataAccess.Model;
using Microsoft.AspNetCore.Components;
using DataAccess;

namespace UI.Components.Create
{
    public partial class CreateServiceComponent
    {
        [Inject] private Context _context { get; set; }
        [CascadingParameter] private BlazoredModalInstance _modal { get; set; } = default!;

        private string _error;

        private string _name;

        private async Task Submit()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this._name)) { throw new Exception("Name darf nicht leer sein"); }

                var entity = new Service
                {
                    Name = this._name
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
    }
}