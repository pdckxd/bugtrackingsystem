using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewFeaturesCSharp4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dynamic Programming #1
            dynamic dynamicObject = GetCustomer();
            Console.WriteLine("First Name is " + dynamicObject.FirstName);
            //Console.WriteLine("Middle Name is " + dynamicObject.MiddelName);
            Console.WriteLine("Last Name is " + dynamicObject.LastName);
            dynamicObject.CalculateSalary();

            //Dynamic Programming #2
            dynamic foo = 1234567890;
            System.Console.WriteLine(foo);
            foo = "John";
            System.Console.WriteLine(foo);
            foo = true;
            System.Console.WriteLine(foo);

            //Named and optional method
            Method(30); //The same as Method(30,"John",4000.99)
            Method(30, "Mary"); //The same as Method(30,"Mary",4000.99)
            Method(30, "Mary", 2000.99);
            //Method(30, 2000.9);
            Method(30, salary: 2000.9);

            //Covariance and Contravariance
            // Say we have a list of strings
            IList<string> arrNames = new List<string>();
            // Now we can covert it into an Enumerable collection
            IEnumerable<object> objects = arrNames;

            Console.Read();
        }

        private static Customer GetCustomer()
        {
            return new Customer() { FirstName = "Tony",LastName="Wu" };
        }

        /// <summary>
        /// Named and optional method
        /// </summary>
        /// <param name="age"></param>
        /// <param name="firstname"></param>
        /// <param name="salary"></param>
        public static void Method(int age, String firstname = "Jack", double salary = 4000.99)
        { }
    }
}
