using InterroleContracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class JobServerProvider : IJob
    {
        NetTcpBinding binding = new NetTcpBinding();
        public string AddEntity(string str)
        {
            string retVal=String.Empty;

            Trace.TraceInformation("Uneta vrednost je: {0}. Treba je sacuvati u tabeli.", str);
            string instanceId = RoleEnvironment.CurrentRoleInstance.Id;
            int instanceIndex = 0;
            if (!int.TryParse(instanceId.Substring(instanceId.LastIndexOf(".") + 1), out instanceIndex))
            {
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf("_") + 1), out instanceIndex);
            }

            List<RoleInstance> instances = RoleEnvironment.Roles["JobWorker"].Instances.Where(instance => instance.Id != RoleEnvironment.CurrentRoleInstance.Id).ToList();

            int internalRoleIndex;

            if (instanceIndex % 2 == 0)
            {
                //paran je
                if (instanceIndex == 0)
                {
                    internalRoleIndex = RoleEnvironment.Roles["JobWorker"].Instances.Count-1;
                }
                else
                {
                    internalRoleIndex = instanceIndex - 1;
                }

            }
            else
            {
                if (instanceIndex < (RoleEnvironment.Roles["JobWorker"].Instances.Count - 1))
                {
                    internalRoleIndex = instanceIndex + 1;
                }
                else
                {
                    internalRoleIndex = 0;
                }
            }

            //EndpointAddress ea = new EndpointAddress(String.Format("net.tcp://{0}/{1}", RoleEnvironment.Roles["JobWorker"].Instances[internalRoleIndex].InstanceEndpoints["InternalRequest"].IPEndpoint.ToString(), "InternalRequest"));
            //Task<string> sendToOtherInstance = new Task<string>(() =>
            //        {
            //            IJobInternal proxy = new ChannelFactory<IJobInternal>(binding, ea).CreateChannel();
            //            return proxy.InsertIntoTable(str, instanceIndex);
            //        });

            //sendToOtherInstance.Start();
            //Task.WaitAll(sendToOtherInstance); //?
            //retVal = sendToOtherInstance.Result;

            foreach (RoleInstance ri in instances)
            {
                string id = ri.Id;
                int index = 0;
                if (!int.TryParse(id.Substring(id.LastIndexOf(".") + 1), out index))
                {
                    int.TryParse(id.Substring(id.LastIndexOf("_") + 1), out index);
                }

                if (index == internalRoleIndex)
                {
                    EndpointAddress ea = new EndpointAddress(String.Format("net.tcp://{0}/{1}", ri.InstanceEndpoints["InternalRequest"].IPEndpoint.ToString(), "InternalRequest"));
                    Task<string> sendToOtherInstance = new Task<string>(() =>
                    {
                        IJobInternal proxy = new ChannelFactory<IJobInternal>(binding, ea).CreateChannel();
                        return proxy.InsertIntoTable(str, instanceIndex);
                    });

                    sendToOtherInstance.Start();
                    Task.WaitAll(sendToOtherInstance); //?
                    retVal = sendToOtherInstance.Result;
                    break;
                }
            }



            return retVal;
        }
    }
}
