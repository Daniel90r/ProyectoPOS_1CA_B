using ProyectoPOS_1CA_B.CapaDatos;
using ProyectoPOS_1CA_B.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPOS_1CA_B.CapaNegocio
{
    public class ClienteBLL
    {
        ClienteDAL dal = new ClienteDAL();
        //Creamos un objeto de la clase ClienteDAL para poder acceder a sus metodos

        //METODO PARA LISTAR TODOS LOS REGISTROS DE LA TABLA CLIENTE
        public DataTable Listar()
        {
            return dal.Listar();
            //La BLL no toca SQL, solo llama a los metodos de la DAL
        }
        //METODO PARA INSERTAR UN NUEVO REGISTRO DE LA TABLA CLIENTE
        public int Guardar(Cliente c)
        {
            //VALIDACIONES DE NEGOCIO
            if(string.IsNullOrWhiteSpace(c.Nombre))
                throw new Exception("El nombre del cliente es obligatorio.");
            //Si el Id es 0, es un nuevo registro INSERt
            if(c.Id == 0)
            {
                return dal.Insertar(c);
                MessageBox.Show("Registro insertado correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Si el Id trae un valor, es una actualizacion UPDATE
                dal.Actualizar(c);
                MessageBox.Show("Registro actualizado correctamente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return c.Id;
            }
        }
        //METODO PARA ELIMINAR UN REGISTRO DE LA TABLA CLIENTE
        public bool Eliminar(int id)
        {
            return dal.Eliminar(id);
            MessageBox.Show("Registro eliminado correctamente.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        //METODO PARA BUSCAR UN REGISTRO DE LA TABLA CLIENTE POR NOMBRE Y TELEFONO
        public DataTable BUscar(string filtro)
        {
              return dal.Buscar(filtro);
        }

    }
}
