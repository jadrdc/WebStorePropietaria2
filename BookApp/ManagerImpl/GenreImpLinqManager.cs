using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.ManagerImpl
{
    public class GenreImpLinqManager : GenreManager
    {
        private BookLibraryDataContext db;

        public GenreImpLinqManager()
        {
            db = new BookLibraryDataContext();
        }

        public void Add(Genre genre)
        {
            db.Genres.InsertOnSubmit(genre);
            db.SubmitChanges();

        }

        public void Delete(Genre genre)
        {
            db.Genres.DeleteOnSubmit(genre);
            db.SubmitChanges();
        }

        public Genre FindOne(long id)
        {
            return db.Genres.Where(genre => genre.Id == id).FirstOrDefault();
        }

        public void Update(int id, Genre genre)
        {
            Genre genreToSave = FindOne(id);
            genreToSave.Description = genre.Description;
            db.SubmitChanges();

        }


        public IEnumerable<Genre> FindAll()
        {

            return db.Genres.ToList();
        }

        ~GenreImpLinqManager()
        {
            db.Dispose();
        }
    }
}