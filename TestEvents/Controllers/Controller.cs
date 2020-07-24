using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEvents.Configurations;

namespace TestEvents.Controllers
{
    class Controller
    {
        public void Initialization()
        {
            ConfigurationServer сonfigurationServer = new ConfigurationServer();
            List<string> conf = сonfigurationServer.connectionString();
            Database database = new Database(conf);
            Observer observer = new Observer(database);
            FingerprintScanner fingerprint = new FingerprintScanner(database);
            database.onDatabaseConnection();
        }
    }
}
