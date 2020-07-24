using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    class FingerprintScanner : IObserver //НаблюдаЮЩИЙ объект
    {
        IObservable database;

        public FingerprintScanner(IObservable obs)
        {
            database = obs;
            database.RegisterObserver(this);//Регистрация подписки
        }

       

        public void UpdateStatusFinger(object ob)
        {
            //FingerprintScannerInfo fInfo = (FingerprintScannerInfo)ob;
            if (!FingerprintScannerInfo.StatusUpdate)
            {
                Console.WriteLine("Тягаем функцию считывания");
            }
            else
            {
                Console.WriteLine("Тягаем функцию записи");
            }
        }

        public void UpdateStatusServer(object ob)
        {
          
        }
        //-------------------------Функции которые нужно тягать-------------------------
        public async Task AcidAddPerson(string name, uint id, int pass)
        {
            await Task.Delay(2000);
        }

        public async Task<object[]> AcidGetRecords()
        {
            await Task.Delay(2000);
            return new object[] { 1, "20.01.2000 11:00", false };
        }
        //-------------------------Функции которые нужно тягать-------------------------
    }
}
