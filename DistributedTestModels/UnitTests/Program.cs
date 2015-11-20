using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DistributedTestEnvironmentUI.Models;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEnvironmentModel te = new TestEnvironmentModel();
            
            te.addNode("ServerHost");
            te.addNode("ClientHost");
            
            string[] argus1 = {"8001", "Patrick"};
            string[] argus2 = {"8002", "Walter"};
            string[] argus3 = { "8003", "Maurice" };
            string[] argus4 = { "8000", "Asim" };

            te.addProcess("ClientTest.exe", "ClientHost", 8000, ".\\", argus1);
            te.addProcess("ClientTest.exe", "ClientHost", 8001, ".\\", argus2);
            te.addProcess("ClientTest.exe", "ClientHost", 8002, ".\\", argus3);
            te.addProcess("ClientTest.exe", "ClientHost", 8003, ".\\", argus4);

            DistributedProcessModel tmp = te.getProcess("ClientHost", "ClientTest.exe", 8000);
            te.setDelay(tmp, 3000);
            te.enableDelay(tmp);

            te.startProcess("ClientHost", 8000, "ClientTest.exe");
            te.startProcess("ClientHost", 8001, "ClientTest.exe");
            te.startProcess("ClientHost", 8002, "ClientTest.exe");
            te.startProcess("ClientHost", 8003, "ClientTest.exe");
              

        }
    }
}
