using System;
using SPV3.Domain;

namespace SPV3.Loader.CLI
{
    public class StatusOutput : IStatus
    {
        public void CommitStatus(string data)
        {
            Console.WriteLine(data);
        }

        public void CommitStatus(string data, StatusType type)
        {
            Console.WriteLine(data);
        }
    }
}