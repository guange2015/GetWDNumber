using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Threading;

namespace GetWDNumber
{
    class Export
    {
        const string sResponseEncoding = "gb2312";
        private static Mutex m = new Mutex();

        public static void write2txt(string logfile, string content)
        {
            m.WaitOne();
            FileStream file = File.Open(logfile, FileMode.Append);
            byte[] data = Encoding.GetEncoding(sResponseEncoding).GetBytes(content+"\r\n");
            file.Write(data, 0, data.Length);
            file.Close();
            m.ReleaseMutex();
        }
    }
}
