using System.Text.Json.Serialization;

namespace Api1.Models{
    public class Categoria{
        public int Id {get; set;}
        public string? Nombre {get; set;}
        public string? Imagen {get; set;}
        
        [JsonIgnore]
        public ICollection<Producto>? Productos {get; set;}
    }
}