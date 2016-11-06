using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class AuthorImpLinqManager : AuthorManager
    {
        private BookLibraryDataContext db;

        public AuthorImpLinqManager()
        {
            db = new BookLibraryDataContext();
        }

        public void Add(Author author)
        {
            db.Authors.InsertOnSubmit(author);
            db.SubmitChanges();

        }

        public void Delete(Author author)
        {
            db.Authors.DeleteOnSubmit(author);
            db.SubmitChanges();
        }

        public Author FindOne(long id)
        {
            return db.Authors.Where(author=> author.Id==id).FirstOrDefault();
        }

        public void Update(int id,Author author)
        {
            Author authorToSave = FindOne(id);
            authorToSave.Name = author.Name;
            authorToSave.LastName = author.LastName;
            db.SubmitChanges();

        }


    public IEnumerable<Author> FindAll()
        {

            return db.Authors.ToList();
        }
        
        ~AuthorImpLinqManager()
        {
            db.Dispose();
        }
    }
}