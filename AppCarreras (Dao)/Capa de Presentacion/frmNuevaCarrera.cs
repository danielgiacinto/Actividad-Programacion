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
    public partial class frmNuevaCarrera : Form
    {
        Carrera carrera = new Carrera();
        public frmNuevaCarrera()
        {
            InitializeComponent();
            ProximaCarrera();
            CargarProductos();
        }
        private void ProximaCarrera()
        {
            int next = HelperDAO.ObtenerInstancia().ProximoIdCarrera();
            lblIdCarrera.Text = "ID CARRERA: " + next.ToString();
        }

        private void CargarProductos()
        {
            DataTable table = HelperDAO.ObtenerInstancia().ConsultaSql("sp_consultar_asignaturas");
            cboMaterias.DataSource = table;
            cboMaterias.ValueMember = "id_asignatura";
            cboMaterias.DisplayMember = "nombre";
            cboMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Asignatura asignatura = new Asignatura();
                asignatura.IdAsignatura = Convert.ToInt32(cboMaterias.SelectedValue);
                asignatura.NombreAsignatura = Convert.ToString(cboMaterias.Text);
                int anioCursado = int.Parse(txtAnioCursado.Text);
                int cuatrimestre = 0;

                if (rbnPrimerCuatrimestre.Checked)
                    cuatrimestre = 1;
                if (rbnSegundoCuatrimestre.Checked)
                    cuatrimestre = 2;

                DetalleCarrera detalle = new DetalleCarrera(anioCursado, cuatrimestre, asignatura);

                carrera.AgregarDetalle(detalle);
                dgvDetalles.Rows.Add(new Object[] { asignatura.IdAsignatura, asignatura.NombreAsignatura, anioCursado, cuatrimestre });

            }
        }

        private bool validar()
        {
            if (txtNombreCarrera.Text == "")
            {
                MessageBox.Show("Por favor ingrese un Nombre de la Carrera");
                txtNombreCarrera.Focus();
                return false;
            }
            if (cboMaterias.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una materia");
                return false;
            }
            if (txtAnioCursado.Text == "")
            {
                MessageBox.Show("Por favor coloque el Año Cursado");
                txtAnioCursado.Focus();
                return false;
            }
            else
            {
                try
                {
                    int.Parse(txtAnioCursado.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ingrese un valor numerico en el Año de Cursado");
                    txtAnioCursado.Focus();
                    return false;
                }
            }
            if (!rbnPrimerCuatrimestre.Checked && !rbnSegundoCuatrimestre.Checked)
            {
                MessageBox.Show("Seleccione un cuatrimestre");
                return false;
            }

            foreach (DetalleCarrera dc in carrera.LisDetalleCarrera)
            {
                if (dc.Asignatura.NombreAsignatura == cboMaterias.Text)
                {
                    MessageBox.Show("La materia ya esta seleccionada !!", "Atencion");
                    return false;
                }
            }
            return true;
        }

        private void frmNuevaCarrera_Load(object sender, EventArgs e)
        {

        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                carrera.EliminarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            carrera.NombreTitulo = txtNombreCarrera.Text;

            if (txtNombreCarrera.Text == "")
            {
                MessageBox.Show("Por favor ingrese un nombre de Carrera !!");
                txtNombreCarrera.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show($"Quiere agregar la Carrera {txtNombreCarrera.Text} ?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) 
                    == DialogResult.Yes && HelperDAO.ObtenerInstancia().Create(carrera))
                {
                    MessageBox.Show($"Se agregó con exito la carrera: {txtNombreCarrera.Text}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }
    }
}
