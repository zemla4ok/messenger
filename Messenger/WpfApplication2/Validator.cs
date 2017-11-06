using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    static class Validator
    {
        public static bool ValidateMessage(string msg)
        {
            if (msg == "" || msg == "Write message...")
                return true;
            return false;
        }

        public static bool ValidTextBoxes(params string[] strings)
        {
            foreach (string i in strings)
                if (i == "")
                    return false;
            return true;
        }
        public static bool ValidRadio(params bool[] bools)
        {
            foreach (bool i in bools)
                if (i == true)
                    return true;
            return false;
        }
        public static bool CheckPassword(string pass1, string pass2)
        {
            if (pass1 != pass2)
                return false;
            else
                return true;
        }    
    }
}