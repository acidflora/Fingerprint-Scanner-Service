using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEvents.Controllers;

namespace TestEvents
{
    class Observer : IObserver //НаблюдаЮЩИЙ объект
    {
        IObservable database;
        Controller controller = new Controller();
        public Observer(IObservable obs)
        {
            database = obs;
            database.RegisterObserver(this);//Регистрация подписки
        }

        public void UpdateStatusFinger(object ob)
        {
        }

        public void UpdateStatusServer(object ob)
        {
            DatabaseInfo dInfo = (DatabaseInfo)ob;
            if (!DatabaseInfo.Status)
                controller.Initialization();
        }

    }
   
}
