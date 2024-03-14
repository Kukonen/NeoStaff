using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum
{
	enum Role
	{
		Participant = 1,
		Organizer = 2,
		Jury = 3,
		Speaker = 4
	}

	enum Result
	{
		Hired = 1,
		Fired = 2,
		Extended = 3
	}

	enum DialogResult
	{
		Positive = 1,
		Negative = 2,
		Neutral = 3
	}
}
