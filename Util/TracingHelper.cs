/**
*@项目名称: 停车场岗亭管理软件系统
*@文件名称: TracingHelper
*@Date: 2015-06-11 10:24:00
*@Copyright: 2015 悦畅科技有限公司. All rights reserved.
*注意：本内容仅限于悦畅科技有限公司内部传阅，禁止外泄以及用于其他的商业目的
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace LoserUtil
{
    public class TracingHelper
    {
        private static string LogPath = System.AppDomain.CurrentDomain.BaseDirectory+"//Log";
        //private static int LocalTraceLevel;
        //private static int TraceLevel;
        //private static int Parking;
        //private static int ParkingBox;

        //private static string CommVersion;
        //private static string AsblyVersion;

        public static void Initialization(int _LocalTraceLevel, int _TraceLevel, int _Parking, int _ParkingBox, string _LogPath, string _CommVersion, string _AsblyVersion)
        {
            LogPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //LocalTraceLevel = _LocalTraceLevel;
            //TraceLevel = _TraceLevel;
            //Parking = _Parking;
            //ParkingBox = _ParkingBox;

            //CommVersion = _CommVersion;
            //AsblyVersion = _AsblyVersion;
        }
        #region static

        private static Queue<Trace> tracingQueue = new Queue<Trace>();
        private static object rootLock = new object();
        private static bool running = false;
        private static bool _openInfoTrace = true;

        public static void SetTracingSwitch(bool openInfoTrace)
        {
            _openInfoTrace = openInfoTrace;
        }

        private static string info = "[{0}]\r\n[{5}]\r\n[Time]={1}\r\n[className]={2}\r\n[ExceptionMessage]={3}\r\n[ExceptionStackTrace]={4}\r\n{5}\r\n\r\n";

        #endregion

        #region Error 不可以关闭，只要系统出现错误必须记录下来

        /// <summary>
        /// 在catch中使用，记录系统未知异常
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="classType">所在的类的类型，如typeof(frmlogin)</param>
        /// <param name="message">附加的信息</param>
        public static void Error(Exception ex, Type classType, string message)
        {
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Trace trace = new Trace() { ex = ex, classType = classType, message = message, traceType = TraceType.Error };
            add(trace);
        }
        /// <summary>
        /// 在catch中使用，记录系统未知异常
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="classType">所在的类的类型，如typeof(frmlogin)</param>
        public static void Error(Exception ex, Type classType)
        {
            Error(ex, classType, null);
        }
        /// <summary>
        /// 在catch中使用，记录系统未知异常
        /// </summary>
        /// <param name="classType">所在的类的类型，如typeof(frmlogin)</param>
        /// <param name="message">附加的信息</param>
        public static void Error(Type classType, string message)
        {
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Error(null, classType, message);
        }

        /// <summary>
        /// 在catch中使用，记录系统未知异常
        /// </summary>
        /// <param name="message">附加的信息</param>
        public static void Error(string message)
        {
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Error(null, null, message);
        }

        #endregion

        #region Info 可以设置开启或者关闭

        /// <summary>
        /// 记录非异常的日志，主要为了检查关键流程是否走到
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            if (!_openInfoTrace) return;
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Info(null, message);
        }

        public static void Warning(string message)
        {
            if (!_openInfoTrace) return;
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Trace trace = new Trace() { ex = null, traceType = TraceType.Warning, classType = null, message = message };
            add(trace);
        }

        /// <summary>
        /// 记录非异常的日志，主要为了检查关键流程是否走到
        /// </summary>
        /// <param name="typeName">所在的类的类型，如typeof(frmlogin)</param>
        /// <param name="message">日志内容</param>
        public static void Info(Type typeName, string message)
        {
            if (!_openInfoTrace) return;
            message += "\r\n[Havetime]=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            Trace trace = new Trace() { ex = null, traceType = TraceType.Info, classType = typeName, message = message };
            add(trace);
        }

        #endregion

        #region private

        private static void add(Trace message)
        {
            try
            {

                lock (rootLock)
                {
                    tracingQueue.Enqueue(message);
                    if (!running)
                        start();
                }
            }
            catch { }
        }

        private static void start()
        {
            try
            {
                Thread th = new Thread(new ThreadStart(get));
                th.Name = "LogThread";
                th.IsBackground = true;
                th.Start();
                running = true;
            }
            catch { }
        }

        private static void get()
        {
            try
            {
                int i = 50;//批量写，提高IO效率
                List<Trace> traceArry = new List<Trace>();
                lock (rootLock)
                {
                    //避免内存溢出只维护2M
                    if (tracingQueue.Count > 20000)
                        tracingQueue.Clear();

                    while (i > 0)
                    {
                        if (tracingQueue.Count == 0)
                            break;

                        Trace trace = tracingQueue.Dequeue();
                        traceArry.Add(trace);
                        i -= 1;
                    }
                }
                if (traceArry.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Trace t in traceArry)
                    {
                        if (t != null)
                        {
                            string temp = buildTrace(t);

                            if (!string.IsNullOrEmpty(temp))
                            {

                                sb.Append(temp);
                            }
                        }
                    }
                    if (sb != null && sb.ToString() != "")
                        WriteLog(sb);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                running = false;
            }
        }

        private static string logFolder = System.AppDomain.CurrentDomain.BaseDirectory + "log";
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(StringBuilder message)
        {
            int i = 0;
            if (!string.IsNullOrEmpty(LogPath))
                logFolder = LogPath;
            if (message == null || message.ToString() == "") return;
            goHere:
            try
            {
                if (!Directory.Exists(logFolder + "\\" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    Directory.CreateDirectory(logFolder + "\\" + DateTime.Now.ToString("yyyyMMdd"));
                }
                String logFilePath = Path.Combine(logFolder + "\\" + DateTime.Now.ToString("yyyyMMdd"), "log" + DateTime.Now.ToString("yyyyMMddHH") + ".txt");//一天一个文件发现日志太大，还是一个小时一个文件
                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.Default))
                {
                    writer.Write(message.ToString());
                }
            }
            catch
            {
                Thread.Sleep(800);//失败往往是文件占用，可以停800毫秒
                if (i < 3)//做3次尝试，如果失败将会扔掉
                {
                    i += 1;
                    goto goHere;
                }
            }
            finally
            {
                if (tracingQueue.Count > 0)
                    get();
            }
        }

        private static string buildTrace(Trace trace)
        {
            //[{0}]\r\n[{5}]\r\n[Time]={1}\r\n[className]={2}\r\n[ExceptionMessage]={3}\r\n[ExceptionStackTrace]={4}\r\n{5}\r\n\r\n";
            //"{0}\r\n[{5}]\r\n[Time]={1}\r\n[className]={1}\r\n[ExceptionMessage]={2}\r\n[ExceptionStackTrace]={3}\r\n{4}
            if (trace == null) return null;
            try
            {
                return string.Format(info, trace.traceType.ToString(),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                    trace.classType == null ? "" : trace.classType.FullName,
                    trace.ex == null ? "" : trace.ex.Message,
                    trace.ex == null ? "" : trace.ex.StackTrace,
                    string.IsNullOrEmpty(trace.message) ? "" : "[Message]=" + trace.message
                     );
            }
            catch (Exception ex) { }
            return "";
        }

        #endregion

        public static void Info(StringBuilder stringBuilder)
        {
            throw new NotImplementedException();
        }
    }

    #region 内部类

    enum TraceType
    {
        /// <summary>
        /// 业务注释
        /// </summary>
        Info = 1,
        /// <summary>
        /// 警告(业务异常)
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 程序日志
        /// </summary>
        Error = 3,
    }

    class Trace
    {
        public Exception ex;

        public Type classType;

        public TraceType traceType;

        public string message;
    }

    #endregion

}