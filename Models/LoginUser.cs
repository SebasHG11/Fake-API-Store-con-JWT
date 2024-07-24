using System.ComponentModel.DataAnnotations;

namespace Api1.Models{
    public class LoginUser{
        public string Nombre {get; set;}
        public string Contraseña {get; set;}

        public LoginUser(string nombre, string contraseña)
        {
            Nombre = nombre;
            Contraseña = contraseña;
        }
    }
}