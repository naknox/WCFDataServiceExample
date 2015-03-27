using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WCFMoviesLib2;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Movie m = new Movie { MovieName = "fjhdsk" };

            //Console.WriteLine(string.Format("MovieName => {0}",m.MovieName));
            //Console.WriteLine(string.Format("MovieId => {0}", m.MovieId));

            PrintProperties(m);

            Console.ReadLine();
        }

        public static void PrintProperties<T>(T obj)
        {
            Type t = typeof(T);

            foreach (PropertyInfo pInfo in t.GetProperties())
            {
                MethodInfo getMethod = pInfo.GetGetMethod();
                object value = getMethod.Invoke(obj, null);

                Console.WriteLine(string.Format("{0} => {1}", pInfo.Name, value));
            }
        }
    }
}
