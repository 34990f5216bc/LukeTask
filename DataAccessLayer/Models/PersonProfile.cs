using System.Text.Json;

namespace DataAccessLayer.Models
{
    public class PersonProfile
    {
        public string FullName { get; set; }
        public string[] Films { get; set; }
        public string[] Vehicles { get; set; }
        public string[] Starships { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
