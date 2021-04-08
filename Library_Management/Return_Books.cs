using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Library_Management
{
    public partial class Return_Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SNf3PER;Initial Catalog=library_management;Integrated Security=True;Pooling=False");

        public Return_Books()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            fillGrid(textBox1.Text);
        }

        public void fillGrid(string enrolmentNumber)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM Issue_Books WHERE student_enrolment_no = '" + enrolmentNumber.ToString() + "'AND books_return_date = ''";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Visible = true;
            int i = 0;

            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT * FROM Issue_Books WHERE id= "+i+"";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    lblBooksName.Text = dr["books_name"].ToString();
                    lblssueDate.Text = Convert.ToString(dr["books_issue_date"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "UPDATE Issue_Books SET books_return_date='"+ dateTimePicker1.Value.ToString()+"' WHERE Id ="+i+"";
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "UPDATE Books_Info SET books_available_quantity = books_available_quantity+1 WHERE books_name = '" + lblBooksName.Text + "'";

              //  cmd1.CommandText = "UPDATE Books_Info SET books_avaliable_quantity = books_avaliable_quantity+1 WHERE books_name ='"+ lblBooksName.Text+"'";
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Book Returned");
                //    panel2.Visible = false;
                //    panel3.Visible = false;
                con.Close();
                fillGrid(textBox1.Text);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
