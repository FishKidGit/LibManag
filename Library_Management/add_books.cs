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
    public partial class add_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SNf3PER;Initial Catalog=library_management;Integrated Security=True;Pooling=False");

        public add_books()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void add_books_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Books_Info (books_name, books_author_name,books_publication_name,books_purchase_date,books_price, books_quantity,books_available_quantity)" + " VALUES (@BooksName,@BooksAuthorName,@BooksPublicationName,@BooksPurchaseDate,@BooksPrice,@BooksQuantity,@BooksAvaliableQuantity)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@BooksName", textBox1.Text);
            cmd.Parameters.AddWithValue("@BooksAuthorName", textBox2.Text);
            cmd.Parameters.AddWithValue("@BooksPublicationName", textBox3.Text);
            cmd.Parameters.AddWithValue("@BooksPurchaseDate", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@BooksPrice", textBox5.Text);
            cmd.Parameters.AddWithValue("@BooksQuantity", textBox6.Text);
            cmd.Parameters.AddWithValue("@BooksAvaliableQuantity", textBox6.Text);

            cmd.Connection = con;
            con.Open();
            try
            {
                int aff = cmd.ExecuteNonQuery();
                MessageBox.Show("Book added.");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            checkNumeric(sender, e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumeric(sender, e);
        }

        private void checkNumeric(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
