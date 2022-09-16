using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pilas_Colas
{
    public partial class Form1 : Form
    {
        private Interface coleccion;
        public Form1()
        {
            InitializeComponent();
            coleccion = new Pila(10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtValor.Text != "")
            {
                object elemento = txtValor.Text;
                if (coleccion.agregar(elemento))
                {
                    lstDatos.Items.Add(elemento);
                    MessageBox.Show("Se agrego el elemento !!");
                }
                else
                {
                    MessageBox.Show("No se puede agregar, Se lleno la lista !!");
                }
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (!coleccion.estaVacia())
            {
                object valor = coleccion.primero();
                MessageBox.Show($"El primer elemento es: {valor}");
            }
        }

        private void btnVacia_Click(object sender, EventArgs e)
        {
            if (coleccion.estaVacia())
            {
                MessageBox.Show("Vacio !!");
            }
            else
            {
                MessageBox.Show("Con elementos !!");
            }
        }

        private void btnExtraer_Click(object sender, EventArgs e)
        {
            object elemento = coleccion.extraer();
            lstDatos.Items.Remove(elemento);
        }
    }
}
