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
            _clientesDAL = new ClientesDAL();
            InitializeComponent();
            dgvClientes.DataSource = _clientesDAL.ObtenerClientes();
        }
    }
}
