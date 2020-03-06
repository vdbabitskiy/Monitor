using Monitor.Handlers;

namespace Monitor
{
    internal class Monitor
    {
        private static void Main(string[] args)
        {
            new ProcessHandler()
                .Handle(
                    new ArgumentsHandler()
                        .Handle(args));
        }
    }
}