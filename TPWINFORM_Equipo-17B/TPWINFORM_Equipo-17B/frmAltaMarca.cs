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
    public partial class frmAltaMarca : Form
    {
        private Marca marcaSeleccionada = null;

        public frmAltaMarca()
        {
            InitializeComponent();
        }

        private void frmAltaMarca_Load(object sender, EventArgs e)
        {
            cargarMarcas();
        }

        private void cargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                dgvMarcas.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca nueva = new Marca();

            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Por favor, ingrese una descripción para la marca.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nueva.Descripcion = txtDescripcion.Text.Trim();
                negocio.agregar(nueva);
                MessageBox.Show("Marca agregada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtDescripcion.Clear();
                cargarMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una marca para editar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            marcaSeleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
            txtDescripcion.Text = marcaSeleccionada.Descripcion;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                if (marcaSeleccionada == null)
                {
                    MessageBox.Show("Por favor, seleccione una marca para modificar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Por favor, ingrese una descripción para la marca.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                marcaSeleccionada.Descripcion = txtDescripcion.Text.Trim();
                negocio.Modificar(marcaSeleccionada);
                MessageBox.Show("Marca modificada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDescripcion.Clear();
                marcaSeleccionada = null;
                cargarMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca seleccionada;

            try
            {
                if (dgvMarcas.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione una marca para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Estás seguro que querés eliminar la Marca?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionada.Id);
                    MessageBox.Show("Marca eliminada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarMarcas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
