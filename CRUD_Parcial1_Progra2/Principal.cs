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
                    else if (dgvClientes.Columns[e.ColumnIndex].Name.Equals("Eliminar"))
                    {
                        var desicion = MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Eliminar Persona",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        _clientesDAL = new ClientesDAL();

                        int resultado = 0;

                        if (desicion != DialogResult.Yes)
                        {
                            MessageBox.Show("El registro se continua mostrando en el listado.");
                        }
                        else
                        {
                            resultado = _clientesDAL.EliminarCliente(id);
                            if (resultado > 0)
                            {
                                MessageBox.Show("El registro eliminado con exito.");
                                Datos();
                            }
                            else
                            {
                                MessageBox.Show("No se logró eliminar el registro.");
                            }
                        }
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
