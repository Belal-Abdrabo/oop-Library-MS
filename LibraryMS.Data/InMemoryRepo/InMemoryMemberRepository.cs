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
        private List<LibraryMember> _members = new List<LibraryMember>()
        {
            new LibraryMember("John Doe"),
            new LibraryMember("Jane Smith")
        };
        public void AddMember(LibraryMember member)
        {
            if(member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null");
            }
            _members.Add(member);
        }

        public void DeleteMember(Guid memberId)
        {
            var memberTodelete = _members.FirstOrDefault(m => m.Id == memberId);
            memberTodelete.IsDeleted = true;
        }

        public List<LibraryMember> GetAllMembers()
        {
            return _members.Where(m => !m.IsDeleted).ToList();
        }

        public LibraryMember GetMemberById(Guid memberId)
        {
            return _members.FirstOrDefault(m => m.Id == memberId && !m.IsDeleted);
        }

        public void UpdateMember(LibraryMember member)
        {
            var memberToUpdate = _members.FirstOrDefault(m => m.Id == member.Id);
            if(memberToUpdate != null)
            {
                memberToUpdate.Name = member.Name;
                memberToUpdate.IsDeleted = member.IsDeleted;
            }

        }
    }
}
