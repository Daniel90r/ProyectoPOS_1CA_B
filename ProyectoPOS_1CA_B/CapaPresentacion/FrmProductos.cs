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
    public partial class FrmProductos : Form
    {
        //Lista estatica para simular la conexion a la BD
        private static List<Producto> listaProductos = new List<Producto>();
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            //carga inicial para poblar la lista con algunos productos
            if (!listaProductos.Any())
            {
                listaProductos.Add(new Producto
                {
                    Id = 1,
                    Nombre = "Café Pacamara",
                    Descripcion = "Importado",
                    Precio = 10.5m,
                    Stock = 100,
                    Estado = true
                });
                listaProductos.Add(new Producto
                {
                    Id = 2,
                    Nombre = "Capuccino",
                    Descripcion = "Galleta Oreo",
                    Precio = 10.5m,
                    Stock = 1,
                    Estado = true
                });
                listaProductos.Add(new Producto
                {
                    Id = 3,
                    Nombre = "Tres Leche",
                    Descripcion = "Leche de vaca",
                    Precio = 21.5m,
                    Stock = 20,
                    Estado = true
                });
            }
            //mandod a llmar el metodo para refrescar 
            RefrescarListaProductos();
            DeshabilitarBotones();
        }
        //metodo para deshabilitar los botones de editar y eliminar
        private void DeshabilitarBotones()
        {
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }
        //metodo para refrescar la lista de productos en el datagridview
        private void RefrescarListaProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listaProductos;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Validaciones basicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("EL nombre es obligartorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validaciones.EsDecimal(txtPrecio.Text))
            {
                MessageBox.Show("El precio debe ser un valor decimal.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validaciones.EsEntero(txtStock.Text))
            {
                MessageBox.Show("Stock invalido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Crear el nuevo producto
            int nuevoId = listaProductos.Any() ? listaProductos.Max(x => x.Id) + 1 : 1;
            var p = new Producto
            {
                Id = nuevoId,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text.Trim()),
                Stock = int.Parse(txtStock.Text.Trim()),
                Estado = chkEstado.Checked
            };
            listaProductos.Add(p);
            MessageBox.Show("Producto agregado exitosamente.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarListaProductos();
            //Limpiar los controles
            LimpiarCampos();
        }
        //metodo para limpiar los controles
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            chkEstado.Checked = false;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Mostrar los datos del producto seleccionado en los controles
            if (dgvProductos.CurrentRow == null) return;
            txtId.Text = dgvProductos.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            txtPrecio.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            txtStock.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            chkEstado.Checked =
                Convert.ToBoolean(dgvProductos.CurrentRow.Cells[5].Value);

            //Habilitar botones
            HabilitarBotones();
        }
        //metodo para habilitar los botones de editar y eliminar
        private void HabilitarBotones()
        {
            btnNuevo.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnLimpiar.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Evento para eliminar un producto
            if(!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un producto valido para eliminar.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var prod = listaProductos.FirstOrDefault(x => x.Id == id);
            if (prod==null)
            {
                MessageBox.Show("Producto no encontrado"); return;
            }
            if(MessageBox.Show("¿Esta seguro de eliminar el producto "+
                prod.Nombre+"?", "Confirmar", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listaProductos.Remove(prod);
                MessageBox.Show("Producto eliminado exitosamente.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefrescarListaProductos();
                LimpiarCampos();
                DeshabilitarBotones();
            }
        }
        //Evento para editar un producto
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //validar que el ID sea correcto
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Seleccione un producto valido para eliminar.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var prod = listaProductos.FirstOrDefault(x => x.Id == id);
            if (prod == null)
            {
                MessageBox.Show("Producto no encontrado"); return;
            }
            //Validaciones basicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("EL nombre es obligartorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validaciones.EsDecimal(txtPrecio.Text))
            {
                MessageBox.Show("El precio debe ser un valor decimal.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validaciones.EsEntero(txtStock.Text))
            {
                MessageBox.Show("Stock invalido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Actualizar el producto
            prod.Nombre = txtNombre.Text.Trim();
            prod.Descripcion = txtDescripcion.Text.Trim();
            prod.Precio = decimal.Parse(txtPrecio.Text.Trim());
            prod.Stock = int.Parse(txtStock.Text.Trim());
            prod.Estado = chkEstado.Checked;
            MessageBox.Show("Producto actualizado exitosamente.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefrescarListaProductos();
            LimpiarCampos();
            DeshabilitarBotones();
        }
    }
}
