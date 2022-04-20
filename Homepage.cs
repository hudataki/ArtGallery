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
    public partial class Homepage : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());
        public Homepage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show(); 
            Hide(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_s_arttype_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void lst_artwork_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btm_gopurchase_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Show();
            Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
