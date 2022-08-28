using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.IO;

namespace PING001
{
    public class PING
    {
        private Ping myPing;
        private PingOptions myPingOptions;
        private string data;
        private string myPath;
        private string myLogFile;
        private byte[] buffer;
        private int myLength;
        private DateTime CurrentTime;
        private StreamWriter myStream;
        private PingReply reply;
        public PING(string[] args)
        {
            myPing = new Ping();
            myPingOptions = new PingOptions();
            data = "icmp_send_request";
            myPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            myLogFile = @"\AccessLog.txt";
            buffer = Encoding.ASCII.GetBytes(data);
            myLength = args.Length;
            myStream = new StreamWriter(myPath + myLogFile);
            CurrentTime = DateTime.Now;
        }

        public void ShowPath()
        {
            Console.WriteLine("Путь к папке \"Мои документы\" текущего пользователя {0}", myPath);
        }
        public void ShowAnswer(string[] args)
        {
            for (int i = 0; i < myLength; i++)
            {
                myPingOptions.DontFragment = false;
                reply = myPing.Send(args[i], 120, buffer, myPingOptions);
                if (reply.Status.ToString() == "Success")
                {
                    Console.WriteLine("{0} узел {1} доступен ip = {2}", CurrentTime, args[i], reply.Address.ToString());
                }
                else
                {
                    Console.WriteLine("{0} узел {1} доступен", CurrentTime, args[i]);
                }
            }
        }
        public void FileAnswer(string[] args)
        {
            for (int i = 0; i < myLength; i++)
            {

                myPingOptions.DontFragment = false;
                reply = myPing.Send(args[i], 120, buffer, myPingOptions);
                if (reply.Status.ToString() == "Success")
                {
                    myStream.WriteLine("{0} узел {1} доступен ip = {2}", CurrentTime, args[i], reply.Address.ToString());
                }
                else
                {
                    myStream.WriteLine("{0} узел {1} доступен", CurrentTime, args[i]);
                }
            }
            myStream.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PING _ping = new PING(args);
                _ping.ShowPath();
                _ping.ShowAnswer(args);
                _ping.FileAnswer(args);
            }
            catch
            {
                Console.WriteLine("Введите аргументы командной строки");
            }
        }
    }
}

