namespace MiMenu_Back.Data.DTOs.Payment
{
    public class WebHookDto
    {
        public string action { get; set; }
        public string api_version { get; set; }
        public DataMP data { get; set; }
        public string date_created { get; set; }
        public long id { get; set; }
        public bool live_mode { get; set; }
        public string type { get; set; }
        public string user_id { get; set; }
    }
    public class DataMP
    {
        public string id { get; set; }
    }
}
