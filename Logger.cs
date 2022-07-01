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
        static string savelogfile1 = "./log/Data_Device1_" + DateTime.Now.Year + ".txt";
        static string savelogfile2 = "./log/Data_Device2_" + DateTime.Now.Year + ".txt";
        static string savelogfile3 = "./log/Data_Device3_" + DateTime.Now.Year + ".txt";
        static string savelogfile4 = "./log/Data_Device4_" + DateTime.Now.Year + ".txt";
        static string savelogfile5 = "./log/Data_Device5_" + DateTime.Now.Year + ".txt";

        public static string ERROR = "E";
        public static string WARN = "W";
        public static string INFO = "I";
        public static string DEBUG = "D";
        public static string SAVE = "S";

        static ManagedThread workerThread = null;
        static ManagedThread workerThread1 = null;
        static ManagedThread workerThread2 = null;
        static ManagedThread workerThread4 = null;
        static ManagedThread workerThread3 = null;
        static ManagedThread workerThread5 = null;

        static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue1 = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue2 = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue3 = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue4 = new ConcurrentQueue<string>();
        static ConcurrentQueue<string> queue5 = new ConcurrentQueue<string>();

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

        static bool DoSave1(UInt64 called_count)
        {
            if (queue1.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile1, true);

                string sPattern = "[S]";

                while (queue1.TryDequeue(out log))
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

        static bool DoSave2(UInt64 called_count)
        {
            if (queue2.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile2, true);

                string sPattern = "[S]";

                while (queue2.TryDequeue(out log))
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

        static bool DoSave3(UInt64 called_count)
        {
            if (queue3.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile3, true);

                string sPattern = "[S]";

                while (queue3.TryDequeue(out log))
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

        static bool DoSave4(UInt64 called_count)
        {
            if (queue4.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile4, true);

                string sPattern = "[S]";

                while (queue4.TryDequeue(out log))
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

        static bool DoSave5(UInt64 called_count)
        {
            if (queue5.IsEmpty == false)
            {
                string log;

                StreamWriter sw = new StreamWriter(savelogfile5, true);

                string sPattern = "[S]";

                while (queue5.TryDequeue(out log))
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

            if (workerThread1 == null)
            {
                workerThread1 = new ManagedThread("Logger", DoSave1);
                workerThread1.Start();
            }

            if (workerThread2 == null)
            {
                workerThread2 = new ManagedThread("Logger", DoSave2);
                workerThread2.Start();
            }

            if (workerThread3 == null)
            {
                workerThread3 = new ManagedThread("Logger", DoSave3);
                workerThread3.Start();
            }

            if (workerThread4 == null)
            {
                workerThread4 = new ManagedThread("Logger", DoSave4);
                workerThread4.Start();
            }

            if (workerThread5 == null)
            {
                workerThread5 = new ManagedThread("Logger", DoSave5);
                workerThread5.Start();
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

            if (workerThread1 != null)
            {
                workerThread1.Stop();
                workerThread1.Join();

                workerThread1 = null;
            }

            if (workerThread2 != null)
            {
                workerThread2.Stop();
                workerThread2.Join();

                workerThread2 = null;
            }

            if (workerThread3 != null)
            {
                workerThread3.Stop();
                workerThread3.Join();

                workerThread3 = null;
            }

            if (workerThread4 != null)
            {
                workerThread4.Stop();
                workerThread4.Join();

                workerThread4 = null;
            }

            if (workerThread5 != null)
            {
                workerThread5.Stop();
                workerThread5.Join();

                workerThread5 = null;
            }

        }

        #endregion

        public static void log(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread1 != null)
            {
                queue1.Enqueue(log);
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

        public static void log1(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread1 != null)
            {
                queue1.Enqueue(log);
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
        public static void log2(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread2 != null)
            {
                queue2.Enqueue(log);
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

        public static void log3(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread1 != null)
            {
                queue3.Enqueue(log);
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

        public static void log4(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread4 != null)
            {
                queue4.Enqueue(log);
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

        public static void log5(string tag, string message)
        {
            string log = DateTime.Now.ToString() + " [" + tag + "] : " + message;
            if (workerThread5 != null)
            {
                queue5.Enqueue(log);
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

        public static void save1(string message)
        {
            log1(SAVE, message);
        }

        public static void save2(string message)
        {
            log2(SAVE, message);
        }
        public static void save3(string message)
        {
            log3(SAVE, message);
        }
        public static void save4(string message)
        {
            log4(SAVE, message);
        }
        public static void save5(string message)
        {
            log5(SAVE, message);
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
            savelogfile1 = "./log/Data_" + logfileName1 + "_" + DateTime.Now.Year + ".txt";
            savelogfile2 = "./log/Data_" + logfileName2 + "_" + DateTime.Now.Year + ".txt";
            savelogfile3 = "./log/Data_" + logfileName3 + "_" + DateTime.Now.Year + ".txt";
            savelogfile4 = "./log/Data_" + logfileName4 + "_" + DateTime.Now.Year + ".txt";
            savelogfile5 = "./log/Data_" + logfileName5 + "_" + DateTime.Now.Year + ".txt";

        }

    }
}
