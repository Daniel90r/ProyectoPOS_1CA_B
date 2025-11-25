using ProyectoPOS_1CA_B.CapaNegocio;
using ProyectoPOS_1CA_B.CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPOS_1CA_B.CapaPresentacion
{
    public partial class FrmCliente : Form
    {
        //VARIABLE PARA ALMACENAR EL ID DEL CLIENTE A MODIFICAR O ELIMINAR
        int clienteId = 0;
        //Creamos un objeto de la clase ClienteBLL para poder acceder a sus metodos
        ClienteBLL bll = new ClienteBLL();
        public FrmCliente()
        {
            InitializeComponent();
        }
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Limpiar();
        }
        void CargarDatos()
        {
            dgvClientes.DataSource = bll.Listar();
        }
        void Limpiar()
        {
            clienteId = 0;
            txtNombre.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtBuscar.Clear();
            chkEstado.Checked = true;
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente c = new Cliente
                {
                    Id = clienteId, // si es 0 INSERT y si es !=0 es UPDATE
                    Nombre = txtNombre.Text.Trim(),
                    Dui = txtDui.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Estado = chkEstado.Checked
                };
                //Llamamos al metodo Guardar de la BLL
                int id = bll.Guardar(c);
                MessageBox.Show("El cliente se ha guardado",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        
        /// METODO PARA LLENAR LOS CAMPOS DESDE EL  GRIDVIEW
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                clienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["Id"].Value);
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDui.Text = dgvClientes.Rows[e.RowIndex].Cells["Dui"].Value.ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(dgvClientes.Rows[e.RowIndex].Cells["Estado"].Value);
            }
        }
        //METODO PARA ELIMINAR UN CLIENTE
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(clienteId == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar");
                return;
            }
            if(MessageBox.Show("¿Está seguro de eliminar el cliente?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bll.Eliminar(clienteId);
                CargarDatos();
                Limpiar();
            }
        }
        //MANDO A LLAMAR EL METODO DE FILTRADO EN TIEMPO REAL
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bll.BUscar(txtBuscar.Text.Trim());
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
