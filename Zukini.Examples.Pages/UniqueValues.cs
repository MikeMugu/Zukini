using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zukini.Pages;



namespace Zukini.Examples.Pages
{
    public class UniqueValues : BasePage
    {

        

        public UniqueValues(BrowserSession browser)
            : base(browser)
        {
        }

        
        //Adds the date and time to the begining of a string to make it unique
        public string UniqueValueDateTime(string Value)
        {
            DateTime dt = DateTime.Now;
            String UniqueValue = "";
            UniqueValue = dt.ToString("MMddyyyyHHmmss") + Value;
            return UniqueValue;
        }


        //Gives you a random string of characters
        public string UniqueValueAlphaCharacters(int numberOfCharsToGenerate)
        {
            var random = new Random();
            char[] chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            var sb = new StringBuilder();
            for (int i = 0; i < numberOfCharsToGenerate; i++)
            {
                int num = random.Next(0, chars.Length);
                sb.Append(chars[num]);
            }
            return sb.ToString();
        }



    }
}