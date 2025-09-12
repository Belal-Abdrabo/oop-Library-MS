using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business.Entities
{
    public class LibraryCard
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime IssuedDate { get; set; }
        public decimal FineBalance { get; set; } // for fines or fees


        public LibraryCard(Guid ownerId)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;
            IsActive = true;
            IssuedDate = DateTime.Now;
            FineBalance = 0.0m; // initial fine balance is zero
        }
    }
}
