using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OmegaTempCollector.Common
{
    public interface ILogger
    {
        void log(string tag, string message);
        public void error(string message);
        public void error(Exception e);
        public void warn(string message);
        public void info(string message);
        public void debug(string message);
        public void save(string message);
    }

    public class FileLogger : ILogger
    {
        public void debug(string message)
        {
            throw new NotImplementedException();
        }

        public void error(string message)
        {
            throw new NotImplementedException();
        }

        public void error(Exception e)
        {
            throw new NotImplementedException();
        }

        public void info(string message)
        {
            throw new NotImplementedException();
        }

        public void log(string tag, string message)
        {
            throw new NotImplementedException();
        }

        public void warn(string message)
        {
            throw new NotImplementedException();
        }
        public void save(string message)
        {
            throw new NotImplementedException();
        }

    }

    public static class Logger
    {
        //static string logfile = AppDomain.CurrentDomain.BaseDirectory + "log_" + DateTime.Now.ToLongDateString() + ".txt";
        static string logfile = "./log/log_" + DateTime.Now.ToLongDateString() + ".txt";
        //static string savelogfile = "./log/Data_" + DateTime.Now.Year + ".txt";
        static string savelogfile1A = "./log/Data_Device1A_" + DateTime.Now.Year + ".txt";
        static string savelogfile2A = "./log/Data_Device2A_" + DateTime.Now.Year + ".txt";
        static string savelogfile3A = "./log/Data_Device3A_" + DateTime.Now.Year + ".txt";
        static string savelogfile4A = "./log/Data_Device4A_" + DateTime.Now.Year + ".txt";
        static string savelogfile5A = "./log/Data_Device5A_" + DateTime.Now.Year + ".txt";

        static string savelogfile1B = "./log/Data_Device1B_" + DateTime.Now.Year + ".txt";
        static string savelogfile2B = "./log/Data_Device2B_" + DateTime.Now.Year + ".txt";
        static string savelogfile3B = "./log/Data_Device3B_" + DateTime.Now.Year + ".txt";
        static string savelogfile4B = "./log/Data_Device4B_" + DateTime.Now.Year + ".txt";
        static string savelogfile5B = "./log/Data_Device5B_" + DateTime.Now.Year + ".txt";

        public static string ERROR = "E";
        public static string WARN = "W";
        public static string INFO = "I";
        public static string DEBUG = "D";
        public static string SAVE = "S";

        static ManagedThread workerThread = null;
        static ManagedThread workerThread1A = null;
        static ManagedThread workerThread2A = null;
        static ManagedThread workerThread4A = null;
        static ManagedThread workerThread3A = null;
        static ManagedThread workerThread5A = null;

        static ManagedThread workerThread1B = null;
        static ManagedThread workerThread2B = null;
        static ManagedThread workerThread4B = null;
        static ManagedThread workerThread3B = null;
        static ManagedThread workerThread5B = null;

        static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue1A = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue2A = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue3A = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue4A = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue5A = new ConcurrentQueue<string>();

        static ConcurrentQueue<string> queue1B = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue2B = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue3B = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue4B = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue5B = new ConcurrentQueue<string>();

        #region Log writer thread 
        static bool DoWork(UInt64 called_count)
        {
            if (queue.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(logfile, true);

                string sPattern = "[S]";

                while (queue.TryDequeue(out log))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave1A(UInt64 called_count)
        {
            if (queue1A.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile1A, true);

                string sPattern = "[S]";

                while (queue1A.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave2A(UInt64 called_count)
        {
            if (queue2A.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile2A, true);

                string sPattern = "[S]";

                while (queue2A.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave3A(UInt64 called_count)
        {
            if (queue3A.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile3A, true);

                string sPattern = "[S]";

                while (queue3A.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave4A(UInt64 called_count)
        {
            if (queue4A.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile4A, true);

                string sPattern = "[S]";

                while (queue4A.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave5A(UInt64 called_count)
        {
            if (queue5A.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile5A, true);

                string sPattern = "[S]";

                while (queue5A.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave1B(UInt64 called_count)
        {
            if (queue1B.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile1B, true);

                string sPattern = "[S]";

                while (queue1B.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave2B(UInt64 called_count)
        {
            if (queue2B.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile2B, true);

                string sPattern = "[S]";

                while (queue2B.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave3B(UInt64 called_count)
        {
            if (queue3B.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile3B, true);

                string sPattern = "[S]";

                while (queue3B.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave4B(UInt64 called_count)
        {
            if (queue4B.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile4B, true);

                string sPattern = "[S]";

                while (queue4B.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        static bool DoSave5B(UInt64 called_count)
        {
            if (queue5B.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile5B, true);

                string sPattern = "[S]";

                while (queue5B.TryDequeue(out log))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(log, sPattern))
                    {
                        Console.WriteLine(log);
                        sw.WriteLine(log);
                    }
                }
                sw.Close();
            }
            return true;
        }

        public static void start()
        {       
                
            if (workerThread == null)
            {
                workerThread = new ManagedThread("Logger", DoWork);
                workerThread.Start();
            }

            if (workerThread1A == null)
            {
                workerThread1A = new ManagedThread("Logger", DoSave1A);
                workerThread1A.Start();
            }

            if (workerThread2A == null)
            {
                workerThread2A = new ManagedThread("Logger", DoSave2A);
                workerThread2A.Start();
            }

            if (workerThread3A == null)
            {
                workerThread3A = new ManagedThread("Logger", DoSave3A);
                workerThread3A.Start();
            }

            if (workerThread4A == null)
            {
                workerThread4A = new ManagedThread("Logger", DoSave4A);
                workerThread4A.Start();
            }

            if (workerThread5A == null)
            {
                workerThread5A = new ManagedThread("Logger", DoSave5A);
                workerThread5A.Start();
            }

            if (workerThread1B == null)
            {
                workerThread1B = new ManagedThread("Logger", DoSave1B);
                workerThread1B.Start();
            }

            if (workerThread2B == null)
            {
                workerThread2B = new ManagedThread("Logger", DoSave2B);
                workerThread2B.Start();
            }

            if (workerThread3B == null)
            {
                workerThread3B = new ManagedThread("Logger", DoSave3B);
                workerThread3B.Start();
            }

            if (workerThread4B == null)
            {
                workerThread4B = new ManagedThread("Logger", DoSave4B);
                workerThread4B.Start();
            }

            if (workerThread5B == null)
            {
                workerThread5B = new ManagedThread("Logger", DoSave5B);
                workerThread5B.Start();
            }
        }

        public static void finish()
        {
            if (workerThread != null)
            {
                workerThread.Stop();
                workerThread.Join();

                workerThread = null;
            }

            if (workerThread1A != null)
            {
                workerThread1A.Stop();
                workerThread1A.Join();

                workerThread1A = null;
            }

            if (workerThread2A != null)
            {
                workerThread2A.Stop();
                workerThread2A.Join();

                workerThread2A = null;
            }

            if (workerThread3A != null)
            {
                workerThread3A.Stop();
                workerThread3A.Join();

                workerThread3A = null;
            }

            if (workerThread4A != null)
            {
                workerThread4A.Stop();
                workerThread4A.Join();

                workerThread4A = null;
            }

            if (workerThread5A != null)
            {
                workerThread5A.Stop();
                workerThread5A.Join();

                workerThread5A = null;
            }

            if (workerThread1B != null)
            {
                workerThread1B.Stop();
                workerThread1B.Join();

                workerThread1B = null;
            }

            if (workerThread2B != null)
            {
                workerThread2B.Stop();
                workerThread2B.Join();

                workerThread2B = null;
            }

            if (workerThread3B != null)
            {
                workerThread3B.Stop();
                workerThread3B.Join();

                workerThread3B = null;
            }

            if (workerThread4B != null)
            {
                workerThread4B.Stop();
                workerThread4B.Join();

                workerThread4B = null;
            }

            if (workerThread5B != null)
            {
                workerThread5B.Stop();
                workerThread5B.Join();

                workerThread5B = null;
            }

        }

        #endregion

        public static void log(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread != null)
            {
                queue.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log1A(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread1A != null)
            {
                queue1A.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }
        public static void log2A(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread2A != null)
            {
                queue2A.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log3A(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread3A != null)
            {
                queue3A.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log4A(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread4A != null)
            {
                queue4A.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log5A(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread5A != null)
            {
                queue5A.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log1B(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread1B != null)
            {
                queue1B.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }
        public static void log2B(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread2B != null)
            {
                queue2B.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log3B(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread3B != null)
            {
                queue3B.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log4B(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread4B != null)
            {
                queue4B.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }

        public static void log5B(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread5B != null)
            {
                queue5B.Enqueue(log);
                log = "." + log;
            }
            else
            {
                StreamWriter sw = new StreamWriter(logfile, true);
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }

            System.Diagnostics.Debug.Print(log);
        }


        public static void error(string message)
        {
            log(ERROR, message);
        }
        public static void error(Exception e)
        {
            log(ERROR, e.Message);
            System.Diagnostics.Debug.Print(e.StackTrace);
        }

        public static void warn(string message)
        {
            log(WARN, message);
        }

        public static void info(string message)
        {
            log(INFO, message);
        }

        public static void debug(string message)
        {
            log(DEBUG, message);
        }

        public static void save1A(string message)
        {
            log1A(SAVE, message);
        }

        public static void save2A(string message)
        {
            log2A(SAVE, message);
        }
        public static void save3A(string message)
        {
            log3A(SAVE, message);
        }
        public static void save4A(string message)
        {
            log4A(SAVE, message);
        }
        public static void save5A(string message)
        {
            log5A(SAVE, message);
        }

        public static void save1B(string message)
        {
            log1B(SAVE, message);
        }

        public static void save2B(string message)
        {
            log2B(SAVE, message);
        }
        public static void save3B(string message)
        {
            log3B(SAVE, message);
        }
        public static void save4B(string message)
        {
            log4B(SAVE, message);
        }
        public static void save5B(string message)
        {
            log5B(SAVE, message);
        }

        public static void WriteErrorLog(Exception ex)
        {
            log(ERROR, ex.Source.ToString() + "; " + ex.Message.ToString().Trim());
        }

        public static void WriteErrorLog(string message)
        {
            log(ERROR, message);
        }

        public static void SetlogfileName(string logfileName1, string logfileName2, string logfileName3, string logfileName4, string logfileName5)
        {
             //logfile = "./log/log_" + logfileName + "_"+ DateTime.Now.ToLongDateString() + ".txt";
            savelogfile1A = "./log/Data_" + logfileName1 + "A_" + DateTime.Now.Year + ".txt";
            savelogfile2A = "./log/Data_" + logfileName2 + "A_" + DateTime.Now.Year + ".txt";
            savelogfile3A = "./log/Data_" + logfileName3 + "A_" + DateTime.Now.Year + ".txt";
            savelogfile4A = "./log/Data_" + logfileName4 + "A_" + DateTime.Now.Year + ".txt";
            savelogfile5A = "./log/Data_" + logfileName5 + "A_" + DateTime.Now.Year + ".txt";

            savelogfile1B = "./log/Data_" + logfileName1 + "B_" + DateTime.Now.Year + ".txt";
            savelogfile2B = "./log/Data_" + logfileName2 + "B_" + DateTime.Now.Year + ".txt";
            savelogfile3B = "./log/Data_" + logfileName3 + "B_" + DateTime.Now.Year + ".txt";
            savelogfile4B = "./log/Data_" + logfileName4 + "B_" + DateTime.Now.Year + ".txt";
            savelogfile5B = "./log/Data_" + logfileName5 + "B_" + DateTime.Now.Year + ".txt";

        }

    }
}
