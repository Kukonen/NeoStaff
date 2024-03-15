using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
	public class Activity<T>
	{
        public DateOnly Date { get; set; }

        public string Note { get; set; }

        public int Mark { get; set; }

        public string Type { get; set; }

        public int Salary { get; set; }

        public T ActivityInfo { get; set; }
    }
}
