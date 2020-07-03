using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Ingrediente
    {
        private int id;
        private string nombre;
        private string descripcion;


        [Required(ErrorMessage = "Nombre es requerido  ")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Descripcion es requerida  ")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Id { get => id; set => id = value; }
    }
}
