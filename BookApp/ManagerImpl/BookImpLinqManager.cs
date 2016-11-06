using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class BookImpLinqManager : BookManager
    {
        private BookLibraryDataContext db;

        public BookImpLinqManager()
        {
            db = new BookLibraryDataContext();
        }

        public void Add(Book book)
        {
            db.Books.InsertOnSubmit(book);
            db.SubmitChanges();

        }

        public void Delete(Book book)
        {
            db.Books.DeleteOnSubmit(book);
            db.SubmitChanges();
        }

        public Book FindOne(long id)
        {
            return db.Books.Where(book => book.Id == id).FirstOrDefault();
        }

        public void Update(int id, Book book)
        {
            Book bookToSave = FindOne(id);
            bookToSave.Name = book.Name;
            bookToSave.ISBN = book.ISBN;
            bookToSave.Published_Date = book.Published_Date;
            bookToSave.Genre_Id = book.Genre_Id;
            bookToSave.Editorial_Id = book.Editorial_Id;
            bookToSave.Author_Id = book.Author_Id;
            db.SubmitChanges();

        }


        public IEnumerable<Book> FindAll()
        {

            return db.Books.ToList();
        }

        ~BookImpLinqManager()
        {
            db.Dispose();
        }
    }
}