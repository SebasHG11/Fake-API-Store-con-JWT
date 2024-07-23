namespace Api1.Models{
    public class Usuario{
        public int Id {get; set;}
        public string? Nombre {get; set;}
        public string? Contraseña {get; set;}
        public string? Rol {get; set;}
        public string? Foto {get; set;}

        public List<Orden>? Ordenes {get; set;}

        public Usuario()
        {
            Ordenes = new List<Orden>();
        }
    }
}