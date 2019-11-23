using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visitors.VisitorsClasses
{
    class contactclass
    {
        //data carrier
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        //Static meter
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connecString"].ConnectionString;
        //Selecting data from Database

        public DataTable Select()
        {
            //1.Database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //2.Writing Sql Querry
                string sql = "SELECT * FROM tbl_visitors";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;



        }
        //Inserting data into database
        public bool Insert(contactclass c)
        {
            bool isSuccess = false;

            //1.Connect Databse
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //2.Creating sql query to insert data

                string sql = "INSERT INTO tbl_visitors (FirstName,LastName,ContactNo,Email,Gender,Address) VALUES(@FirstName,@LastName,@ContactNo,@Email,@Gender,@Address)";
                //sql cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                //Connection open
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }





            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;


        }

        public bool Update(contactclass u)
        {
            bool isFine = false;
            //Creating database connextion
            SqlConnection connec = new SqlConnection(myconnstrng);

            try
            {
                //sql query
                string sql = "UPDATE tbl_visitors SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Email=@Email, Gender=@Gender ,Address=@Address WHERE ContactId=@ContactId ";
                //creating cmd using sql and connec
                SqlCommand cmd = new SqlCommand(sql, connec);
                //creating parameters
                cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("@LastName", u.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", u.ContactNo);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Gender", u.Gender);
                cmd.Parameters.AddWithValue("@Address", u.Address);
                cmd.Parameters.AddWithValue("@ContactId", u.ContactId);

                //opening Connection 
                connec.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isFine = true;

                }
                else
                {
                    isFine = false;

                }



            }
            catch(Exception ex)
            {

            }
            finally
            {
                connec.Close();


            }

            return isFine;


        }


        public bool Delete(contactclass c)
        {
            bool isSuccess = false;
            
            SqlConnection connec = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_visitors WHERE ContactId=@ContactId";
                SqlCommand cmd = new SqlCommand(sql, connec);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                connec.Open();
                int check = cmd.ExecuteNonQuery();
                if(check>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }


            }
            catch(Exception ex)
            {

            }
            finally
            {
                connec.Close();

            }
            return isSuccess;



        }



             
            
        


              
            

    }
}
