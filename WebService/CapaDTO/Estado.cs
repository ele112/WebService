using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Estado
    {
        private int id;
        private string nombre;
        private DateTime createdAt;
        private DateTime deletedAt;
        
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime DeletedAt { get => deletedAt; set => deletedAt = value; }
        public int Id { get => id; set => id = value; }
    }
}
