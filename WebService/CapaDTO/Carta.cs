using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO
{
    public class Carta
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string precio_unidad;
        private int id_promocion;
        private int id_foto;
        private string urlFoto;
        private DateTime created_at;
        private IEnumerable<Promocion> promocion;
        private IEnumerable<Ingrediente> ingredientes;

        [Required(ErrorMessage = "El nombre de la carta es requerido.")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "El precio de la carta es requerido.")]
        public string Precio_unidad { get => precio_unidad; set => precio_unidad = value; }

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Id_foto { get => id_foto; set => id_foto = value; }
        public int Id_promocion { get => id_promocion; set => id_promocion = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
        public IEnumerable<Ingrediente> Ingredientes { get => ingredientes; set => ingredientes = value; }

        public IEnumerable<Promocion> Promocion { get => promocion; set => promocion = value; }
        public string UrlFoto { get => urlFoto; set => urlFoto = value; }
    }
}
