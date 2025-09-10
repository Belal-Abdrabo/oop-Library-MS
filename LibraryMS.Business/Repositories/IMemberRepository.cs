using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public interface IMemberRepository
    {
        public LibraryMember GetMemberById(Guid memberId);
        public List<LibraryMember> GetAllMembers();
        public void AddMember(LibraryMember member);
        public void UpdateMember(LibraryMember member);
    }
}
