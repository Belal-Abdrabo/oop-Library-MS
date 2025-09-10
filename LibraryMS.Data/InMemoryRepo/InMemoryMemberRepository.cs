using LibraryMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Data
{
    public class InMemoryMemberRepository : IMemberRepository
    {
        public void AddMember(LibraryMember member)
        {
            throw new NotImplementedException();
        }

        public List<LibraryMember> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public LibraryMember GetMemberById(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(LibraryMember member)
        {
            throw new NotImplementedException();
        }
    }
}
