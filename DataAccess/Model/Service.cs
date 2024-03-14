using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }

        public ServiceVersion? LatestVersion => this.Versions.OrderByDescending(x => x.Version).FirstOrDefault();

        [ForeignKey(nameof(ServiceVersion.ServiceId))]
        public ICollection<ServiceVersion> Versions { get; set; }

        [ForeignKey(nameof(Setting.ServiceId))]
        public ICollection<Setting> Settings { get; set; }
    }
}
