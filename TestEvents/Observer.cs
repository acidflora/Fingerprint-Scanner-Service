using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    class Observer : IObserver //НаблюдаЮЩИЙ объект
    {
        IObservable database;
        public Observer(IObservable obs)
        {
            database = obs;
            database.RegisterObserver(this);//Регистрация подписки
        }

        public void UpdateStatusFinger(object ob)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatusServer(object ob)
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
