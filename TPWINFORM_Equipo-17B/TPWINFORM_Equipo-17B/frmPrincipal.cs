using Negocio;
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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        private Articulo articuloSeleccionado;
        private int indiceImagenActual = 0;

        public frmPrincipal()
        {
            InitializeComponent();
        } 

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = (negocio.listar());
                dgvArticulos.DataSource = listaArticulos;
                //dgvArticulos.Columns["UrlImagen"].Visible = false;
                dgvArticulos.Columns["Marca"].Visible = false;
                dgvArticulos.Columns["Categoria"].Visible = false;

                dgvArticulos.Columns["MarcaDescripcion"].HeaderText = "Marca";
                dgvArticulos.Columns["CategoriaDescripcion"].HeaderText = "Categoria";

                if (listaArticulos.Count > 0)
                {
                    articuloSeleccionado = listaArticulos[0];
                    indiceImagenActual = 0;
                    mostrarImagen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar artículos: " + ex.Message);
            }
        }

        private void dgvArticulos_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                indiceImagenActual = 0;
                mostrarImagen();
            }
        }

        private void mostrarImagen()
        {
            if (articuloSeleccionado.Imagenes.Count > 0)
                cargarImagen(articuloSeleccionado.Imagenes[indiceImagenActual].Imagen_URL);
            else
                cargarImagen("");

            actualizarBotones();
        }

        private void cargarImagen(string url)
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

        private void actualizarBotones()
        {
            btnAnterior.Enabled = indiceImagenActual > 0;
            btnSiguiente.Enabled = indiceImagenActual < articuloSeleccionado.Imagenes.Count - 1;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagenActual > 0)
            {
                indiceImagenActual--;
                mostrarImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceImagenActual < articuloSeleccionado.Imagenes.Count - 1)
            {
                indiceImagenActual++;
                mostrarImagen();
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Estas seguro que queres eliminar el Articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);
                }
                    return;
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
