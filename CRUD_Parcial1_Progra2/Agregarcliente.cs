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
    public partial class Agregarcliente : Form
    {
        ClientesDAL _clientesDAL;
        int _id;
        public Agregarcliente(int id = 0)
        {
            _id = id;
            InitializeComponent();
            if (_id > 0)
            {
                _clientesDAL = new ClientesDAL();
                var datos = _clientesDAL.ObtenerClientesID(_id);
                txtNombre.Text = datos.Nombres.ToString();
                txtApellidos.Text = datos.Apellidos.ToString();
                txtTelefono.Text = datos.Telefono.ToString();
                txtDireccion.Text = datos.Direccion.ToString();
                txtCorreo.Text = datos.Correo.ToString();
                txtEstadoCivil.Text = datos.EstadoCivil.ToString();
                chbActivo.Checked = datos.Activo;

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _clientesDAL = new ClientesDAL();
            if (_id > 0)
            {
                try
                {
                    Clientes datos = new Clientes
                    {
                        Nombres = txtNombre.Text,
                        Apellidos = txtApellidos.Text,
                        Telefono = int.Parse(txtTelefono.Text),
                        Direccion = txtDireccion.Text,
                        Correo = txtCorreo.Text,
                        EstadoCivil = txtEstadoCivil.Text,
                        Activo = chbActivo.Checked,
                        Identificacion = _id,
                    };
                    int filasAfectadas = _clientesDAL.EditarCliente(datos);

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show($"Se edito el usuario con exito.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrio un error {ex}");
                }
            }
            else
            {
                try
                {                   
                    Clientes datos = new Clientes
                    {
                        Nombres = txtNombre.Text,
                        Apellidos = txtApellidos.Text,
                        Telefono = int.Parse(txtTelefono.Text),
                        Direccion = txtDireccion.Text,
                        Correo = txtCorreo.Text,
                        EstadoCivil = txtEstadoCivil.Text,
                        Activo = chbActivo.Checked,
                    };
                    int filasAfectadas = _clientesDAL.InsertarCliente(datos);

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Se agrego el usuario con exito.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrio un error {ex}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
