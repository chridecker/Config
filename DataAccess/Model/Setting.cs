using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Setting : BaseEntity
    {

        [ForeignKey(nameof(ServiceObj))]
        public Guid ServiceId { get; set; }

        public string Version { get; set; }
        public string Value { get; set; }

        public Service ServiceObj { get; set; }
    }
}
