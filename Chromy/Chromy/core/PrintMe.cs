﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chromy.core
{
    class PrintMe
    {
        public static void PrintHeader()
        {
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
            sbg.AppendLine("                                            Protect Yourself        ");
            sbg.AppendLine(@"                                                                                                   ");
            sbend.AppendLine(@"####################################################################################################");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sb);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sbg);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sbend);
        }

        public static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintInfo("HELP", ConsoleColor.Yellow, "$> dump              [For dump password list on local folder]");
            PrintInfo("HELP", ConsoleColor.Yellow, "$> dump -d           [For dump password list on desktop]");
            PrintInfo("HELP", ConsoleColor.Yellow, "$> dump -p path      [For dump password list on specific path]");
            PrintInfo("HELP", ConsoleColor.Yellow, "$> dump -m           [For dump password list on specific email (using TSL/SSL)]");
            PrintInfo("HELP", ConsoleColor.Yellow, "$> clear             [Clear chrome password list]");
        }

        public static void PrintInfo(string info, ConsoleColor color, string mx)
        {
            StringBuilder l = new StringBuilder();
            StringBuilder c = new StringBuilder();
            StringBuilder r = new StringBuilder();

            l.Append("[");
            c.Append(info);
            r.Append("]");


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(l);
            Console.ForegroundColor = color;
            Console.Write(c);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(r);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(mx);
        }
    }
}
