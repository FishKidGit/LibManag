using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace Library_Management
{
    public partial class Add_Student_Info : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionLibrary"].ConnectionString);
 
        string wanted_path;

        public Add_Student_Info()
        {
            InitializeComponent();
            clearForm(Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            DialogResult result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files(*.gif)|*.gif";
            if (result == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pwd = Create_Password.GetRandomPassword(20);
            string img_path;
            
            if (string.IsNullOrEmpty(textBox1.Text.ToString()))
            {
                MessageBox.Show("You must enter at least a name to add a student.");
                return;
            }

            if (string.IsNullOrEmpty(openFileDialog1.FileName.ToString())) // No image so use default blank image
            {
                wanted_path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                openFileDialog1.FileName = wanted_path + "\\Images\\blanksil.png";
            }

            File.Copy(openFileDialog1.FileName, wanted_path + "\\Images_Students\\" + pwd + ".jpg");

            img_path = "Images_Students\\" + pwd +".jpg";
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Student_Info (student_name,student_image,student_enrolment_no,student_department,student_term,student_contact,student_email)" + " VALUES (@Studentname,@StudentImage,@StudentEnrolmentNo,@StudentDepartment,@StudentTerm,@StudentContact,@StudentEmail)";
 
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StudentName", textBox1.Text);
            cmd.Parameters.AddWithValue("@StudentImage", img_path.ToString());
            cmd.Parameters.AddWithValue("@StudentEnrolmentNo", textBox2.Text);
            cmd.Parameters.AddWithValue("@StudentDepartment", textBox3.Text);
            cmd.Parameters.AddWithValue("@StudentTerm", textBox4.Text);
            cmd.Parameters.AddWithValue("@StudentContact", textBox5.Text);
            cmd.Parameters.AddWithValue("@StudentEmail", textBox6.Text);
            cmd.Connection = con;
            
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student added.");
                clearForm(Controls);
            }
            catch
            {
                MessageBox.Show("Error encountered during INSERT operation.");
            }
            finally
            {
                con.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void clearForm(Control.ControlCollection controls)
        {
            ClearTextBoxes(controls);
            openFileDialog1.FileName = "";

            pictureBox1.ImageLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Images\\blanksil.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void ClearTextBoxes(Control.ControlCollection controls)
        {
            foreach (TextBox tb in controls.OfType<TextBox>())
                tb.Text = string.Empty;
            foreach (Control c in controls)
                ClearTextBoxes(c.Controls);
        }
    } 
}
