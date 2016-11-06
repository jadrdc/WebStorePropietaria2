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
    public class GenreImpAdoManager :GenreManager
    {
        private static readonly string constr = ConfigurationManager.ConnectionStrings["BookLibraryConnectionString"].ConnectionString;
        private SqlConnection sqlConnection;

        public GenreImpAdoManager()
        {
            sqlConnection = new SqlConnection(constr);
            sqlConnection.Open();
        }


        public void Add(Genre genre)
        {
            SqlCommand sqlInsert = new SqlCommand("Insert_Genre");
            sqlInsert.CommandType = CommandType.StoredProcedure;
            sqlInsert.Connection = sqlConnection;
            sqlInsert.Parameters.AddWithValue("@Description", genre.Description);
            sqlInsert.ExecuteNonQuery();
        }


        public void Delete(Genre genre)
        {//Delete_Author

            try
            {
                SqlCommand sqlDelete = new SqlCommand("Delete_Genre");
                sqlDelete.CommandType = CommandType.StoredProcedure;
                sqlDelete.Connection = sqlConnection;
                sqlDelete.Parameters.AddWithValue("@Id", genre.Id);
                sqlDelete.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Genre FindOne(long id)
        {
            Genre genre = new Genre();

            try
            {

                SqlCommand sqlSelect = new SqlCommand("Select_Genre");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                sqlSelect.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    genre.Id = reader.GetInt32(0);
                    genre.Description = reader.GetString(1);

                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return genre;
        }

        public void Update(int id, Genre genre)
        {
            SqlCommand sqlUpdate = new SqlCommand("Update_Genre");
            sqlUpdate.CommandType = CommandType.StoredProcedure;
            sqlUpdate.Connection = sqlConnection;
            sqlUpdate.Parameters.AddWithValue("@Id", id);
            sqlUpdate.Parameters.AddWithValue("@Description", genre.Description);
            sqlUpdate.ExecuteNonQuery();

        }

        public IEnumerable<Genre> FindAll()
        {
            List<Genre> list = new List<Genre>();

            try
            {
                SqlCommand sqlSelect = new SqlCommand("Find_Genre");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    Genre genre = new Genre();
                    genre.Id = reader.GetInt32(0);
                    genre.Description = reader.GetString(1);
                    list.Add(genre);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }


            return list;
        }

        ~GenreImpAdoManager()
        {

        }

    }
}