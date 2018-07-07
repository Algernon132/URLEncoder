/*Charles Bruscato
 * July 6th, 2018
 * Musser IT 2001
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            bool again = true;
            var lib = new replace();    //initialize our library
            string projName;
            string actName;


            do
            {
                Console.WriteLine("What is the project name?");
                projName = Console.ReadLine();

                Console.WriteLine("\nWhat is the activity name?");
                actName = Console.ReadLine();

                projName = lib.MakeSafe(projName);
                actName = lib.MakeSafe(actName);

                if(projName == null || actName == null) //if either string was found to have a control character by MakeSafe
                {
                    Console.WriteLine("An invalid control character was detected.");
                }
                else
                {
                    string safeURL = "https://companyserver.com/content/" + projName + "/files/" + actName + "/" + actName + "Report.pdf";
                    Console.WriteLine(safeURL);
                }

                Console.WriteLine("Would you like to create a URL again?\nY for yes, any other key for no.");
                string againString = Console.ReadLine();
                if(againString != "Y")
                {
                    again = false;
                }

            } while (again == true);

        }
    }

    class replace
    {
        Dictionary<char, string> newVal = new Dictionary<char, string>();


        public replace()    //Class' constructor, automatically runs when class is created.
        {
            newVal.Add(' ', "%20");
            newVal.Add('<', "%3C");
            newVal.Add('>', "%3E");
            newVal.Add('#', "%23");
            newVal.Add('%', "%25");
            newVal.Add('\"', "%93");  //converts "
            newVal.Add('{', "%7B");
            newVal.Add('}', "%7D");
            newVal.Add('|', "%7C");
            newVal.Add('\\', "%5C");  // converts \
            newVal.Add('^', "%5E");
            newVal.Add('[', "%5B");
            newVal.Add(']', "%5D");
            newVal.Add('`', "%60");  
            newVal.Add(';', "%3B");
            newVal.Add('/', "%2F");
            newVal.Add('?', "%3F");
            newVal.Add(':', "%3A");
            newVal.Add('@', "%40");
            newVal.Add('&', "%26");
            newVal.Add('=', "%3D");
            newVal.Add('+', "%2B");
            newVal.Add('$', "%24");
            newVal.Add(',', "%2C");
            /*Creates library of invalid characters and what they should be replaced with. This library
             * can easily be updated as necessary.
             */
        }

        public string SwapChar (char oldChar)
        {

            string returnString;

            if(newVal.TryGetValue(oldChar, out returnString))
            {
                return returnString;    //The character was found in the library. Returns the replacement string
            }
            else  //The character could not be found in the library
            {
                return oldChar.ToString();  //returns original character as a string
            }
        }

        public string MakeSafe(string oldString)
        {
            string safeString = "";

            foreach (char c in oldString)
            {
                if (Char.IsControl(c))
                {
                    return null;    //indicates a control character was detected
                }

                safeString += SwapChar(c);  //add safe character to safeString
            }

            return safeString;
        }
    }
}
