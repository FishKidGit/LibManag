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

namespace Library_Management
{
    public partial class Issue_Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SNf3PER;Initial Catalog=library_management;Integrated Security=True;Pooling=False");

        public Issue_Books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Student_Info WHERE student_enrolment_no=" + txtEnrolmentNo.Text + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                i = Convert.ToInt32( dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Enrolment Number Not Found");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtStudentsName.Text = dr["student_name"].ToString();
                        txtStudentsDepartment.Text = dr["student_department"].ToString();
                        txtStudentsTerm.Text = dr["student_term"].ToString();
                        
                   //     dateTimePicker1.Value = Convert.ToDateTime(dr["books_purchase_date"].ToString());
                        txtStudentsContact.Text= dr["student_contact"].ToString();
                        txtStudentsEMail.Text = dr["student_email"].ToString();
                //        txtBooksName.Text = dr["books_price"].ToString();


                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Issue_Books_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
       //     con.Open();
        }

        private void txtStudentsDepartment_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBooksName_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;

            if (e.KeyCode != Keys.Enter)
            {
                listBox1.Items.Clear();

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Books_Info WHERE books_name LIKE ('%" + txtBooksName.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                if (count>0)
                {
                    listBox1.Visible = true;
                    foreach(DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
                con.Close();
            }
            

        }

        private void txtBooksName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBooksName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBooksName.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txtBooksName.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int bookQuantity = 0;

            con.Open();

            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT * FROM Books_Info WHERE books_name = '" + txtBooksName.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach(DataRow dr2 in dt2.Rows)
            {
                bookQuantity = Convert.ToInt32(dr2["books_available_quantity"].ToString());
            }

            if (bookQuantity > 0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "INSERT INTO Issue_Books (student_enrolment_no,student_name,student_department,student_term,student_contact,student_email,books_name,books_issue_date,books_return_date)" + " VALUES (@EnrolmentNo,@StudentName,@StudentDepartment,@StudentTerm,@StudentContact,@StudentEmail,@BooksName,@BooksIssueDate,@BooksReturnDate)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EnrolmentNo", txtEnrolmentNo.Text);
                cmd.Parameters.AddWithValue("@StudentName", txtStudentsName.Text);
                cmd.Parameters.AddWithValue("@StudentDepartment", txtStudentsDepartment.Text);
                cmd.Parameters.AddWithValue("@StudentTerm", txtStudentsTerm.Text);
                cmd.Parameters.AddWithValue("@StudentContact", txtStudentsContact.Text);
                cmd.Parameters.AddWithValue("@StudentEmail", txtStudentsEMail.Text);
                cmd.Parameters.AddWithValue("@BooksName", txtBooksName.Text);
                cmd.Parameters.AddWithValue("@BooksIssueDate", dateTimePicker1.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@BooksReturnDate", "");
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "UPDATE Books_Info SET books_available_quantity = books_available_quantity-1 WHERE books_name = '" + txtBooksName.Text + "'";
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Book Issued");
            }
            else
            {
                MessageBox.Show("No books avaliable");
            }
            con.Close();
        }
    }
}
