using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.ActivityInfo
{
	public class CompetitionActivityInfo
	{
        public string Place { get; set; }

        public string? Result { get; set; }

        public string Theme { get; set; }

        public int Role { get; set; }
    }
}
