using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    interface AuthorManager
    {
        void Add(Author author);
        void Delete(Author author);
        Author FindOne(long id);
        void Update(int id,Author author);
        IEnumerable<Author> FindAll();
    }
}
