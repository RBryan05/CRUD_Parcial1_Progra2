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
            if (!chkActivos.Checked)
            {
                dgvClientes.DataSource = _clientesDAL.ObtenerClientes();
                dgvClientes.Columns["Activo"].Visible = false;
            }
            else
            {
                dgvClientes.Columns["Activo"].Visible = false;
                dgvClientes.DataSource = _clientesDAL.FiltrarPorEstado();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _clientesDAL = new ClientesDAL();
                int id = int.Parse(txtId.Text);
                dgvClientes.DataSource = _clientesDAL.ObtenerClientesID2(id);
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
                // Verificar que el clic fue en una celda válida
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Obtener el ID del cliente desde la celda "Identificacion"
                    int id = int.Parse(dgvClientes.Rows[e.RowIndex].Cells["Identificacion"].Value.ToString());

                    // Verificar el nombre de la columna clicada
                    if (dgvClientes.Columns[e.ColumnIndex].Name.Equals("Editar"))
                    {
                        // Abrir el formulario para editar el cliente
                        Agregarcliente agregarcliente = new Agregarcliente(id);
                        agregarcliente.ShowDialog();
                        Datos(); // Actualizar los datos
                    }
                    else if (dgvClientes.Columns[e.ColumnIndex].Name.Equals("Eliminar"))
                    {
                        // Verificar si el CheckBox está marcado para decidir la acción
                        if (chkActivos.Checked)
                        {
                            // Confirmar la eliminación permanente
                            var desicion = MessageBox.Show("¿Está seguro que desea eliminar el registro permanentemente?", "Eliminar Persona",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            _clientesDAL = new ClientesDAL();

                            if (desicion == DialogResult.Yes)
                            {
                                int resultado = _clientesDAL.EliminarClientePermanete(id);
                                if (resultado > 0)
                                {
                                    MessageBox.Show("El registro eliminado con éxito.");
                                    Datos(); // Actualizar los datos
                                }
                                else
                                {
                                    MessageBox.Show("No se logró eliminar el registro.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El registro se continúa mostrando en el listado.");
                            }
                        }
                        else
                        {
                            // Confirmar la eliminación normal
                            var desicion = MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Eliminar Persona",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            _clientesDAL = new ClientesDAL();

                            if (desicion == DialogResult.Yes)
                            {
                                int resultado = _clientesDAL.EliminarCliente(id);
                                if (resultado > 0)
                                {
                                    MessageBox.Show("El registro eliminado con éxito.");
                                    Datos(); // Actualizar los datos
                                }
                                else
                                {
                                    MessageBox.Show("No se logró eliminar el registro.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El registro se continúa mostrando en el listado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }


        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                Datos();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _clientesDAL = new ClientesDAL();
            dgvClientes.DataSource = _clientesDAL.ObtenerPorNombre(txtNombre.Text);
        }

        private void chkActivos_CheckedChanged(object sender, EventArgs e)
        {
            Datos();
        }
    }
}
