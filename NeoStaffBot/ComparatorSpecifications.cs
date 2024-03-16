using NeoStaffBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoStaffBot
{
    internal class ComparatorSpecifications
    {
        public static List<string> FindOldCertificated(EmployeeSpecification[] employeeSpecifications, DateOnly toodayDate)
        {
            List<string> employeesNames = new List<string>();


            if (employeeSpecifications == null)
            {
                return employeesNames;
            }

            foreach (var employeeSpecification in employeeSpecifications)
            {
                DateOnly specificationDate = employeeSpecification.LastCertification;
                string lastCertificationDate = employeeSpecification.LastCertification.ToString();

                if (lastCertificationDate.Equals("01/01/0001"))
                {
                    employeesNames.Add(string.Join(" ", "-", employeeSpecification.Surname, employeeSpecification.Name, employeeSpecification.Middlename,
                        "[ отсутствие аттестаций ]"));
                    continue;
                }

                // Получение разницы в месяцах
                int differenceInMonths = (toodayDate.Year - specificationDate.Year) * 12 + toodayDate.Month - specificationDate.Month;

                // Сравнение с разницей в один месяц
                if (differenceInMonths > 1)
                {
                    employeesNames.Add(string.Join(" ", "-", employeeSpecification.Surname, employeeSpecification.Name, employeeSpecification.Middlename,
                        "[ последняя аттестация более месяца назад:", lastCertificationDate, "]"));
                }
            }

            return employeesNames;
        }
    }
}
