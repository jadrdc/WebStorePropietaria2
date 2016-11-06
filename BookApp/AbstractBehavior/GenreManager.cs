using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.AbstractBehavior
{
    interface GenreManager
    {
        void Add(Genre genre);
        void Delete(Genre genre);
        Genre FindOne(long id);
        void Update(int id, Genre genre);
        IEnumerable<Genre> FindAll();
    }
}
