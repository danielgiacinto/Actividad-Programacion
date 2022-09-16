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
    public partial class frmCargarAsignatura : Form
    {
        List<Asignatura> LisAsignatura = new List<Asignatura>();
        public frmCargarAsignatura()
        {
            InitializeComponent();
        }

        private void frmCargarAsignatura_Load(object sender, EventArgs e)
        {
            habilitar(false);
            cargarListaAsignaturas();
            lblAsignatura.Focus();
        }
        private void habilitar(bool valor)
        {
            btnInsertar.Enabled = valor;
            txtNombreInsertAsignatura.Enabled = valor;
            lstAsignaturas.Enabled = !valor;
        }
        private void cargarListaAsignaturas()
        {
            lstAsignaturas.Items.Clear();
            LisAsignatura.Clear();

            DataTable table = HelperDAO.ObtenerInstancia().ConsultaSql("sp_consultar_asignaturas");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Asignatura a = new Asignatura();
                a.IdAsignatura = Convert.ToInt32(table.Rows[i][0]);
                a.NombreAsignatura = Convert.ToString(table.Rows[i][1]);

                lstAsignaturas.Items.Add(a);
                LisAsignatura.Add(a);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar(true);
            txtNombreInsertAsignatura.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtNombreInsertAsignatura.Text == "")
            {
                MessageBox.Show("Por favor ingrese una Asignatura !!");
                txtNombreInsertAsignatura.Focus();
                return;
            }

            string nombre = txtNombreInsertAsignatura.Text;

            if (!existeAsignatura())
            {
                if (HelperDAO.ObtenerInstancia().insertAsignatura(nombre))
                {
                    MessageBox.Show("Se ingreso con exito la Asignatura !!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarListaAsignaturas();
                    txtNombreInsertAsignatura.Text = string.Empty;
                }
            }
        }
        private bool existeAsignatura()
        {
            for (int i = 0; i < LisAsignatura.Count; i++)
            {
                if (LisAsignatura[i].NombreAsignatura == txtNombreInsertAsignatura.Text)
                {
                    MessageBox.Show("Esta Asignatura ya existe !!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreInsertAsignatura.Text = "";
                    return true;
                }
            }
            return false;
        }
    }
}
