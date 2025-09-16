using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWINFORM_Equipo_17B
{
    public partial class frmDetalleProducto : Form
    {
        private Articulo _producto;
        private int indiceImagenActual = 0;

        public frmDetalleProducto()
        {
            InitializeComponent();
        }

        // Constructor que recibe un artículo completo
        public frmDetalleProducto(Articulo articulo) : this()
        {
            _producto = articulo;
        }

        private void frmDetalleProducto_Load(object sender, EventArgs e)
        {
            if (_producto == null)
                return;

            // Cargar datos del artículo
            txtCodigo.Text = _producto.Codigo;
            txtNombre.Text = _producto.Nombre;
            txtDescripcion.Text = _producto.Descripcion;
            txtPrecio.Text = _producto.Precio.ToString("C");
            txtMarca.Text = _producto.MarcaDescripcion;
            txtCategoria.Text = _producto.CategoriaDescripcion;

            MostrarImagen();
        }

        private void MostrarImagen()
        {
            if (_producto.Imagenes != null && _producto.Imagenes.Count > 0)
            {
                CargarImagen(_producto.Imagenes[indiceImagenActual].Imagen_URL);
            }
            else
            {
                CargarImagen("");
            }

            ActualizarBotones();
        }

        private void CargarImagen(string url)
        {
            try
            {
                pbxImagen.Load(url);
            }
            catch
            {
                pbxImagen.Load("https://media.istockphoto.com/id/1128826884/vector/no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment.jpg?s=612x612&w=0&k=20&c=390e76zN_TJ7HZHJpnI7jNl7UBpO3UP7hpR2meE1Qd4=");
            }
        }

        private void ActualizarBotones()
        {
            btnAnterior.Enabled = _producto.Imagenes != null && indiceImagenActual > 0;
            btnSiguiente.Enabled = _producto.Imagenes != null && indiceImagenActual < _producto.Imagenes.Count - 1;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagenActual > 0)
            {
                indiceImagenActual--;
                MostrarImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_producto.Imagenes != null && indiceImagenActual < _producto.Imagenes.Count - 1)
            {
                indiceImagenActual++;
                MostrarImagen();
            }
        }
    }
}
