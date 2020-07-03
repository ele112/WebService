using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class CartaIngrediente
    {
        private int idCarta;
        private int idIngrediente;
        private int cantidad;

        public int IdCarta { get => idCarta; set => idCarta = value; }
        public int IdIngrediente { get => idIngrediente; set => idIngrediente = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}
