namespace ConfigApi.Model
{
    public class Setting
    {
        public int Id { get; set; }
        public Guid ServiceId { get; set; }
        public string Version { get; set; }
        public string Value { get; set; }
    }
}
