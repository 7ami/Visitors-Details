using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using visitors.VisitorsClasses;
//Second

namespace visitors
{
    public partial class Visitors : Form
    {
        public Visitors()
        {
            InitializeComponent();
        }
        contactclass c = new contactclass();

        private void Visitors_Load(object sender, EventArgs e)
        {   //Loading data on dgv
            DataTable dt = c.Select();
            dgv.DataSource = dt;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tbcontactId_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          

            if (tbFirstName.Text=="" || tbLastName.Text == ""|| tbcontactno.Text == ""|| cbGender.Text == ""|| tbAddress.Text == "")
            {
                MessageBox.Show("Please Enter all the information listed above before Adding.Thank You");

            }
            else
            {
                //getting data from input textbox
                c.FirstName = tbFirstName.Text;
                c.LastName = tbLastName.Text;
                c.ContactNo = tbcontactno.Text;  //error
                c.Email = tbemail.Text;
                c.Gender = cbGender.Text;
                c.Address = tbAddress.Text;


                //inserting data into database
               bool success = c.Insert(c);

                if (success == true)
                {
                    DataTable dt = c.Select();
                    dgv.DataSource = dt;
                    
                    //To clear the text box
                    clear();
                    MessageBox.Show("New Information Successfully Added");

                }
                else
                {
                    MessageBox.Show("Failed to add new information.Please try again");
                }
                //loading data on dgv
                



            }

          



        }
        //Static meter
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connecString"].ConnectionString;
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = tbSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tbl_visitors WHERE  FirstName LIKE '%"+keyword+ "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clear()
        {
            tbcontactId.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbemail.Text = "";
            tbcontactno.Text = "";
            tbAddress.Text = "";
            cbGender.Text = "";


        }

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            tbcontactId.Text = dgv.Rows[index].Cells[0].Value.ToString();
            tbFirstName.Text = dgv.Rows[index].Cells[1].Value.ToString();
            tbLastName.Text = dgv.Rows[index].Cells[2].Value.ToString();
            tbcontactno.Text = dgv.Rows[index].Cells[3].Value.ToString();
            tbemail.Text = dgv.Rows[index].Cells[4].Value.ToString();
            cbGender.Text = dgv.Rows[index].Cells[5].Value.ToString();
            tbAddress.Text = dgv.Rows[index].Cells[6].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {


            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbcontactno.Text == "" || cbGender.Text == "" || tbAddress.Text == "")
            {
                MessageBox.Show("Please Enter all the information listed above before Updating.Thank You");

            }
            else
            {

                //getting data from input textbox
                c.ContactId = int.Parse(tbcontactId.Text);
                c.FirstName = tbFirstName.Text;
                c.LastName = tbLastName.Text;
                c.ContactNo = tbcontactno.Text;  //error
                c.Email = tbemail.Text;
                c.Gender = cbGender.Text;
                c.Address = tbAddress.Text;


                //inserting data into database
                bool success = c.Update(c);
                if (success == true)
                {
                    
                    //To clear the text box
                    //loading data on dgv
                    DataTable dt = c.Select();
                    dgv.DataSource = dt;
                    clear();
                    MessageBox.Show("New Information Successfully Updated");


                }
                else
                {
                    MessageBox.Show("Failed to update new information.Please try again");
                }

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactId = Convert.ToInt32(tbcontactId.Text);
            bool fine = c.Delete(c);
            if(fine==true)
            {
                
                DataTable dt = c.Select();
                dgv.DataSource = dt;
                clear();
                MessageBox.Show("Information Successfully Deleted");

            }
            else
            {
                MessageBox.Show("Deletion Failed.Try Again");
            }
        }
    }
}
