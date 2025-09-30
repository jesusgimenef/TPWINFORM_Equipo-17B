using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPWINFORM_Equipo_17B
{
    public partial class frmAltaCategoria : Form
    {
        private Categoria categoriaSeleccionada = null;

        public frmAltaCategoria()
        {
            InitializeComponent();
        }

        private void frmAltaCategoria_Load(object sender, EventArgs e)
        {
            cargarCategorias();
        }

        private void cargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                dgvCategorias.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria nueva = new Categoria();

            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Por favor, ingrese una descripción para la categoría.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nueva.Descripcion = txtDescripcion.Text.Trim();
                negocio.agregar(nueva);
                MessageBox.Show("Categoría agregada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtDescripcion.Clear();
                cargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una categoría para editar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            categoriaSeleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            txtDescripcion.Text = categoriaSeleccionada.Descripcion;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if (categoriaSeleccionada == null)
                {
                    MessageBox.Show("Por favor, seleccione una categoría para modificar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Por favor, ingrese una descripción para la categoría.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                categoriaSeleccionada.Descripcion = txtDescripcion.Text.Trim();
                negocio.Modificar(categoriaSeleccionada);
                MessageBox.Show("Categoría modificada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDescripcion.Clear();
                categoriaSeleccionada = null;
                cargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionada;

            try
            {
                if (dgvCategorias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione una categoría para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Estás seguro que querés eliminar la Categoría?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionada.Id);
                    MessageBox.Show("Categoría eliminada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarCategorias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
