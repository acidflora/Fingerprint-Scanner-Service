using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            Observer observer = new Observer(database);
            database.onDatabaseConnection();
            Console.ReadLine();
        }
    }
}
