using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sem2Task2
{       
     internal class Server
     {
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public static CancellationToken ct = cts.Token;
        private static bool exitRequested = false;

        public static async Task msgServ()
        {            
            Task t = new Task(AcceptMsg, ct);
            t.Start();

            try
            {
                await t;                
            }
            catch (OperationCanceledException e)
            {
                if (t.IsCanceled)
                {                    
                    Console.WriteLine("Задача завершена");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Задача не была завершена");
                    Console.ReadLine();
                };
            }
            t.Wait();
        }
        public static void AcceptMsg()
        //public static async Task AcceptMsg() 
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(5050);
            Console.WriteLine("Сервер ожидает сообщение. Нажмите <Esc> для выхода: ");
            //Запустим задачу для ожидания нажатия клави


            Task exitTask = Task.Run(() =>
            {
                Console.WriteLine("Жду сообщения для exitTask");
                Console.ReadKey();
                exitRequested = true;
                cts.Cancel();                
            });

            //exitTask.Wait();


            while (!ct.IsCancellationRequested)//while (!exitRequested)
            {
                Console.WriteLine("возможно    здесь ставить exitRequested = true ");
                var data = udpClient.Receive(ref ep);
                string data1 = Encoding.UTF8.GetString(data);
                Task.Run(() =>
                {
                    Message msg = Message.FromJson(data1);
                    Console.WriteLine(msg.ToString());
                    Message responseMsg = new Message("Server", "Message accept on serv!");
                    string responseMsgJs = responseMsg.ToJson();
                    byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                    udpClient.Send(responseDate, responseDate.Length, ep);
                });

                //Дождитесь завершения задачи по нажатию клавиши
                //exitTask.Wait();
            }
            //ct.ThrowIfCancellationRequested();

        }
    }
}
