//using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
    public class PersonVO
    {
        //[JsonPropertyName("code")] -> Mudar o nome
        public int Id { get; set; }

        //[JsonPropertyName("name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[JsonIgnore] -> Não vai ser serializado
        public string Address { get; set; }
        public string Gender { get; set; }

    }
}
