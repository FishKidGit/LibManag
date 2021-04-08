using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Library_Management
{
    public partial class view_student_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SNf3PER;Initial Catalog=library_management;Integrated Security=True;Pooling=False");

        string wanted_path;
        string pwd;
        DialogResult result;

        public view_student_info()
        {
            InitializeComponent();
        }

        private void view_student_info_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                fillGrid();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fillGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            int i = 0;

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Student_Info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            Bitmap img;
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "Student Image";
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageCol.Width = 100;
            dataGridView1.Columns.Add(imageCol);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["student_image"].ToString() != "")
                {
                    img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                    dataGridView1.Rows[i].Cells[8].Value = img;
                    dataGridView1.Rows[i].Height = 100;
                }
                i = i + 1;
            }

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                int i = 0;

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Student_Info WHERE student_name LIKE ('%" + textBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                Bitmap img;
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.HeaderText = "Student Image";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView1.Columns.Add(imageCol);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["student_image"].ToString() != "")
                    {
                        img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                        dataGridView1.Rows[i].Cells[8].Value = img;
                        dataGridView1.Rows[i].Height = 100;
                    }
                    i = i + 1;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files(*.gif)|*.gif";
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (result == DialogResult.OK)
            {
                string img_path;
                int i;
                bool imageValid = true;
                pwd = Create_Password.GetRandomPassword(20);
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                try
                {
                    File.Copy(openFileDialog1.FileName, wanted_path + "\\Images_Students\\" + pwd + ".jpg");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    imageValid = false;

                }
                img_path = "Images_Students\\" + pwd + ".jpg";

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
             //   cmd.CommandText = "UPDATE * FROM Student_Info WHERE id =" + i + "";

                cmd.CommandText = "UPDATE Student_Info SET student_name = @StudentName, student_image = @StudentImage, student_enrolment_no = @StudentEnrolmentNo, student_department = @StudentDepartment, student_term = @StudentTerm, student_contact = @StudentContact, student_email = @StudentEMail WHERE id = " + i + "";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentName", student_name.Text);
                cmd.Parameters.AddWithValue("@StudentImage", img_path.ToString());
                cmd.Parameters.AddWithValue("@StudentEnrolmentNo", student_enrolment_no.Text);
                cmd.Parameters.AddWithValue("@StudentDepartment", student_departement.Text);
                cmd.Parameters.AddWithValue("@StudentTerm", student_term.Text);
                cmd.Parameters.AddWithValue("@StudentContact", student_contact.Text);
                cmd.Parameters.AddWithValue("@StudentEMail", student_email.Text);

                cmd.ExecuteNonQuery();
                fillGrid();
                con.Close();
                MessageBox.Show("Student Record Updated");
            }
            else if (result == DialogResult.Cancel)
            {
                int i;

                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //   cmd.CommandText = "UPDATE * FROM Student_Info WHERE id =" + i + "";

                cmd.CommandText = "UPDATE Student_Info SET student_name = @StudentName, student_enrolment_no = @StudentEnrolmentNo, student_department = @StudentDepartment, student_term = @StudentTerm, student_contact = @StudentContact, student_email = @StudentEMail WHERE id = " + i + "";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentName", student_name.Text);
          //      cmd.Parameters.AddWithValue("@StudentImage", img_path.ToString());
                cmd.Parameters.AddWithValue("@StudentEnrolmentNo", student_enrolment_no.Text);
                cmd.Parameters.AddWithValue("@StudentDepartment", student_departement.Text);
                cmd.Parameters.AddWithValue("@StudentTerm", student_term.Text);
                cmd.Parameters.AddWithValue("@StudentContact", student_contact.Text);
                cmd.Parameters.AddWithValue("@StudentEMail", student_email.Text);

                cmd.ExecuteNonQuery();
                fillGrid();
                con.Close();
                MessageBox.Show("Student Record Updated");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Student_Info WHERE id =" + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    student_name.Text = dr["student_name"].ToString();
                    student_enrolment_no.Text = dr["student_enrolment_no"].ToString();
                    student_departement.Text = dr["student_department"].ToString();
                    student_term.Text = dr["student_term"].ToString();
                    student_contact.Text = dr["student_contact"].ToString();
                    student_email.Text = dr["student_email"].ToString();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void student_enrolement_no_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
