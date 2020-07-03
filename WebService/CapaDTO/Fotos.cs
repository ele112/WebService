using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Fotos
    {
        private readonly int id;
        private string nombre;
        private string url;
        private DateTime createdAt;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Url { get => url; set => url = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }

        public int Id => id;
    }
}
