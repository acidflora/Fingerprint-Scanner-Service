using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    interface IObserver
    {
        void Update(Object ob);
    }


    class Observer : IObserver //НаблюдаЮЩИЙ объект
    {

        IObservable database;
        public Observer(IObservable obs)
        {
            database = obs;
            database.RegisterObserver(this);
        }
        public void Update(object ob)
        {
            DatabaseInfo dInfo = (DatabaseInfo)ob;
            if (!dInfo.Status)
            {
                Console.WriteLine("Сервер упал!");
            }
            else
            {
                Console.WriteLine("Сервер стоит!");
            }
        }

    }
   
}
