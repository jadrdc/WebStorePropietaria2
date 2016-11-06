using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.AbstractBehavior
{
    public interface BookManager
    {
        void Add(Book book);
        void Delete(Book book);
        Book FindOne(long id);
        void Update(int id, Book book);
        IEnumerable<Book> FindAll();
    }
}