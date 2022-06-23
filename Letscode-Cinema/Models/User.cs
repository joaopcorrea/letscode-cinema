using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Letscode_Cinema.Models
{
    public class User
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Email { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public DateTime BirthDate { get; set; }
        [JsonProperty]
        public long CPF { get; set; }
        [JsonProperty]
        public bool IsAdmin { get; set; }
        [JsonProperty]
        public bool IsStudent { get; set; }
    }
}
