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
    public partial class Purchase : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());
        public Purchase()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            FillDGV("");
            DateTime idate = DateTime.Now;

            txt_date.Text = idate.ToString("yyyy-MM-dd HH:mm:ss");
            txt_qty.Text = "1";
        }
        public void FillDGV(string value)
        {
            string query = "SELECT ARTNO, title as 'Title', Year_made as 'Year', type_art as ' Art Type' ,selling_price as ' Price' , SSN, art_image as'Artwork' FROM ARTWORK WHERE type_art LIKE'%" + txt_search.Text + "%'";
            query += "OR title LIKE'%" + txt_search.Text + "%'";

            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable td = new DataTable();

            adapter.Fill(td);
            dataGridView1.RowTemplate.Height =60;
            dataGridView1.AllowUserToResizeColumns = false;
            
            dataGridView1.DataSource = td;
            dataGridView1.Columns["SSN"].Visible = false;
            dataGridView1.Columns["ARTNO"].Visible = false;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView1.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picBox_preview_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)


        {
            try
            {
                byte[] art_image = (byte[])dataGridView1.CurrentRow.Cells[6].Value;
                MemoryStream ms = new MemoryStream(art_image);

                picBox_preview.Image = Image.FromStream(ms);

                txt_artno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_totalprice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txt_ssn1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch(Exception msj)
            {
                MessageBox.Show(msj.ToString());
            }
        }

        private void txt_date_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (int.Parse(txt_qty.Text) > 1)
            {
                int qty;
                qty = int.Parse(txt_qty.Text);
                int price;
                price = int.Parse(txt_totalprice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString());

                int total;
                total = qty * price;
                txt_totalprice.Text = System.Convert.ToString(total);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_qty.Text = "1";
            txt_totalprice.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btn_purchase_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();


            try
            {
               
                

                DateTime idate = DateTime.Now;
                idate.ToString("yyyy-MM-dd HH:mm:ss");

                String query = "INSERT INTO BOUGHTBY VALUES (@SSN,@ARTNO,@purchase_date,@qty,@ssna)";
                

                SqlCommand com = new SqlCommand(query,con);
                com.Parameters.Add("@SSN", int.Parse(txt_ssn.Text));

                com.Parameters.Add("@ARTNO", int.Parse(txt_artno.Text));
                com.Parameters.Add("@purchase_date", DateTime.Parse(txt_date.Text));
                com.Parameters.Add("@qty", int.Parse(txt_qty.Text));
                com.Parameters.Add("@ssna", int.Parse(txt_ssn1.Text));
                com.Transaction = tran;

                
                String query1 = "UPDATE CUSTOMER SET total_dollar=total_dollar+@total_dollar"+" WHERE SSN=@SSN1";

                SqlCommand com1 = new SqlCommand(query1, con);
                com1.Parameters.Add("@total_dollar", int.Parse(txt_totalprice.Text));
                com1.Parameters.Add("@SSN1", int.Parse(txt_ssn.Text));

                com1.Transaction = tran;
                String query2 = "INSERT INTO BOUGHTBYY VALUES (@SSN2,@ARTNO1,@purchase_date1,@qty1,@ssna1,@price1)";

                SqlCommand com2 = new SqlCommand(query2, con);
                com2.Parameters.Add("@SSN2", int.Parse(txt_ssn.Text));

                com2.Parameters.Add("@ARTNO1", int.Parse(txt_artno.Text));
                com2.Parameters.Add("@purchase_date1", DateTime.Parse(txt_date.Text));
                com2.Parameters.Add("@qty1", int.Parse(txt_qty.Text));
                com2.Parameters.Add("@ssna1", int.Parse(txt_ssn1.Text));
                com2.Parameters.Add("@price1", int.Parse(txt_totalprice.Text));
                com2.Transaction = tran;




                if (con.State != ConnectionState.Open)
                    con.Open();

                com.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();
                tran.Commit();


                if (con.State != ConnectionState.Closed)
                    con.Close();
                MessageBox.Show("Order Done Successfully! Please Check Your Email for the Shipping Info");



            }

            catch (Exception msj){ 
           
            MessageBox.Show(msj.ToString());
               
            }
            finally
            {
                con.Close();
            }

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            Hide();

        }

        private void btn_cst_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            Hide();

        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            FillDGV(txt_search.Text);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_ssn1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
