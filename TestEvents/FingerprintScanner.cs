using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    class FingerprintScanner : IObserver
    {
        IObservable database;

        public FingerprintScanner(IObservable obs)
        {
            database = obs;
            database.RegisterObserver(this);//Регистрация подписки
        }

        public void UpdateStatusFinger(object ob)
        {
            FingerprintScannerInfo fInfo = (FingerprintScannerInfo)ob;
            if (fInfo.Status)
            {
                Task task = new Task(()=>Console.WriteLine("Ожидаание отпечатков пальцев"));
            }
        }

        public void UpdateStatusServer(object ob)
        {
            DatabaseInfo dInfo = (DatabaseInfo)ob;
            if (!dInfo.Status)
            {
                Console.WriteLine("Отпечаток пальца не будет считан!");
            }
            else
            {
                Console.WriteLine("Отпечаток пальца будет считан");
            }
        }

    }
}
