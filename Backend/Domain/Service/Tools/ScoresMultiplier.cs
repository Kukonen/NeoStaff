using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Tools
{
  public class ScoresMultiplier
 	{
 		//Хардкод, по хорошему необходимо брать из API компании
 		const double COEFF_INNER_PROJECT = 1.2;
 		const double COEFF_IMPROVE_SKILLS = 1.1;
 		const double COEFF_COMPETITION = 1.1;
 		const double COEFF_PUBLIC_EVENT = 1.3;

 		public static int MultiplyScores(int scores, string type, double KPI = 0.8)
 		{
      double newScores = scores;

			//Множитель баллов за разную сферу активностей
			if (type == "start" || type == "end" || type == "endTestPeriod" || type == "careerDialog" || type == "rebuke")
			{
				newScores *= COEFF_INNER_PROJECT;
			}
			else if(type == "skills" || type == "learn" || type == "certification")
			{
				newScores *= COEFF_IMPROVE_SKILLS;
			}
			else if(type == "competition")
			{
				newScores *= COEFF_COMPETITION;
			} 
			else if(type == "event")
			{
				newScores *= COEFF_PUBLIC_EVENT;
			}

			//Множитель баллов за разную KPI работника
			newScores *= (0.8 + 0.2);

			return (int)Math.Ceiling(newScores);
		}
	}
}