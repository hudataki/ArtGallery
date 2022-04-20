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

namespace Art_Gallery
{
    public partial class Customer1 : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());

        public Customer1()
        {
            InitializeComponent();
        }

        private void btn_artist2_Click(object sender, EventArgs e)
        {
            Artist artist = new Artist();
            artist.Show();
            Hide();
        }

        private void btn_artwork2_Click(object sender, EventArgs e)
        {
            Artworks artwork = new Artworks();
            artwork.Show();
            Hide();

        }

        private void btn_cst2_Click(object sender, EventArgs e)
        {

            Customer1 customer1 = new Customer1();
            customer1.Show();
            Hide();
        }

        private void btn_add1_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "INSERT INTO CUSTOMER VALUES (@SSN,@first_name,@middle_name,@last_name,@address,@phone,@email,@total_dollar)";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@SSN", int.Parse(txt_ssn.Text));

                com.Parameters.Add("@first_name", txt_firstn.Text);
                com.Parameters.Add("@middle_name", txt_middlen.Text);
                com.Parameters.Add("@last_name", txt_lastn.Text);
                com.Parameters.Add("@address", txt_addr.Text);

                com.Parameters.Add("@phone", int.Parse(txt_phone.Text));
                com.Parameters.Add("@email", txt_email.Text);
                com.Parameters.Add("@total_dollar", int.Parse(txt_tds.Text));

                if (con.State != ConnectionState.Open)
                    con.Open();
                com.ExecuteNonQuery();

                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Customer Added Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show("Please Check the Values Entered");
            }

        }

        private void btn_load_customer_Click(object sender, EventArgs e)
        {

            String query = "Select * From CUSTOMER";
            DataTable td = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(td);
            dataGridView1.DataSource = td;
            txt_ssn1.Text = " ";
            txt_phone1.Text = " ";
            txt_email1.Text = " ";
            txt_address.Text = " ";

        }
        private void btn_home_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowIndex);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            DataGridViewRow SelectedRow = dataGridView1.Rows[e.RowIndex];

            txt_ssn1.Text = SelectedRow.Cells[0].Value.ToString();
            txt_phone1.Text = SelectedRow.Cells[5].Value.ToString();
            txt_email1.Text = SelectedRow.Cells[6].Value.ToString();
            txt_address.Text = SelectedRow.Cells[4].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE CUSTOMER SET phone=@txt_phone1, email=@txt_email1, address=@txt_address WHERE SSN = @txt_ssn1";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@txt_ssn1", int.Parse(txt_ssn1.Text));
                com.Parameters.Add("@txt_phone1", int.Parse(txt_phone1.Text));
                com.Parameters.Add("@txt_email1", txt_email1.Text);
                com.Parameters.Add("@txt_address", txt_address.Text);


                if (con.State != ConnectionState.Open)
                    con.Open();

                com.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)
                    con.Close();
                MessageBox.Show("Customer Info Updated successfully!");
            }


            catch (Exception msj)
            {

                MessageBox.Show(msj.ToString());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {

                string query = "DELETE FROM BOUGHTBY WHERE SSN = @txt_ssn";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@txt_ssn", int.Parse(txt_ssn1.Text));
                com.Transaction = tran;

                string query1 = "DELETE FROM CUSTOMER WHERE SSN=@txt_ssn1";
                SqlCommand com1 = new SqlCommand(query1, con);
                com1.Parameters.Add("@txt_ssn1", int.Parse(txt_ssn1.Text));
                com1.Transaction = tran;

                if (con.State != ConnectionState.Open)
                    con.Open();

                com.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                tran.Commit();

                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Customer Info Deleted Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show(msj.ToString());
            }


            con.Close();


        }


        private void btn_updateArtist_Click(object sender, EventArgs e)
        {

        }

        private void btn_load_artist_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM CUSTOMER WHERE SSN LIKE'%" + txt_search.Text + "%'";
                query += "OR first_name LIKE'%" + txt_search.Text + "%'";
                query += "OR last_name LIKE'%" + txt_search.Text + "%'";
                query += "OR middle_name LIKE'%" + txt_search.Text + "%'";
                query += "OR address LIKE'%" + txt_search.Text + "%'";
                query += "OR phone LIKE'%" + txt_search.Text + "%'";
                query += "OR email LIKE'%" + txt_search.Text + "%'";

                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable td = new DataTable();
                adapter.Fill(td);
                dataGridView1.DataSource = td;
            }
            catch (Exception)
            {
                MessageBox.Show("Something Went Wrong!");
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }


        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Customer1_Load(object sender, EventArgs e)
        {

        }
    }
}
   


