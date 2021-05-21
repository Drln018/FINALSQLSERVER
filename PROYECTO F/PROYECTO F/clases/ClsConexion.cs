using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace PROYECTO_F.clases
{
    class ClsConexion
    {
         SqlConnection conex;
        public void Conectar()
        {
            conex = new SqlConnection("Data Source=(Localdb)\\Darlin;Initial Catalog=basedatos_final;Integrated Security=True");
            conex.Open();
        }

        public void Desconectar()
        {
            conex.Close();
        }
        // recibe la consulta que se ejecutara
        public void EjecutarSql(string consulta)
        {
            //se crea el objeto que contenerá la consulta
            SqlCommand com = new SqlCommand(consulta, conex);
            //ejecuta la consulta y tra las filas de la base de datos
            int filasAfectadas = com.ExecuteNonQuery();
            if (filasAfectadas > 0)
                MessageBox.Show("Operacion realizada correctamente", "La base de datos ha sido modificada", MessageBoxButtons.OK);
            else
                MessageBox.Show("No se conectó a la base de datos", "Error del sitema");

        }

        public void ActualizarGrid(DataGridView dg, string consulta)
        {
            //hacemos referencia al metodo conectar
            this.Conectar();
            //Data set ayuda para llenar los datos del grid 
            System.Data.DataSet ds = new System.Data.DataSet();

            //DataAdapter adaptará los datos
            SqlDataAdapter da = new SqlDataAdapter(consulta, conex);
          
            //Llenar el dataset
            //Fill opcion de llenado de tabla
            da.Fill(ds, "tabla_final");
            //asignamos valores y propiedades
            dg.DataSource = ds;
            dg.DataMember = "tabla_final";

            this.Desconectar();
        }
    }
}
