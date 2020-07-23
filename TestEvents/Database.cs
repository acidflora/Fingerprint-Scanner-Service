using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
    interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    class Database: IObservable //НаблюдаЕМЫЙ объект
    {
        private string ip;
        private string port;
        private string username;
        private string password;
        private string database;

        DatabaseInfo dInfo;

        List<IObserver> observers;

        public void onDatabaseConnection() //Market()
        {
            using (var conn = new NpgsqlConnection($"Host={ip};Port={port};Username={username};Password={password};Database={database}"))
            {
                try
                {
                    conn.Open();
                    conn.Notification += (o, e) =>
                    {
                    };
                        using (var cmd = new NpgsqlCommand("LISTEN virtual;", conn))//Wait "NOTIFY virtual" trigger
                        {
                            cmd.ExecuteNonQuery();
                        }
                    while (true)
                    {
                        conn.Wait();
                    }
                }
                catch
                {
                    dInfo.Status = false;
                    NotifyObservers();
                }
            }
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update(dInfo);
            }
        }

        public Database() 
        {
            ip = "localhost"; username = "postgres"; password = "Ms34901351"; database= "postgres";
            observers = new List<IObserver>();
            dInfo = new DatabaseInfo();
        }
        public Database(string ip, string port, string username, string password, string database) { this.ip = ip; this.port = port; this.username = username; this.password = password; this.database = database; }

    }
}
