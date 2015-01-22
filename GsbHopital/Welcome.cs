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
using System.Configuration;

namespace GsbHopital
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            
            // Initialisation des composants par default
            InitializeComponent();

            using (SqlCommand cmdAllCustomers = Manager.SqlCommand("SELECT * FROM [Customer] WHERE CustomerID = 1"))
                {
                    try
                    {
                        Manager.Connection.Open();
                        SqlDataReader reader = cmdAllCustomers.ExecuteReader();
                        while (reader.Read())
                        {
                            //label1.Text = (String)reader["CustomerID"].ToString();

                        }

                        reader.Close();
                        Manager.Connection.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Problème de connection à la base de donnée.");
                    }
 
                }

                Manager.Connection.Close();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void label2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A bientôt sur l'application GSB Hospital!", "A bientôt");
            Application.Exit();
        }

        private void btnGoToAdd_Click(object sender, EventArgs e)
        {
            AddCustomerAndOrder box = new AddCustomerAndOrder();
            box.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("© GSB Hopital - Tous droits reservés - 2015 Kévin Tirado", "Copyright");
        }

        private void bntGoToFillOrCancel_Click(object sender, EventArgs e)
        {
            FillOrCancelAnOrder box = new FillOrCancelAnOrder();
            box.Show();
        }
    }
}