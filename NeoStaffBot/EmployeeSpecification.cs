using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoStaffBot.Model
{
    internal class EmployeeSpecification
    {

        public string ServiceNumber { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string Name { get; set; }
        public DateOnly LastSpecificationDate { get; set; }

    }
}
