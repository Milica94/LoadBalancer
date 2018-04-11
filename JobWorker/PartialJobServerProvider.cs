using InterroleContracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class PartialJobServerProvider : IJobInternal
    {
        LogInterroleDataRepository repo = new LogInterroleDataRepository();

        public string InsertIntoTable(string str, int fromId)
        {
            string retStr = String.Empty;
            Trace.TraceInformation("Primljena je vrednost: " + str + " od instance sa rednim brojem " + fromId);

            string currId = RoleEnvironment.CurrentRoleInstance.Id;
            int currIndex = 0;
            if (!int.TryParse(currId.Substring(currId.LastIndexOf(".") + 1), out currIndex))
            {
                int.TryParse(currId.Substring(currId.LastIndexOf("_") + 1), out currIndex);
            }

            try
            {
                LogInterrole li = new LogInterrole(str) { FromId = fromId, CurrentId = currIndex, Value = str };
                repo.AddLog(li);
                retStr = "Upisana je vrednost: " + str;
            }
            catch(Exception e)
            {
                Trace.TraceInformation("AddLog failed with error message: {0}", e.Message);
                retStr = String.Format("Vrednost *{0}* nije upisana u tabelu", str);
            }

            return retStr;
        }
    }
}
