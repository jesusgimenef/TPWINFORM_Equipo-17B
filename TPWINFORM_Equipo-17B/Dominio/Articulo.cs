using System.Collections.Generic;

namespace Dominio
{
	public class Articulo
	{
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public Marca Marca { get; set; }
		public Categoria Categoria { get; set; }
		public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
		public decimal Precio { get; set; }
        public string UrlImagen { get; set; }
        public string MarcaDescripcion => Marca != null ? Marca.Descripcion : "";
        public string CategoriaDescripcion => Categoria != null ? Categoria.Descripcion : "";
    }
}


