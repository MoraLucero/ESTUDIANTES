using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Data.SqlClient;

namespace ESTUDIANTES
{
    public partial class FrmEstudiante : Form
    {
        public FrmEstudiante()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("Conexion con Base de Datos exitosa");

            dataGridView1.DataSource = llenar_Grid();
        }

        public DataTable llenar_Grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM ALUMNO";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }

        // Evento boton Agregar
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO ALUMNO(CODIGO,NOMBRES,APELLIDOS,CARRERA)VALUES(@CODIGO,@NOMBRES,@APELLIDOS,@CARRERA)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());

            cmd1.Parameters.AddWithValue("@CODIGO", txtCodigo.Text);
            cmd1.Parameters.AddWithValue("@NOMBRES", txtNombres.Text);
            cmd1.Parameters.AddWithValue("@APELLIDOS", txtApellidos.Text);
            cmd1.Parameters.AddWithValue("@CARRERA", txtCarrera.Text);

            cmd1.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron agregados con exito");
            dataGridView1.DataSource = llenar_Grid();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCodigo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtNombres.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtApellidos.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtCarrera.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            { }
        }
        // Evento boton Modificar
        private void button3_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizar = "UPDATE ALUMNO SET CODIGO=@CODIGO, NOMBRES=@NOMBRES,APELLIDOS=@APELLIDOS,CARRERA=@CARRERA WHERE CODIGO=@CODIGO";
            SqlCommand cmd2 = new SqlCommand(actualizar, Conexion.Conectar());

            cmd2.Parameters.AddWithValue("@CODIGO", txtCodigo.Text);
            cmd2.Parameters.AddWithValue("@NOMBRES", txtNombres.Text);
            cmd2.Parameters.AddWithValue("@APELLIDOS", txtApellidos.Text);
            cmd2.Parameters.AddWithValue("@CARRERA", txtCarrera.Text);

            cmd2.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron actualizados con exito");
            dataGridView1.DataSource = llenar_Grid();
        }
        // Evento boton Eliminar
        private void button4_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "DELETE FROM ALUMNO WHERE CODIGO = @CODIGO";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());

            cmd3.Parameters.AddWithValue("@CODIGO", txtCodigo.Text);

            cmd3.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron eliminados con exito");
            dataGridView1.DataSource = llenar_Grid();
        }
        // Evento boton Nuevo
        private void button2_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCarrera.Clear();

            txtCodigo.Focus();
        }
        // Icono Cerrar
        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("User32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.dll", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam,int lParam);
        // Mover ventana
        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // DOCENTES
        private void button5_Click(object sender, EventArgs e)
        {
            FrmDocente Docente = new FrmDocente();
            Docente.ShowDialog(this);
        }
    }
}
