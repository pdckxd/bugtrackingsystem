using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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

            //Dynamic Programming #3
            //the result of evaluating a dynamic expression is a dynamic expression
            dynamic d = 123;
            var result = M(d); // Note: 'var result' is the same as 'dynamic result'


            //Dynamic Programming #4 COM Operation
            //Application excel = new Application();
            //excel.Visible = true;
            //excel.Workbooks.Add(Type.Missing);
            //((Range)excel.Cells[1, 1]).Value = "Text in cell A1"; // Put this string in cell A1
            //excel.Cells[1, 1].Value = "Text in cell A1"; // Put this string in cell A1

            //Dynamic Programming #5 Reflection
            Object target = "Jeffrey Richter";
            Object arg = "ff";
            // Find a method on the target that matches the desired argument types
            Type[] argTypes = new Type[] { arg.GetType() };
            MethodInfo method = target.GetType().GetMethod("Contains", argTypes);
            // Invoke the method on the target passing the desired arguments
            Object[] arguments = new Object[] { arg };
            Boolean result1 = Convert.ToBoolean(method.Invoke(target, arguments));

            dynamic targetD = "Jeffrey Richter";
            dynamic argD = "ff";
            Boolean resultD = targetD.Contains(argD);

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

        private static void M(Int32 n) { Console.WriteLine("M(Int32): " + n); }

        #region Old way for Named and Optional method

        //public static void Method(int age)
        //{
        //    Method(age, "John");
        //}

        //public static void Method(int age, String firstname)
        //{
        //    Method(age, firstname, 2000);
        //}

        //public static void Method(int age, String firstname, double salary)
        //{
        //    // Actual work done here
        //}

        #endregion
    }
}
