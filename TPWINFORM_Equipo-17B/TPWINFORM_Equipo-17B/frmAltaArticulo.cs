using Dominio;
using Negocio;
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
    public partial class frmAltaArticulo : Form
    {

        private Articulo articuloEnEdicion;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo) : this()
        {
            articuloEnEdicion = articulo;
        }
        private bool validarArticulo()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código no puede estar vacío.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.");
                return false;
            }

            if (txtDescripcion.Text.Trim().Length < 2 || txtDescripcion.Text.All(char.IsDigit))
            {
                MessageBox.Show("La descripción debe tener al menos 2 caracteres y no puede ser solo números.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return false;
            }
            if (txtNombre.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre no puede contener números.");
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Debe ingresar un precio.");
                return false;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un número válido.");
                return false;
            }
            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.");
                return false;
            }

            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (!validarArticulo())
                    return;

                if (articuloEnEdicion == null)
                    articuloEnEdicion = new Articulo();

                articuloEnEdicion.Codigo = txtCodigo.Text;
                articuloEnEdicion.Nombre = txtNombre.Text;
                articuloEnEdicion.Descripcion = txtDescripcion.Text;
                articuloEnEdicion.Marca = (Marca)cboMarca.SelectedItem;
                articuloEnEdicion.Categoria = (Categoria)cboCategoria.SelectedItem;
                articuloEnEdicion.Precio = decimal.Parse(txtPrecio.Text);
                articuloEnEdicion.UrlImagen = txtImagen.Text;

                if (articuloEnEdicion.Id > 0)
                {
                    negocio.Modificar(articuloEnEdicion);
                    MessageBox.Show("Articulo modificado correctamente.");
                }
                else
                {
                    negocio.Agregar(articuloEnEdicion);
                    MessageBox.Show("Articulo agregado correctamente.");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.ToString());
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.ValueMember = "Id";

                if (articuloEnEdicion != null)
                {
                    txtCodigo.Text = articuloEnEdicion.Codigo;
                    txtNombre.Text = articuloEnEdicion.Nombre;
                    txtDescripcion.Text = articuloEnEdicion.Descripcion;
                    txtPrecio.Text = articuloEnEdicion.Precio.ToString();
                    txtImagen.Text = articuloEnEdicion.UrlImagen;

                    if (articuloEnEdicion.Marca != null)
                        cboMarca.SelectedValue = articuloEnEdicion.Marca.Id;
                    if (articuloEnEdicion.Categoria != null)
                        cboCategoria.SelectedValue = articuloEnEdicion.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message);
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            try
            {
                pbxImagen.Load(txtImagen.Text);
            }
            catch
            {
                pbxImagen.Load("https://media.istockphoto.com/id/1128826884/vector/no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment.jpg?s=612x612&w=0&k=20&c=390e76zN_TJ7HZHJpnI7jNl7UBpO3UP7hpR2meE1Qd4=");
            }
        }
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
        private void pbxImagen_Leave(object sender, EventArgs e)
        {

        }
    }
}
