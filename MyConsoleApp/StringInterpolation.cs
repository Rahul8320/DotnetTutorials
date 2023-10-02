namespace MyConsoleApp
{
    public class StringInterpolation
    {
        public static void PrintString()
        {
            int a = 26;
            float b = 109.48f;
            string c = "csharp";

            Console.WriteLine($"The value is {a}, {b}, {c}");

            Console.WriteLine($"The value is {a}, {b}, {c.ToUpper()}");

            DateTime now = DateTime.Now;
            Console.WriteLine($"Today is {now}");

            // Formate the string in base align
            Console.WriteLine("{0, -15} {1, 10}", "Float Value", "Int Value");
            Console.WriteLine($"{b,-15} {a,10}");
        }

        public static void IterateString()
        {
            string str = "The quick brown fox jumps over the lazy dog";
            Console.WriteLine(str.Length);
            Console.WriteLine(str[14]);
            // Iterate through the string
            foreach (char ch in str)
            {
                Console.Write(ch);
                if (ch == 'b')
                {
                    Console.WriteLine();
                    break;
                }
            }

            string[] stars = ["One", "Two", "Three", "Four"];
            string outerStr;
            outerStr = string.Concat(stars); // Concat the string array
            Console.WriteLine(outerStr);

            Console.WriteLine(string.Join(".", stars)); // joins the string array with '.'
            Console.WriteLine(string.Join("---", stars)); // joins the string array with '---'
        }

        public static void CompareString()
        {
            string str = "This is a string";

            // Equals just returns a regular expression.
            bool isEqual = str.Equals("THIS is a STRING");
            Console.WriteLine($"{isEqual}");

            // Compare will perform an ordinal comparison and return:
            // * "< 0" : first string comes before second in sort order
            // ! "0" : first and second strings are same position in sort order
            // * "> 0" : first string comes after the second in sort order
            int result = string.Compare(str, "This is a string");
            Console.WriteLine($"{result}");

            string str2 = "The quick brown Fox jumps over the lazy Dog";
            string outerStr = str2.Replace("Fox", "cat");
            Console.WriteLine(outerStr);

            // Contains determines whether a string contains certain content
            Console.WriteLine(str2.Contains("fox")); //! -> False
            // We can specify to ignore case sensitivity by passing an argument
            Console.WriteLine(str2.Contains("fox", StringComparison.CurrentCultureIgnoreCase)); //! -> True

            // * We can determine if the string StartWith or EndWith certain content
            Console.WriteLine(str2.StartsWith("The"));
            Console.WriteLine(str2.EndsWith("dog", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}