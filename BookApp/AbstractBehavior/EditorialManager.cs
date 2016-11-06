using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.AbstractBehavior
{
    interface EditorialManager
    {
        void Add(Editorial editorial);
        void Delete(Editorial  editorial);
        Editorial FindOne(long id);
        void Update(int id, Editorial editorial);
        IEnumerable<Editorial> FindAll();
    }
}
