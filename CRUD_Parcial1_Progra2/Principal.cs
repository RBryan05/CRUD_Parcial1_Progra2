using CapaDatos;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Parcial1_Progra2
{
    public partial class Principal : Form
    {
        ClientesDAL _clientesDAL;
        public Principal()
        {                     
            InitializeComponent();
            Datos();
        }

        private void Datos()
        {
            _clientesDAL = new ClientesDAL();
            dgvClientes.DataSource = _clientesDAL.ObtenerClientes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _clientesDAL = new ClientesDAL();
                int id = int.Parse(txtId.Text);
                dgvClientes.DataSource = _clientesDAL.ObtenerClientesID(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrio un error. {ex}");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregarcliente agregarcliente = new Agregarcliente();
            agregarcliente.ShowDialog();
            Datos();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int id = int.Parse(dgvClientes.Rows[e.RowIndex].Cells["Identificacion"].Value.ToString());

                    if (dgvClientes.Columns[e.ColumnIndex].Name.Equals("Editar"))
                    {
                        Agregarcliente agregarcliente = new Agregarcliente(id);
                        agregarcliente.ShowDialog();
                        Datos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrio un error {ex}");
            }
        }
    }
}
