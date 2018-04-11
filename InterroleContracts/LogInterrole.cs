using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterroleContracts
{
    public class LogInterrole : TableEntity
    {
        public int CurrentId { get; set; }
        public int FromId { get; set; }
        public string Value { get; set; }

        public LogInterrole()
        {
            
        }

        public LogInterrole(string val)
        {
            PartitionKey = "Logging";
            RowKey = val;
        }
    }
}
