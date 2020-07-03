using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CapaDTO
{
    public class Usuario
    {
        private string rut;
        private string dv;
        private string nombre;
        private string apellidos;
        private string direccion;
        private string email;
        private string celular;
        private string password;
        private DateTime createdAt;
        private DateTime deletedAt;

        [Required(ErrorMessage = "Rut es requerido  ")]
        public string Rut { get => rut; set => rut = value; }

        [Required(ErrorMessage = "Digito Verificador es requerido  ")]
        public string Dv { get => dv; set => dv = value; }

        [Required(ErrorMessage = "Nombre es requerido  ")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "Rut es requerido  ")]
        public string Apellidos { get => apellidos; set => apellidos = value; }

        [Required(ErrorMessage = "Direccion es requerido  ")]
        public string Direccion { get => direccion; set => direccion = value; }

        [Required(ErrorMessage = "Email es requerido  ")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Formato de email no valido.")]
        public string Email { get => email; set => email = value; }

        public string Celular { get => celular; set => celular = value; }
        public string Password { get => password; set => password = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime DeletedAt { get => deletedAt; set => deletedAt = value; }

        
        
    }
}
