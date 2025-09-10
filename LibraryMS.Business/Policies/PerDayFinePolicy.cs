using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class PerDayFinePolicy : IFinePolicy
    {
        public decimal CalculateFine(DateTime dueDate, DateTime returnedDate)
        {
            throw new NotImplementedException();
        }
    }
}
