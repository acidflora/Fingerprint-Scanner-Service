﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestEvents
{
   

    class Database: IObservable //НаблюдаЕМЫЙ объект
    {
        private string ip;
        private string port;
        private string username;
        private string password;
        private string database;

        DatabaseInfo dInfo;
        FingerprintScanner fInfo;
        Logger logger = new Logger();

        List<IObserver> observers;//Список наблюдателей

        public void onDatabaseConnection() //Проверка NOTIFY из БД и 
        {
            using (var conn = new NpgsqlConnection($"Host={ip};Port={port};Username={username};Password={password};Database={database}"))
            {
                try
                {
                    conn.Open();
                    DatabaseInfo.Status = true;
                    FingerprintScannerInfo.StatusUpdate = false;
                    NotifyObserversAboutUpdate();
                    logger.CreateLoggerFile("Server connected");
                    conn.Notification += (o, e) =>
                    {
                        FingerprintScannerInfo.StatusUpdate = true;
                        NotifyObserversAboutUpdate();
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
                    if (DatabaseInfo.Status)
                        logger.CreateLoggerFile("Server connection lost!");
                    DatabaseInfo.Status = false;
                    NotifyObserversAboutConnection();
                }
            }
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);//Добавление подписавшихся
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Add(o);//Удаление подписчиков
        }


        public void NotifyObserversAboutConnection()
        {
            foreach (IObserver o in observers)
            {
                o.UpdateStatusServer(dInfo);//Оповещение подписчиков о состоянии соединения
            }
        }

        public void NotifyObserversAboutUpdate()
        {
            foreach (IObserver o in observers)
            {
                o.UpdateStatusFinger(fInfo);//Оповещение подписчиков о новом пользователе
            }
        }

        public Database() 
        {
            ip = "localhost"; username = "postgres"; password = "Ms34901351"; database= "postgres";
            observers = new List<IObserver>();
            dInfo = new DatabaseInfo();
        }
        public Database(List<string> configuration) { this.ip = configuration[0]; this.port = configuration[1]; this.username = configuration[2]; this.password = configuration[3]; this.database = configuration[4]; }
        public Database(string ip, string port, string username, string password, string database) { this.ip = ip; this.port = port; this.username = username; this.password = password; this.database = database; }

    }
}
