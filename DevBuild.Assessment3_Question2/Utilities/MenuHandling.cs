using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBuild.Utilities {
    class MenuHandling {

        public static void PrintMenuOptions(IList<object> menuOptions, [Optional]bool menuMode, [Optional]string messagePrompt) {
            Console.WriteLine();
            Console.Write(String.IsNullOrEmpty(messagePrompt) ? "" : (messagePrompt + "\n"));
            for (int i = 0; i < menuOptions.Count; i++) {
                Console.WriteLine((menuMode ? $"{i + 1}.) " : "") + menuOptions[i].ToString());
            }
        }
    }
}