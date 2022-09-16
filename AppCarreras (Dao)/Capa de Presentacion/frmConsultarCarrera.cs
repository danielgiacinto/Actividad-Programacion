using AppCarreras__Dao_.Capa_de_Acceso_a_Datos;
using AppCarreras__Dao_.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppCarreras__Dao_.Capa_de_Presentacion
{
    public partial class frmConsultarCarrera : Form
    {
        Carrera carrera;
        public frmConsultarCarrera()
        {
            InitializeComponent();
            carrera = new Carrera();
        }

        private void frmConsultarCarrera_Load(object sender, EventArgs e)
        {
            DataTable table = HelperDAO.ObtenerInstancia().ConsultaSql("sp_consultar_carreras");
            cboCarreras.DataSource = table;
            cboCarreras.DisplayMember = "nombre";
            cboCarreras.ValueMember = "id_carrera";
            cboCarreras.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnConsultarCarrera_Click(object sender, EventArgs e)
        {
            if (cboCarreras.SelectedIndex != -1)
            {
                string carrera = cboCarreras.Text;
                dgvDetalles.DataSource = HelperDAO.ObtenerInstancia().consultarDetalleCarrera(carrera);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cboCarreras.SelectedIndex != -1)
            {
                int id = (int)cboCarreras.SelectedValue;

                if (MessageBox.Show("Quiere Dar de Baja la Carrera ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes
                    && HelperDAO.ObtenerInstancia().bajaCarrera(id))
                {
                    MessageBox.Show("Se registró la baja de la carrera!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
        }
    }
}
