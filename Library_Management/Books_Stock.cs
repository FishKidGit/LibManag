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
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Diagnostics;


namespace Library_Management
{
    public partial class Books_Stock : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionLibrary"].ConnectionString);
             
        public Books_Stock()
        {
            Sucks1.Method1();
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Books_Stock_Load(object sender, EventArgs e)
        {
            Fill_Books_Info();
        }

        public void Fill_Books_Info()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT books_name, books_author_name,books_quantity,books_available_quantity FROM Books_Info";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i = dataGridView1.SelectedCells[0].Value.ToString();
            
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM Issue_Books where books_name ='"+ i.ToString() +"' AND books_return_date=''";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            con.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT  books_name, books_author_name,books_quantity,books_available_quantity FROM Books_Info WHERE books_name LIKE('%"+textBox1.Text+"%') ";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i = dataGridView2.SelectedCells[6].Value.ToString();
            textBox2.Text = i.ToString();
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587);
            smpt.EnableSsl = true;
            smpt.UseDefaultCredentials = false;
            smpt.Credentials = new NetworkCredential("fishkid1967@gmail.com", "xxxxx");

            MailMessage mail = new MailMessage("xxxx@gmail.com", textBox2.Text, "Book Return", textBox3.Text);
            mail.Priority = MailPriority.High;
            smpt.Send(mail);
            MessageBox.Show("Mail Sent");
            */
        }
    }
}
