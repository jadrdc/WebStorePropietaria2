using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class EditorialImpAdoManager : EditorialManager
    {
       private static readonly string constr = ConfigurationManager.ConnectionStrings["BookLibraryConnectionString"].ConnectionString;
        private SqlConnection sqlConnection;

        public EditorialImpAdoManager()
        {
            sqlConnection = new SqlConnection(constr);
            sqlConnection.Open();
        }

        
        public void Add(Editorial editorial)
        {
            SqlCommand sqlInsert = new SqlCommand("Insert_Editorial");
            sqlInsert.CommandType = CommandType.StoredProcedure;
            sqlInsert.Connection = sqlConnection;
            sqlInsert.Parameters.AddWithValue("@Name", editorial.Name);
            sqlInsert.ExecuteNonQuery();

        }

        public void Delete(Editorial editorial)
        {
            SqlCommand sqlInsert = new SqlCommand("Delete_Editorial");
            sqlInsert.CommandType = CommandType.StoredProcedure;
            sqlInsert.Connection = sqlConnection;
            sqlInsert.Parameters.AddWithValue("@Id", editorial.Id);
            sqlInsert.ExecuteNonQuery();

        }

        public Editorial FindOne(long id)
        {
            Editorial  editorial = new Editorial();

            try
            {

                SqlCommand sqlSelect = new SqlCommand("Select_Editorial");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                sqlSelect.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    editorial.Id = reader.GetInt32(0);
                    editorial.Name = reader.GetString(1);

                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }


            return editorial;
        }

        public void Update(int id, Editorial editorial)
        {

            SqlCommand sqlUpdate = new SqlCommand("Update_Editorial");
            sqlUpdate.CommandType = CommandType.StoredProcedure;
            sqlUpdate.Connection = sqlConnection;
            sqlUpdate.Parameters.AddWithValue("@Id", id);
            sqlUpdate.Parameters.AddWithValue("@Name", editorial.Name);
            sqlUpdate.ExecuteNonQuery();


        }


        public IEnumerable<Editorial> FindAll()
        {
            List<Editorial> list = new List<Editorial>();

            try
            {
                SqlCommand sqlSelect = new SqlCommand("Find_Editorial");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    Editorial editorial = new Editorial();
                    editorial.Id = reader.GetInt32(0);
                    editorial.Name = reader.GetString(1);
                    list.Add(editorial);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }


            return list;
        }

        ~EditorialImpAdoManager()
        {
         }






    }
}