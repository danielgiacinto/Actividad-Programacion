using AppCarreras__Dao_.Capa_de_Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCarreras__Dao_
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quiere Salir de la Aplicacion ?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void nuevaCarreraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevaCarrera frmNuevaCarrera = new frmNuevaCarrera();
            frmNuevaCarrera.ShowDialog();
        }

        private void consultarCarreraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarCarrera frmConsultarCarrera = new frmConsultarCarrera();
            frmConsultarCarrera.ShowDialog();
        }

        private void cargarAsignaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargarAsignatura frmCargarAsignatura = new frmCargarAsignatura();
            frmCargarAsignatura.ShowDialog();
        }

        private void verReporteDeCarrerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporte frmReporte = new frmReporte();
            frmReporte.ShowDialog();
        }
    }
}
