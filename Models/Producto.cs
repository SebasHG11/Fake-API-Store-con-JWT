namespace Api1.Models{
    
    public class Producto{
        public int? Id {get; set;}
        public string? Nombre {get; set;}
        public double? Precio {get; set;}
        public int IdCategoria{get; set;}
        public Categoria? Categoria {get; set;}
        public string? Descripcion {get; set;}
        public string? Imagen {get; set;}
    }

}