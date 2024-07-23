namespace Api1.Models{
    public class Orden{
        public int? Id {get; set;}
        public DateTime? Fecha {get; set;}
        public int? UsuarioId {get; set;}
        public Usuario? Usuario {get; set;}
        public List<OrdenProducto>? OrdenProductos {get; set;}
    }
}