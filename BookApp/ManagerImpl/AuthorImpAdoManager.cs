using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class AuthorImpAdoManager : AuthorManager
    {
        private static readonly string constr = ConfigurationManager.ConnectionStrings["BookLibraryConnectionString"].ConnectionString;
        private SqlConnection sqlConnection;
        public AuthorImpAdoManager()
        {
            sqlConnection = new SqlConnection(constr);
            sqlConnection.Open();
        }

        public void Add(Author author)
        {
            SqlCommand sqlInsert = new SqlCommand("Insert_Author");
            sqlInsert.CommandType = CommandType.StoredProcedure;
            sqlInsert.Connection = sqlConnection;
            sqlInsert.Parameters.AddWithValue("@LastName", author.LastName);
            sqlInsert.Parameters.AddWithValue("@Name", author.Name);
            sqlInsert.Parameters.AddWithValue("@hiredate", author.BirthDate);
            sqlInsert.ExecuteNonQuery();
        }

        public void Delete(Author author)
        {//Delete_Author

            try
            {
                SqlCommand sqlDelete = new SqlCommand("Delete_Author");
                sqlDelete.CommandType = CommandType.StoredProcedure;
                sqlDelete.Connection = sqlConnection;
                sqlDelete.Parameters.AddWithValue("@Id", author.Id);
                sqlDelete.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Author FindOne(long id)
        {
            Author author = new Author();

            try
            {

                SqlCommand sqlSelect = new SqlCommand("Select_Author");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                sqlSelect.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    author.Id = reader.GetInt32(0);
                    author.LastName = reader.GetString(2);
                    author.Name = reader.GetString(1);
                    author.BirthDate = reader.GetDateTime(3);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return author;
        }
        public void Update(int id, Author author)
        {

            SqlCommand sqlUpdate = new SqlCommand("Update_Author");
            sqlUpdate.CommandType = CommandType.StoredProcedure;
            sqlUpdate.Connection = sqlConnection;
            sqlUpdate.Parameters.AddWithValue("@Id", id);
            sqlUpdate.Parameters.AddWithValue("@Name", author.Name);
            sqlUpdate.Parameters.AddWithValue("@LastName", author.LastName);
            sqlUpdate.Parameters.AddWithValue("@hiredate", author.BirthDate);
            sqlUpdate.ExecuteNonQuery();

        }

        public IEnumerable<Author> FindAll()
        {
            List<Author> list = new List<Author>();

            try
            {
                SqlCommand sqlSelect = new SqlCommand("Find_Author");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    Author author = new Author();
                    author.Id = reader.GetInt32(0);
                    author.LastName = reader.GetString(2);
                    author.Name = reader.GetString(1);
                    author.BirthDate = reader.GetDateTime(3);
                    list.Add(author);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }


            return list;
        }
        ~AuthorImpAdoManager()
        {
        }
    }
}