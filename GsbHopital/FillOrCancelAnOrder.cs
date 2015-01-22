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
    public partial class FillOrCancelAnOrder : Form
    {
        public FillOrCancelAnOrder()
        {
            InitializeComponent();

            dataGridView.ColumnCount = 6;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView.Font, FontStyle.Bold);

            dataGridView.Columns[0].Name = "CustomerID";
            dataGridView.Columns[1].Name = "OrderID";
            dataGridView.Columns[2].Name = "OrderDate";
            dataGridView.Columns[3].Name = "FilleDate";
            dataGridView.Columns[4].Name = "Status";
            dataGridView.Columns[5].Name = "Amount";
            dataGridView.ReadOnly = true;
            
            
            btnCancelOrder.Enabled = false;
            btnFillOrder.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlCommand testCMD = Manager.SqlCommand("uspFillOrder"))
            {

                testCMD.CommandType = CommandType.StoredProcedure;

                SqlParameter OrderID = testCMD.Parameters.Add("@OrderID", SqlDbType.Int);
                OrderID.Direction = ParameterDirection.Input;

                SqlParameter FilledDate = testCMD.Parameters.Add("@FilledDate", SqlDbType.Date);

                FilledDate.Direction = ParameterDirection.Input;


                OrderID.Value = dataGridView.CurrentRow.Cells["OrderID"].Value;
                FilledDate.Value = dateTimePicker1.Value;

                Manager.Connection.Open();

                testCMD.ExecuteNonQuery();
                Manager.Connection.Close();

                this.Peuple();

            }
        }
        private void button2_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            using (SqlCommand testCMD = Manager.SqlCommand("uspCancelOrder"))
            {

                testCMD.CommandType = CommandType.StoredProcedure;

                SqlParameter OrderID = testCMD.Parameters.Add("@OrderID", SqlDbType.Int);
                OrderID.Direction = ParameterDirection.Input;

                OrderID.Value = dataGridView.CurrentRow.Cells["OrderID"].Value;

                Manager.Connection.Open();
                testCMD.ExecuteNonQuery();
                Manager.Connection.Close();

                this.Peuple();

            }
        }

        private void btnFindOrder_Click(object sender, EventArgs e)
        {

            this.Peuple();
            
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Peuple()
        {
            using (SqlCommand testCMD = Manager.SqlCommand("getOrdersById"))
            {

                testCMD.CommandType = CommandType.StoredProcedure;

                SqlParameter OrderID = testCMD.Parameters.Add("@OrderID", SqlDbType.Int);

                OrderID.Direction = ParameterDirection.Input;



                OrderID.Value = txtOrderID.Text;

                Manager.Connection.Open();

                SqlDataReader reader = testCMD.ExecuteReader();
                
                dataGridView.Rows.Clear();

                List<Order> Orders = new List<Order>();

                while (reader.Read())
                {
                    string[] row = { 
                                        reader["CustomerID"].ToString(), 
                                        reader["OrderID"].ToString(), 
                                        reader["OrderDate"].ToString(), 
                                        reader["FilledDate"].ToString(),
                                        reader["Status"].ToString(),
                                        reader["Amount"].ToString()
                                    };
                    Order order = new Order();
                    order.CustomerID = (Int32)reader["CustomerID"];
                    order.OrderID = (Int32)reader["OrderID"];
                    order.OrderDate = reader["OrderDate"].ToString();
                    order.Status = (String)reader["Status"];
                    order.Amount = (Int32)reader["Amount"];
                    order.FilledDate = reader["FilledDate"].ToString();
                    Orders.Add(order);
                    dataGridView.Rows.Add(row);
                }
                reader.Close();
                Manager.Connection.Close();

                if (Orders.Count > 0)
                {
                    btnCancelOrder.Enabled = true;
                    btnFillOrder.Enabled = true;
                    dateTimePicker1.Enabled = true;
                }
                else
                {
                    btnCancelOrder.Enabled = false;
                    btnFillOrder.Enabled = false;
                    dateTimePicker1.Enabled = false;
                }
                
                

            }
        }
    }
}
