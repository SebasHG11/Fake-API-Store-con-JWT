namespace Api1.Models{
    
    public class ProductoDTO{
        public int? Id {get; set;}
        public string? Nombre {get; set;}
        public double? Precio {get; set;}
        public int IdCategoria{get; set;}
        public string? Descripcion {get; set;}
        public string? Imagen {get; set;}
    }

}