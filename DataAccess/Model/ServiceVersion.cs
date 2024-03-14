using DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class ServiceVersion : BaseEntity
    {

        [ForeignKey(nameof(ServiceObj))]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(SettingObj))]
        public Guid? SettingId { get; set; }
        public string Version { get; set; }
        public EServiceConfiguration ServiceConfiguration { get; set; }

        public Service ServiceObj { get; set; }
        public Setting? SettingObj { get; set; }

    }
}
