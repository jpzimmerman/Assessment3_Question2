using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild.Assessment3_Question2 {
    class TeamMember : Person {
        public double   Salary { get; set; }
        public string   PhysicalAddress { get; set; }

        public TeamMember() { }

        public TeamMember(string firstName, string lastName, uint age, string emailAddress, double salary) :
            base(firstName, lastName, age, emailAddress) {

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            EmailAddress = emailAddress;
            Salary = salary;
        }

        public override string ToString() {
            return base.ToString() + Salary.ToString("$#,###.00").PadLeft(18) + PhysicalAddress?.PadLeft(38);
        }

    }
}
