using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsbHopital
{
    public partial class AddCustomerAndOrder : Form
    {
        public AddCustomerAndOrder()
        {
            InitializeComponent();
            btnPlaceOrder.Enabled = false;
            
        }

        private void AddCustomerAndOrder_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == String.Empty)
            {
                MessageBox.Show("Write customer name please.", "Error");
                txtCustomerName.Focus();
                return;
            }

            if (txtCustomerName.Text.Length < 5)
            {
                MessageBox.Show("The client name must be at least 5 character", "Error");
                txtCustomerName.Focus();
                return;
            }
            // Success validate

            using (SqlCommand testCMD = Manager.SqlCommand("uspNewCustomer"))
            {

                testCMD.CommandType = CommandType.StoredProcedure;
                
                //SqlParameter RetVal = testCMD.Parameters.Add
                //    ("RetVal", SqlDbType.Int);
                // RetVal.Direction = ParameterDirection.ReturnValue;

                SqlParameter CustomerName = testCMD.Parameters.Add
                  ("@CustomerName", SqlDbType.VarChar, 40);
                CustomerName.Direction = ParameterDirection.Input;

                SqlParameter CustomerID = testCMD.Parameters.Add
                   ("@CustomerID", SqlDbType.VarChar, 11);
                CustomerID.Direction = ParameterDirection.Output;

                CustomerName.Value = txtCustomerName.Text;

                Manager.Connection.Open();
                testCMD.ExecuteNonQuery();
                Manager.Connection.Close();

               
                txtCustomerID.Text = CustomerID.Value.ToString();
                txtCustomerName.ReadOnly = true;
                btnCreateAccount.Enabled = false;
                btnPlaceOrder.Enabled = true;
                
            }
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnAddFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAnotherAccount_Click(object sender, EventArgs e)
        {

            txtCustomerName.Focus();
            txtCustomerName.ResetText();
            txtCustomerName.ReadOnly = false;
            txtCustomerID.ResetText();
            numOrderAmount.Value = 0;
            dtpOrderDate.ResetText();
            btnCreateAccount.Enabled = true;
            btnPlaceOrder.Enabled = false;

        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            using (SqlCommand testCMD = Manager.SqlCommand("uspPlaceNewOrders"))
            {

                testCMD.CommandType = CommandType.StoredProcedure;

               SqlParameter RetVal = testCMD.Parameters.Add
                   ("RetVal", SqlDbType.Int);
               RetVal.Direction = ParameterDirection.ReturnValue;

                SqlParameter Amount = testCMD.Parameters.Add
                  ("@Amount", SqlDbType.Int);
                Amount.Direction = ParameterDirection.Input;

                SqlParameter CustomerID = testCMD.Parameters.Add
                  ("@CustomerID", SqlDbType.Int);
                CustomerID.Direction = ParameterDirection.Input;

                SqlParameter OrderDate = testCMD.Parameters.Add
                  ("@OrderDate", SqlDbType.DateTime);
                OrderDate.Direction = ParameterDirection.Input;

                CustomerID.Value = txtCustomerID.Text;
                Amount.Value = numOrderAmount.Text;
                OrderDate.Value = dtpOrderDate.Value;

                Manager.Connection.Open();
                testCMD.ExecuteNonQuery();
                Manager.Connection.Close();


                txtCustomerID.Text = CustomerID.Value.ToString();
                btnAddFinish.Focus();
                if ((int)RetVal.Value == 0)
                    MessageBox.Show("Recorded order.", "Success");
                else
                    MessageBox.Show("Sorry there was a problem", "Sorry...");
                txtCustomerName.ReadOnly = true;
                btnCreateAccount.Enabled = false;
                btnPlaceOrder.Enabled = true;

            }
        }
    }
}
