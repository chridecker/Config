using DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DataAccess.Model
{
    public class ServiceVersion
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(ServiceObj))]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(SettingObj))]
        public int? SettingId { get; set; }
        public string Version { get; set; }
        public EServiceConfiguration ServiceConfiguration { get; set; }

        public Service ServiceObj { get; set; }
        public Setting? SettingObj { get; set; }

    }
}
