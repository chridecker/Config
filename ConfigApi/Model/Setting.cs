using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigApi.Model
{
    public class Setting
    {
        public int Id { get; set; }

        [ForeignKey(nameof(ServiceObj))]
        public Guid ServiceId { get; set; }
        
        public string Version { get; set; }
        public string Value { get; set; }

        public Service ServiceObj { get; set; }
    }
}
