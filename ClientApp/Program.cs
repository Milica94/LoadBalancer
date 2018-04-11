using InterroleContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            while (true)
            {
                Console.WriteLine("\nUnesite nesto: ");
                string str = Console.ReadLine();

                IJob proxy = new ChannelFactory<IJob>(binding, new EndpointAddress("net.tcp://localhost:10100/InputRequest")).CreateChannel();
                string s = proxy.AddEntity(str);
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }
    }
}
