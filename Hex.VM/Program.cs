using System.Threading;
using Hex.VM.Core;

namespace Hex.VM
{
    class Program
    {
        static void Main(string[] args)
        {
             new Engine().Run(args);
        }
    }
}
