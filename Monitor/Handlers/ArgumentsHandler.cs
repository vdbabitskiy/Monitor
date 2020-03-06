using System;

namespace Monitor
{
    public class ArgumentsHandler
    {
        /// <summary>
        /// Проверяет аргумент и парсит их в объект
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Params Handle(string[] args) => CheckArguments(args)? Parse(args): throw new ArgumentException();

        private bool CheckArguments(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Incorrect count of arguments");
                return false;
            }

            if (!int.TryParse(args[1], out int s))
            {
                Console.WriteLine("Uptime is incorrect type");
                return false;
            }

            if (!int.TryParse(args[2], out int t))
            {
                Console.WriteLine("Duration is incorrect type");
                return false;
            }

            return true;
        }

        private Params Parse(string[] args)
        {
            try
            {
                return new Params
                {
                    Name = args[0].ToLower(),
                    UpTime = double.Parse(args[1]),
                    Duration = double.Parse(args[2])
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to parse arguments");
                throw;
            }
        }
    }
}