using LibEnumRemotePreviousVersion;
using System;

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
