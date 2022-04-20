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
    public partial class Artist : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());

        public Artist()

        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Artist_Load(object sender, EventArgs e)
        {
           
        }
       

        private void btn_add_artist_Click_1(object sender, EventArgs e)
        {
            try
            {
                String query = "INSERT INTO ARTIST VALUES (@SSN,@name,@birthplace,@age,@phone,@email,@art_style)";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@SSN", int.Parse(txt_ssn.Text));

                com.Parameters.Add("@name", txt_name.Text);
                com.Parameters.Add("@birthplace", txt_birthp.Text);
                com.Parameters.Add("@age", int.Parse(txt_age.Text));
                com.Parameters.Add("@phone", int.Parse(txt_phone.Text));
                com.Parameters.Add("@email", txt_email.Text);
                com.Parameters.Add("@art_style", txt_artstyle.Text);

                if (con.State != ConnectionState.Open)
                    con.Open();
                com.ExecuteNonQuery();

                if (con.State != ConnectionState.Closed)
                    con.Close();




                MessageBox.Show("Artist Added Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show("Please Check the Values Entered");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_load_artist_Click(object sender, EventArgs e)
        {
            String query = "Select * From ARTIST";
            DataTable td = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(td);
            dataGridView1.DataSource = td;
            txt_ssn1.Text = " ";
            txt_phone1.Text = " ";
            txt_email1.Text = " ";
        }

        private void btn_cst1_Click(object sender, EventArgs e)
        {
            Customer1 customer1 = new Customer1();
            customer1.Show();
            Hide();
        }

        private void btn_artwork1_Click(object sender, EventArgs e)
        {
            Artworks artwork = new Artworks();
            artwork.Show();
            Hide();
        }

        private void btn_artist1_Click(object sender, EventArgs e)
        {
            Artist artist = new Artist();
            artist.Show();
            Hide();
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

        private void btn_exit_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_load_customer_Click(object sender, EventArgs e)
        {

        }

        private void btn_updateArtist_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE ARTIST SET phone=@txt_phone1, email=@txt_email1 WHERE SSN = @txt_ssn1";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@txt_ssn1", int.Parse(txt_ssn1.Text));
                com.Parameters.Add("@txt_phone1", int.Parse(txt_phone1.Text));
                com.Parameters.Add("@txt_email1", txt_email1.Text);

                if (con.State != ConnectionState.Open)
                    con.Open();
                com.ExecuteNonQuery();

                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Artist Info Updated Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show(msj.ToString());
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                string query2 = "DELETE FROM BOUGHTBY WHERE SSN_artist= @txt_ssn2";

                SqlCommand com2 = new SqlCommand(query2, con);
                com2.Parameters.Add("@txt_ssn2", int.Parse(txt_ssn1.Text));
                com2.Transaction = tran;

                string query1 = "DELETE FROM ARTWORK WHERE SSN = @txt_ssn";
                SqlCommand com1 = new SqlCommand(query1, con);
                com1.Parameters.Add("@txt_ssn", int.Parse(txt_ssn1.Text));
                com1.Transaction = tran;

                string query = "DELETE FROM ARTIST WHERE SSN = @txt_ssn1";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@txt_ssn1", int.Parse(txt_ssn1.Text));
                com.Transaction = tran;

                if (con.State != ConnectionState.Open)
                    con.Open();
                com2.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                com.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Artist Info Deleted Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show(msj.ToString());
            }
            con.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM ARTIST WHERE SSN LIKE'%" + txt_search.Text + "%'";
                query += "OR Name LIKE'%" + txt_search.Text + "%'";
                query += "OR BirthPlace LIKE'%" + txt_search.Text + "%'";
                query += "OR art_style LIKE'%" + txt_search.Text + "%'";
                query += "OR phone LIKE'%" + txt_search.Text + "%'";
                query += "OR email LIKE'%" + txt_search.Text + "%'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable td = new DataTable();
                adapter.Fill(td);
                dataGridView1.DataSource = td;
            }
            catch(Exception msj)
            {
                MessageBox.Show("Something Went Wrong!");
            }
        }

        private void txt_ssn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_birthp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_age_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_artstyle_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;

            DataGridViewRow SelectedRow = dataGridView1.Rows[e.RowIndex];

            txt_ssn1.Text = SelectedRow.Cells[0].Value.ToString();
            txt_phone1.Text = SelectedRow.Cells[4].Value.ToString();
            txt_email1.Text = SelectedRow.Cells[5].Value.ToString();
        }

        private void txt_ssn1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_phone1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
    

