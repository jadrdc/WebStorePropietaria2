using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class EditorialImpLinqManager : EditorialManager
    {
      private BookLibraryDataContext db;

        public EditorialImpLinqManager()
        {
            db = new BookLibraryDataContext();
        }

        public void Add(Editorial editorial)
        {
            db.Editorials.InsertOnSubmit(editorial);
            db.SubmitChanges();

        }

        public void Delete(Editorial editorial)
        {
            db.Editorials.DeleteOnSubmit(editorial);
            db.SubmitChanges();
        }

        public Editorial FindOne(long id)
        {
            return db.Editorials.Where(editorial => editorial.Id == id).FirstOrDefault();
        }

        public void Update(int id, Editorial editorial)
        {
            Editorial genreToSave = FindOne(id);
            genreToSave.Name = editorial.Name;
            db.SubmitChanges();

        }


        public IEnumerable<Editorial> FindAll()
        {

            return db.Editorials.ToList();
        }

        ~EditorialImpLinqManager()
        {
            db.Dispose();
        }
    }
}