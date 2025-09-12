using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class PerDayFinePolicy : IFinePolicy
    {
        public decimal Fine { get; } = 1.0m; // Default fine amount per day



        public decimal CalculateFine(DateTime dueDate, DateTime returnedDate)
        {
            if (returnedDate <= dueDate)
            {
                return 0.0m; // No fine if returned on or before due date
            }

            return (returnedDate.Day - dueDate.Day) * Fine;
        }
    }
}