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
    public partial class FrmDocente : Form
    {
        public FrmDocente()
        {
            InitializeComponent();
        }
        public DataTable llenar_Grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM DOCENTE";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtIdDocente.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtNOMBRES.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAPELLIDOS.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTitulo_Academico.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            { }
        }

        //NUEVO
        private void button3_Click(object sender, EventArgs e)
        {
            txtIdDocente.Clear();
            txtNOMBRES.Clear();
            txtAPELLIDOS.Clear();
            txtTitulo_Academico.Clear();

            txtIdDocente.Focus();
        }
        //AGREGAR
        private void button4_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO DOCENTE(IdDocente,NOMBRES,APELLIDOS,Titulo_Academico)VALUES(@IdDocente,@NOMBRES,@APELLIDOS,@Titulo_Academico)";
            SqlCommand cmd11 = new SqlCommand(insertar, Conexion.Conectar());

            cmd11.Parameters.AddWithValue("@IdDocente", txtIdDocente.Text);
            cmd11.Parameters.AddWithValue("@NOMBRES", txtNOMBRES.Text);
            cmd11.Parameters.AddWithValue("@APELLIDOS", txtAPELLIDOS.Text);
            cmd11.Parameters.AddWithValue("@Titulo_Academico", txtTitulo_Academico.Text);

            cmd11.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron agregados con exito");
            dataGridView1.DataSource = llenar_Grid();
        }

        private void FrmDocente_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("Conexion con Base de Datos exitosa");

            dataGridView1.DataSource = llenar_Grid();
        }
        // MODIFICAR
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizar = "UPDATE DOCENTE SET IdDocente=@IdDocente, NOMBRES=@NOMBRES,APELLIDOS=@APELLIDOS,Titulo_Academico=@Titulo_Academico WHERE IdDocente=@IdDocente";
            SqlCommand cmd22 = new SqlCommand(actualizar, Conexion.Conectar());

            cmd22.Parameters.AddWithValue("@IdDocente", txtIdDocente.Text);
            cmd22.Parameters.AddWithValue("@NOMBRES", txtNOMBRES.Text);
            cmd22.Parameters.AddWithValue("@APELLIDOS", txtAPELLIDOS.Text);
            cmd22.Parameters.AddWithValue("@Titulo_Academico", txtTitulo_Academico.Text);

            cmd22.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron actualizados con exito");
            dataGridView1.DataSource = llenar_Grid();
        }
        //ELIMINAR
        private void button2_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "DELETE FROM DOCENTE WHERE IdDocente = @IdDocente";
            SqlCommand cmd33 = new SqlCommand(eliminar, Conexion.Conectar());

            cmd33.Parameters.AddWithValue("@IdDocente", txtIdDocente.Text);

            cmd33.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron eliminados con exito");
            dataGridView1.DataSource = llenar_Grid();
        }
    }
}
