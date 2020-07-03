using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Venta
    {
        private readonly int id;
        private int idCarrito;
        private int idUsuario;
        private DateTime fecha;
        private int subTotal;
        private int total;
        private int propina;

        [Required(ErrorMessage = "El id del carrito es requerido.")]
        public int IdCarrito { get => idCarrito; set => idCarrito = value; }

        [Required(ErrorMessage = "El id del usuario es requerido.")]
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }

        [Required(ErrorMessage = "El sub total es requerido.")]
        public int SubTotal { get => subTotal; set => subTotal = value; }

        [Required(ErrorMessage = "El total es requerido.")]

        public int Total { get => total; set => total = value; }
        public int Propina { get => propina; set => propina = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }

        public int Id => id;
    }
}
