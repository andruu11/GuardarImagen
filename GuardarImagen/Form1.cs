using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardarImagen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MostrarDatos();
        }
        conexion conexion_bd = new conexion();
        string nombre_imagen;
        string ruta_imagen;
        OpenFileDialog cuadro_dialogo = new OpenFileDialog();
        private void btn_abrir_Click(object sender, EventArgs e)
        {
            cuadro_dialogo.Title = "Abrir";
            cuadro_dialogo.Filter = "Jpg files (*.jpg)|*.jpg|Gif files (*.gif)|*.gif|PNG files (*.png)|*.png";
            if(cuadro_dialogo.ShowDialog() == DialogResult.OK){
                ruta_imagen = cuadro_dialogo.FileName;
                Bitmap imagen = new Bitmap(ruta_imagen);
                pictureBox1.Image = (Image)imagen;
                nombre_imagen = cuadro_dialogo.SafeFileName;

                textBox1.Text = nombre_imagen;
                
                textBox2.Text = ruta_imagen;
                textBox2.Text = textBox2.Text.Replace("\\","\\\\");
               
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            //boton para agregar persona

            string consulta_agregar = "INSERT INTO imagen VALUES ('" + "" + "','" + textBox1.Text + "','" + textBox2.Text.Trim() + "')";
            if (conexion_bd.InsertarDatos(consulta_agregar))
            {
                MessageBox.Show("Datos insertados");
                MostrarDatos();
               
            }
            else
            {
                MessageBox.Show("Datos no insertados");
            }
        }

        public void MostrarDatos()
        {
            //metodo para mostrar datos
            conexion_bd.consulta("SELECT * FROM imagen", "imagen");
            dataGridView1.DataSource = conexion_bd.ds.Tables["imagen"];
            
            //borrar linea 35



        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            //forma para obtener los registros en los campos al dar click sobre un registro
            DataGridViewRow registro = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = registro.Cells["nombre_imagen"].Value.ToString();
            textBox2.Text = registro.Cells["ruta_imagen"].Value.ToString();
            string ruta_imagen_m = textBox2.Text;
            pictureBox2.ImageLocation = @ruta_imagen_m.Trim();
            
           
             
        }
    }
}
