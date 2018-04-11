using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterroleContracts
{
    public class LogInterroleDataRepository
    {
        //Obavezno dodati Microsoft.WindowsAzure.ConfigurationManager u manage nuget packages!!!
        private CloudTable _table;
        private CloudStorageAccount _storageAccount;

        public LogInterroleDataRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("LogInterroleTable");
            _table.CreateIfNotExists();
        }

        public void AddLog(LogInterrole newLog)
        {
            TableOperation insertOperation = TableOperation.Insert(newLog);
            _table.Execute(insertOperation);
        }
    }
}
