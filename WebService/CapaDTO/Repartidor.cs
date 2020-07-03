using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Repartidor
    {
        private int id;
        private string nombre;
        private string apellidos;
        private string celular;
        private string rut;
        private string dv;
        private string email;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Celular { get => celular; set => celular = value; }
        public string Rut { get => rut; set => rut = value; }
        public string Dv { get => dv; set => dv = value; }
        public string Email { get => email; set => email = value; }
        public int Id { get => id; set => id = value; }
    }
}
