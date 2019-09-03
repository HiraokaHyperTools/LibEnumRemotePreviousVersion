using LibEnumRemotePreviousVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine(@"TestApp \\server\share");
                return 1;
            }
            Console.WriteLine(
                string.Join(
                    Environment.NewLine,
                    PreviousVersionOnRemote.Enum(args[0])
                )
            );
            return 0;
        }
    }
}
