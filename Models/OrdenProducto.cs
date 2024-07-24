using System.Text.Json.Serialization;

namespace Api1.Models{
    public class OrdenProducto{
        public int Id {get; set;}
        public int OrdenId {get; set;}
        [JsonIgnore]
        public Orden? Orden {get; set;}
        public int ProductoId {get; set;}
        public Producto? Producto {get; set;}
        public int Cantidad {get; set;}
    }
}