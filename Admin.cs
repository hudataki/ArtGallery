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
    public partial class Admin : Form
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["mydbcon"].ToString());

        public Admin()
        {
            InitializeComponent();
        }

        private void btn_cst_Click(object sender, EventArgs e)
        {
            Artworks artworks = new Artworks();
            artworks.Show();
            Hide();

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
           
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_home_Click_1(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            Hide();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Text == "")
            {
                MessageBox.Show("Please Enter Correct Username and Password");
            }
            try
            {
                string query = "SELECT * FROM LOGIN WHERE username=@txt_username AND pass=@txt_password";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.Add("@txt_username", txt_username.Text);
                com.Parameters.Add("@txt_password", txt_password.Text);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet td = new DataSet();
                adapter.SelectCommand = com;
                adapter.Fill(td);
                con.Close();

                int count = td.Tables[0].Rows.Count;
                if (count == 1)
                {
                   

                    Artist artist = new Artist();
                    artist.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Please Enter Correct Username and Password");
                }
            }

            catch (Exception msj)
            {
                MessageBox.Show(msj.ToString());
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


        
