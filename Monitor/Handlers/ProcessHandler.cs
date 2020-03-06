using System;
using System.Diagnostics;
using System.Linq;
using Polly;

namespace Monitor.Handlers
{
    public class ProcessHandler
    {
        /// <summary>
        /// Проверяет процесс и завершает по условию из параметров
        /// </summary>
        /// <param name="param"></param>
        public void Handle(Params param)
        {
            if (IsProcessRunning(param.Name))
            {
                Policy.HandleResult<bool>(result => result)
                    .WaitAndRetry((int)Math.Ceiling(param.UpTime / param.Duration), s => TimeSpan.FromMinutes(param.Duration))
                    .Execute(() => IsProcessRunning(param.Name));
                KillProcess(param.Name);
            }
            else
            {
                Console.WriteLine("Process is not running");
            }
        }

        private bool IsProcessRunning(string name) =>
            !string.IsNullOrEmpty(name) && Process.GetProcessesByName(name).Length > 0;

        private void KillProcess(string name)
        {
            Process.GetProcessesByName(name)
                .ToList()
                .ForEach(
                    process =>
                    {
                        process.Kill();
                        Console.WriteLine($"Process {process} was killed");
                    });
        }
    }
}