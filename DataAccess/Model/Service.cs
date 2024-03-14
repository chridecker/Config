using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Service
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }

        [ForeignKey(nameof(ServiceVersion.ServiceId))]
        public  ICollection<ServiceVersion> Versions { get; set; }
        
        [ForeignKey(nameof(Setting.ServiceId))]
        public  ICollection<Setting> Settings { get; set; }
    }
}
