using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBuild.Utilities;

namespace DevBuild.Assessment3_Question2 {
    class Program {
        public static string[] mainMenuOptions  = { "Display List of Persons", "Add Person", "Delete Person", "Exit" };
        public static List<Person> ourPeeps = new List<Person>() {  new Person("Jim", "Sterling", 38, "jsterling@yahoo.com"),
                                                                    new Person("Kenneth", "Alton", 43, "kenalton@aol.com"),
                                                                    new Person("Jennifer", "Redford", 36, "codingpro82@gmail.com"),
                                                                    new TeamMember("Satya", "Nadella", 51, "satyasmailbox@hotmail.com", 20000000.00)};

    static void Main(string[] args) {



            Console.Write("***********************************************************\n" +
              "*               Dev.Build(2.0) - My Peeps                 *\n" +
              "***********************************************************\n\n");
            while (true) {
                MenuHandling.PrintMenuOptions(mainMenuOptions, menuMode: true);
                var userSelection = UserInput.SelectMenuOption(mainMenuOptions.Length);

                switch (mainMenuOptions[userSelection - 1]) {
                    case "Display List of Persons": {

                            MenuHandling.PrintMenuOptions(ourPeeps.ToArray(),
                                                          messagePrompt: "First Name".PadRight(15) + "Last Name".PadRight(15) + "Age".PadRight(6) + "Email Address".PadRight(20) + "\n" +
                                                                            "==========".PadRight(15) + "=========".PadRight(15) + "===".PadRight(6) + "======================".PadRight(20));
                            break;
                        }
                    case "Add Person": AddPerson(ourPeeps);  break;
                    case "Delete Person": DeletePerson(ourPeeps);  break;
                    case "Exit": return;
                }
            }
        }

        public static void AddPerson(List<Person> masterList) {
            string userEntry = "";
            uint ageEntry = 0;
            double salaryEntry = 0.00;

            YesNoAnswer isTeamMember = UserInput.GetYesOrNoAnswer("Is this new person a Team Member? (y/n or yes/no. If so, we'll ask you for salary information): ");

            Person tmp = isTeamMember == YesNoAnswer.Yes ? new TeamMember() : new Person();

            tmp.FirstName = UserInput.PromptUntilValidEntry("Please enter new person's first name (first letter capitalized, 1-30 alphabetic characters): ", InformationType.Name);
            tmp.LastName = UserInput.PromptUntilValidEntry("Please enter new person's last name (1-30 alphabetic characters): ", InformationType.LastName);

            while (!uint.TryParse(userEntry, out ageEntry) || ageEntry < 0) {
                userEntry = UserInput.PromptUntilValidEntry("Please enter new person's age: ", InformationType.Numeric);
            }
            tmp.Age = ageEntry;
            if (!tmp.CheckIfAdult()) {
                Console.WriteLine("New people added to this list must be 18 or older (apparently we're having some kind of party.)");
                return;
            }
            else {
                //if we know the person is an adult and will be added to our list, then let's proceed
                tmp.EmailAddress = UserInput.PromptUntilValidEntry("Please enter new person's email address: ", InformationType.EmailAddress);

                if (tmp is TeamMember) {
                    userEntry = "";
                    while (!double.TryParse(userEntry, out salaryEntry) || salaryEntry < 0) {
                        userEntry = UserInput.PromptUntilValidEntry("Please enter new person's salary: $", InformationType.Numeric);
                    }
                    (tmp as TeamMember).Salary = salaryEntry;
                }
                masterList.Add(tmp);
            }
        }

        public static void DeletePerson(List<Person> masterList) {
            MenuHandling.PrintMenuOptions(masterList.ToArray(), menuMode: true, messagePrompt: $"Here's a list of our peeps. Press 1-{masterList.Count} to delete a person from the list, or {masterList.Count + 1} to go back:");
            Console.WriteLine($"{masterList.Count + 1}.) Go back");

            //let's allow for the possibility that this method will return one more than the length of the list, just so we have a "go back" option
            var userSelection = UserInput.SelectMenuOption(masterList.Count + 1);

            if (userSelection == masterList.Count + 1) { return; }

            YesNoAnswer yesNo = UserInput.GetYesOrNoAnswer("Are you sure? (y/n or yes/no): ");
            switch (yesNo) {
                case YesNoAnswer.Yes: masterList.RemoveAt((int)userSelection - 1); break;
                case YesNoAnswer.No: break;
                default: break;
            }

        }
    }
}
