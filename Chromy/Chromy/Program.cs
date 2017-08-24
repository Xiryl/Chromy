using Chromy.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Chromy
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Title
            var sb = new StringBuilder();
            var sbg = new StringBuilder();
            var sbend = new StringBuilder();

            sb.AppendLine(@"####################################################################################################");
            sb.AppendLine("                                                                                                     ");
            sb.AppendLine(@"               _..._                            .-'''-.                                             ");
            sb.AppendLine(@"            .-'_..._''.                        '   _    \                                           ");
            sb.AppendLine(@"          .' .'      '.\  .                  /   /` '.   \  __  __   ___                            ");
            sb.AppendLine(@"         / .'           .'|                 .   |     \  ' |  |/  `.'   `..-.          .-           ");
            sb.AppendLine(@"        . '            <  |         .-,.--. |   '      |  '|   .-.  .-.   '\ \\       / /           ");
            sb.AppendLine(@"        | |             | |         |  .-. |\    \     / / |  |  |  |  |  | \ \      / /            ");
            sb.AppendLine(@"        | |             | | .'''-.  | |  | | `.   ` ..' /  |  |  |  |  |  |  \ \    / /             ");
            sb.AppendLine(@"        . '             | |/.'''. \ | |  | |    '-...-'`   |  |  |  |  |  |   \ \  / /              ");
            sb.AppendLine(@"         \ '.          .|  /    | | | |  '-                |  |  |  |  |  |    \ `  /               ");
            sb.AppendLine(@"          '. `._____.-'/| |     | | | |                    |__|  |__|  |__|     \  /                ");
            sb.AppendLine(@"            `-.______ / | |     | | | |                                         / /                 ");
            sb.AppendLine(@"                     `  | '.    | '.|_|                                     |`-' /                  ");
            sb.AppendLine(@"                        '---'   '---'                                        '..'                   ");
            sb.AppendLine("                                                                                                     ");
            sbg.AppendLine(@"                                   _              _     ___        ");
            sbg.AppendLine(@"                                  |_)_      _ .__|_  ||  | _  _ | _");
            sbg.AppendLine(@"                                  | (_)\/\/(/_|  ||_|||  |(_)(_)|_>");
            sbg.AppendLine(@"                                                                                                   ");
            sbend.AppendLine(@"####################################################################################################");



            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sb);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sbg);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sbend);
            #endregion

            Console.Write("                               Performing some task... ");
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(20);
                }
            }
            Console.WriteLine("Done.");

            Console.ReadLine();


            //#region Kill All Chrome Process
            //var chromeInstances = Process.GetProcessesByName("chrome");

            //foreach (Process p in chromeInstances)
            //    p.Kill();

            //#endregion
        }
    }
}
