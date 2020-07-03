using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Promocion
    {
        private int id;
        private string nombre;
        private string descripcion;
        private int porc_descuento;
        private int max_descuento;
        private DateTime created_at;
        private DateTime expired_at;

        [Required(ErrorMessage = "Nombre es requerido  ")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "Porcentaje de descuento")]

        public string Descripcion { get => descripcion; set => descripcion = value; }


        public int Porc_descuento { get => porc_descuento; set => porc_descuento = value; }
        public int Max_descuento { get => max_descuento; set => max_descuento = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
        public DateTime Expired_at { get => expired_at; set => expired_at = value; }
        public int Id { get => id; set => id = value; }
    }
}
