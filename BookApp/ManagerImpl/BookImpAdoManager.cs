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
    public class BookImpAdoManager : BookManager
    {
            private static readonly string constr = ConfigurationManager.ConnectionStrings["BookLibraryConnectionString"].ConnectionString;
        private SqlConnection sqlConnection;

        public BookImpAdoManager()
        {
            sqlConnection = new SqlConnection(constr);
            sqlConnection.Open();
        }


        public void Add(Book book)
        {
           

            SqlCommand sqlInsert = new SqlCommand("Insert_Book");
            sqlInsert.CommandType = CommandType.StoredProcedure;
            sqlInsert.Connection = sqlConnection;
            sqlInsert.Parameters.AddWithValue("@Name", book.Name);
            sqlInsert.Parameters.AddWithValue("@ISBN", book.ISBN);
            sqlInsert.Parameters.AddWithValue("@Genre", book.Genre_Id);
            sqlInsert.Parameters.AddWithValue("@Editorial", book.Editorial_Id);
            sqlInsert.Parameters.AddWithValue("@Published", book.Published_Date);
            sqlInsert.Parameters.AddWithValue("@Author", book.Author_Id);
            sqlInsert.ExecuteNonQuery();
        }


        public void Delete(Book book)
        {//Delete_Author

            try
            {
                SqlCommand sqlDelete = new SqlCommand("Delete_Book");
                sqlDelete.CommandType = CommandType.StoredProcedure;
                sqlDelete.Connection = sqlConnection;
                sqlDelete.Parameters.AddWithValue("@Id", book.Id);
                sqlDelete.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Book FindOne(long id)
        {
            Book book = new Book();

            try
            {

                SqlCommand sqlSelect = new SqlCommand("Select_Book");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                sqlSelect.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    book.Id = reader.GetInt32(0);
                    book.Name = reader.GetString(1);
                    book.ISBN = reader.GetString(2);
                    book.Published_Date = reader.GetDateTime(3);
                    book.Author_Id = reader.GetInt32(4);
                    book.Genre_Id = reader.GetInt32(5);
                    book.Editorial_Id = reader.GetInt32(6);
              }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return book;
        }

        public void Update(int id, Book book)
        {



            SqlCommand sqlUpdate = new SqlCommand("Update_Book");
            sqlUpdate.CommandType = CommandType.StoredProcedure;
            sqlUpdate.Connection = sqlConnection;
            sqlUpdate.Parameters.AddWithValue("@Id", id);
            sqlUpdate.Parameters.AddWithValue("@Name", book.Name);
            sqlUpdate.Parameters.AddWithValue("@ISBN", book.ISBN);
            sqlUpdate.Parameters.AddWithValue("@Published_Date", book.Published_Date);
            sqlUpdate.Parameters.AddWithValue("@Author", book.Author_Id);
            sqlUpdate.Parameters.AddWithValue("@Genre", book.Genre_Id);
            sqlUpdate.Parameters.AddWithValue("@Editorial",book.Editorial_Id);
            sqlUpdate.ExecuteNonQuery();

        }

        public IEnumerable<Book> FindAll()
        {
            List<Book> list = new List<Book>();
            GenreManager genreMa = new GenreImpAdoManager();
            AuthorManager authorMa = new AuthorImpAdoManager();
            EditorialManager editorial = new EditorialImpAdoManager();
            try
            {
                SqlCommand sqlSelect = new SqlCommand("Find_Book");
                sqlSelect.CommandType = CommandType.StoredProcedure;
                sqlSelect.Connection = sqlConnection;
                SqlDataReader reader = sqlSelect.ExecuteReader();


                while (reader.Read())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(1);
                    book.ISBN = reader.GetString(2);
                    book.Published_Date = reader.GetDateTime(3);
                    book.Author_Id = reader.GetInt32(4);
                    book.Genre_Id = reader.GetInt32(5);
                    book.Editorial_Id = reader.GetInt32(6);
                    book.Genre = genreMa.FindOne(book.Genre_Id);
                    book.Author = authorMa.FindOne(book.Author_Id);
                    book.Editorial = editorial.FindOne(book.Editorial_Id);
                    list.Add(book);
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }


            return list;
        }

        ~BookImpAdoManager()
        {

        }



    }
}