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
    public partial class Customer : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());

        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            Hide();
        }

        private void btn_cst1_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            Hide();
        }

        private void btn_admin1_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Hide();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "INSERT INTO CUSTOMER VALUES (@SSN,@first_name,@middle_name,@last_name,@address,@phone,@email,0)";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@SSN", int.Parse(txt_ssn.Text));

                com.Parameters.Add("@first_name",txt_firstn.Text);
                com.Parameters.Add("@middle_name", txt_middlen.Text);
                com.Parameters.Add("@last_name", txt_lastn.Text);
                com.Parameters.Add("@address", txt_addr.Text); 

                com.Parameters.Add("@phone", int.Parse(txt_phone.Text));
                com.Parameters.Add("@email",txt_email.Text);


                if (con.State != ConnectionState.Open)
                    con.Open();
                com.ExecuteNonQuery();

                if (con.State != ConnectionState.Closed)
                    con.Close();

                MessageBox.Show("Record Added Successfully"); 
            }
             
            catch (Exception msj)
            {
                MessageBox.Show("Please Check the Values Entered");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_lastn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_middlen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_firstn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ssn_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_addr_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
