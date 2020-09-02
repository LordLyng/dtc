using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace dtc
{
    class Program
    {
        static int Main(string[] args)
        {
            var command = new RootCommand()
            {
                new Argument<long>("timestamp", () => 0) { Description = "utc timestamp" },
                new Option(new string[] {"--now", "-n"}) {Argument = new Argument<bool>(() => false), IsRequired = false}
            };

            command.Handler = CommandHandler.Create<long, bool>(Handle);

            return command.InvokeAsync(args).Result;
        }

        static int Handle(long timestamp, bool now)
        {
            if (now)
            {
                var nowDt = DateTimeOffset.UtcNow;
                Console.WriteLine($"UTC Timestamp:{'\t'}{nowDt.ToUnixTimeMilliseconds()}");
                Console.WriteLine($"UTC:{"\t\t"}{nowDt}");
                Console.WriteLine($"Local:{"\t\t"}{nowDt.ToLocalTime()}");

                return 0;
            }

            var dt = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            Console.WriteLine($"UTC:{'\t'}{dt}");
            Console.WriteLine($"Local:{'\t'}{dt.ToLocalTime()}");
            return 0;
        }

    }
}
