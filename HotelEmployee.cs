using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_project
{
    internal class HotelEmployee
    {
        private double salary;
        public double Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if(value>0)
                {
                    salary = value;
                }
            }
        
        }
        public String FirstName{ get; set; }
        public String LastName { get; set; }

        public HotelEmployee(double salary, string firstName, string lastName)
        {
            Salary = salary;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {salary}";
        }
    }
}
