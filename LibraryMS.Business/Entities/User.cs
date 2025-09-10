using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public abstract class User
    {
        private Guid _guid = Guid.NewGuid();
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
