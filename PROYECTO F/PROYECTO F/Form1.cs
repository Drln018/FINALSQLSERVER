using PROYECTO_F.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_F
{
    public partial class Form1 : Form
    {
        //crear objeto
        ClsConexion con = new ClsConexion();
        int Id;
        Boolean editar;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            editar = false;
            this.ActualizarGrid();
        }

        public void ActualizarGrid()
        {
            con.ActualizarGrid(this.dataGridView1, "Select * from tabla_final");
        }

        public void Limpiar()
        {
            
            textBoxNombre.Text = "";
            textBoxMarca.Text = "";
            textBoxFecha.Text = "";
            textBoxCantidad.Text = "";
            textBoxPrecio.Text = "";
            textBoxTotal.Text = "";
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            //if para
            if (editar)
            {
                con.Conectar();
                string consulta = "update tabla_final set Nombre='" + textBoxNombre.Text + "',Marca='" + textBoxMarca.Text + "',Fecha='" + textBoxFecha.Text + "',Cantidad='" + textBoxCantidad.Text + "',PrecioUnidad='" + textBoxPrecio.Text + "',Total='" + textBoxTotal.Text + "'Where Id ='" +Id+"'";
                con.EjecutarSql(consulta);
                this.ActualizarGrid();
                con.Desconectar();
                this.Limpiar();
                editar = false; //poder hacer la edicion del dato
            }
            else
            {
                con.Conectar();
                //crear la consulta
                string consulta = "Insert into tabla_final (Nombre, Marca, Fecha, Cantidad, PrecioUnidad, Total) values ('"  + textBoxNombre.Text + "','" + textBoxMarca.Text + "','" + textBoxFecha.Text + "','" + textBoxCantidad.Text + "','" + textBoxPrecio.Text + "','" + textBoxTotal.Text + "');";

                con.EjecutarSql(consulta);
                //llamamos el metodo
                this.ActualizarGrid();
                con.Desconectar();

                this.Limpiar();
            }
        }

        private void buttonTotal_Click(object sender, EventArgs e)
        {
            int numCantidad = Convert.ToInt32(textBoxCantidad.Text);
            int numPrecio = Convert.ToInt32(textBoxPrecio.Text);
            int result = numCantidad * numPrecio;
            textBoxTotal.Text = result.ToString();
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            editar = true;
            Id= int.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBoxNombre.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxMarca.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxFecha.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCantidad.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBoxPrecio.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBoxTotal.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void ButtonEiminar_Click(object sender, EventArgs e)
        {
            Id = int.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());

            var result = MessageBox.Show("¿Desea eliminar el registro?","Confirme lo borrado", MessageBoxButtons.YesNo);

            if(result== DialogResult.Yes)
            {
                con.Conectar();
                string consulta = "delete from tabla_final where Id= '" + Id + "';";

                con.EjecutarSql(consulta);
                //actualizar el dbgrid
                this.ActualizarGrid();
                con.Desconectar();
            }
            else
            {
                return;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            con.ActualizarGrid(this.dataGridView1, "select * from tabla_final where Nombre like '" + textBox1.Text + "%';") ;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            con.ActualizarGrid(this.dataGridView1, "select * from tabla_final where Fecha like '" + textBox2.Text + "%';");
        }
    }
}
