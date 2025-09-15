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

        public frmDetalleProducto(Articulo articulo) : this()
        {
            _producto = articulo;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
