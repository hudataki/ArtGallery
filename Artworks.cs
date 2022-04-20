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
using System.Drawing.Imaging;


namespace Art_Gallery
{
    public partial class Artworks : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());

        public Artworks()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)MSSQLLocalDB;AttachDbFilename=;Integrated Security=True;Connect Timeout=30");
        String imagelocation = "";
        SqlCommand com;
        private void btn_artist1_Click(object sender, EventArgs e)
        {
            Artist artist = new Artist();
            artist.Show();
            Hide();
        }

        private void btn_artwork1_Click(object sender, EventArgs e)
        {
            Artworks artwork = new Artworks();
            artwork.Show();
            Hide();
        }

        private void btn_cst1_Click(object sender, EventArgs e)
        {
            Customer1 customer1 = new Customer1();
            customer1.Show();
            Hide()
        ;
        }

        private void Artworks_Load(object sender, EventArgs e)
        {
            FillDGV();
          
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Files(*.jpg, *.png)|*.jpg, *.png| png Files(*.png)|*.png| jpeg files(*.jpg)|*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagelocation = dialog.FileName.ToString();
                picBox_artwork.ImageLocation = imagelocation;
            }

        }

        private void btn_add1_Click(object sender, EventArgs e)
        {
            try
            {

                MemoryStream ms = new MemoryStream();
                picBox_artwork.Image.Save(ms, picBox_artwork.Image.RawFormat);
                byte[] art_image = ms.ToArray();


                String query = "INSERT INTO ARTWORK VALUES(@ARTNO,@title,@Year_made,@type_art,@original_price,@selling_price,@art_style,@art_image,@txt_ssn3)";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@ARTNO", int.Parse(txt_artno.Text));
                com.Parameters.Add("@title", txt_title.Text);
                com.Parameters.Add("@year_made", txt_year.Text);
                com.Parameters.Add("@type_art", txt_arttype.Text);
                com.Parameters.Add("@original_price", int.Parse(txt_oprice.Text));
                com.Parameters.Add("@selling_price", int.Parse(txt_sprice.Text));
                com.Parameters.Add("@art_style", txt_artstyle.Text);
                com.Parameters.Add("@art_image", picBox_artwork).Value = art_image;
                com.Parameters.Add("@txt_ssn3", int.Parse(txt_artist.Text));


                if (con.State != ConnectionState.Open)
                    con.Open();
                com.ExecuteNonQuery();

                if (con.State != ConnectionState.Closed)
                    con.Close();


                MessageBox.Show("Record Added Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show("Please Check the Entered Values");
            }
        }
    
        private void btn_load_customer_Click(object sender, EventArgs e)
        {
            String query = "Select * From ARTWORK";
            DataTable td = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(td);
            dataGridView1.DataSource = td;


        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {

                string query = "DELETE FROM BOUGHTBY WHERE ARTNO = @txt_artno";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@txt_artno", int.Parse(txt_artno1.Text));
                com.Transaction = tran;

                string query2 = "DELETE FROM ARTWORK WHERE ARTNO = @txt_artno1";
                SqlCommand com2 = new SqlCommand(query2, con);
                com2.Parameters.Add("@txt_artno1", int.Parse(txt_artno1.Text));
                com2.Transaction = tran;

                if (con.State != ConnectionState.Open)
                    con.Open();

                com.ExecuteNonQuery();
                com2.ExecuteNonQuery();
                tran.Commit();

                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Artowrk Deleted Successfully");
            }

            catch (Exception msj)
            {
                MessageBox.Show("Select a Row to Delete");
            }


            con.Close();


        }
       
            public void FillDGV()
            {
                try
                {
                    SqlCommand com = new SqlCommand("SELECT * FROM ARTWORK", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable td = new DataTable();

                    adapter.Fill(td);

                    dataGridView1.AllowUserToResizeColumns = false;

                    dataGridView1.DataSource = td;
                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();

                    imageColumn = (DataGridViewImageColumn)dataGridView1.Columns[7];

                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowTemplate.Height = 70;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something Went Wrong!");
                }

            }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            dataGridView1.RowTemplate.Height = 60;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                if (dataGridView1.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)dataGridView1.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    break;
                }


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

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "UPDATE ARTWORK SET selling_price=@txt_price WHERE ARTNO=@txt_artno1";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@txt_price", int.Parse(txt_price.Text));
                com.Parameters.Add("@txt_artno1", int.Parse(txt_artno1.Text));

                if (con.State != ConnectionState.Open)
                    con.Open();

                com.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)
                    con.Close();
                MessageBox.Show("Artwork Info Updated successfully!");
            }
            catch (Exception msj)
            {
                MessageBox.Show("Select a Row to Update!");
            }
        }

        
        

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * ARTWORK WHERE ARTNO LIKE'%" + txt_search.Text + "%'";
            query += "OR title LIKE'%" + txt_search.Text + "%'";
            query += "OR Year_made LIKE'%" + txt_search.Text + "%'";
            query += "OR art_type LIKE'%" + txt_search.Text + "%'";
            query += "OR selling_price LIKE'%" + txt_search.Text + "%'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable td = new DataTable();
            adapter.Fill(td);
            dataGridView1.DataSource = td;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow SelectedRow = dataGridView1.Rows[e.RowIndex];

            txt_artist1.Text = SelectedRow.Cells[8].Value.ToString();
            txt_artno1.Text = SelectedRow.Cells[0].Value.ToString();
            txt_price.Text = SelectedRow.Cells[5].Value.ToString();
        }

        private void btn_load1_Click(object sender, EventArgs e)
        {
            String query = "Select * From BOUGHTBYY";
            DataTable td = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(td);
            dataGridView2.DataSource = td;

            int sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
            }
            txt_benefits.Text = sum.ToString();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_artno3_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            DataGridViewRow SelectedRow = dataGridView1.Rows[e.RowIndex];
          
        }

        private void txt_benefits_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_artist1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_artno1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
    
}
    
