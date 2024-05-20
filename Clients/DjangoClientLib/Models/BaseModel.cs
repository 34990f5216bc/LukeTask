using DjangoClientLib.Models.Contracts;
using System.Text.Json;

namespace DjangoClientLib.Models
{
    public class BaseModel : IToJson
    {
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
