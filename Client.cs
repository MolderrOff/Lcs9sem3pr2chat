using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sem2Task2
{
    internal class Client
    {
        public static async Task SendMsg(string name, int numberOfThread)//        public static void SendMsg(string name)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5050);
            UdpClient udpClient = new UdpClient();




            while (true)
            {
                Console.WriteLine("Введите сообщение или <Exit> для выхода из Client.cs (внутри while (true):");
                string text = Console.ReadLine();

                if (text.ToLower() == "exit")
                {
                    Console.WriteLine($"Введено {text}");
                    break;
                }



                string doubleString = numberOfThread.ToString() + text + " Привет";
                Message msg = new Message(name, doubleString);//            Message msg = new Message(name,  "Привет");
                string responseMsgJs = msg.ToJson();
                byte[] responseData = Encoding.UTF8.GetBytes(responseMsgJs);
                await udpClient.SendAsync(responseData, responseData.Length, ep); // я добавил

                byte[] answerData = udpClient.Receive(ref ep);
                
                string answerMsgJs = Encoding.UTF8.GetString(answerData);
                Message answerMsg = Message.FromJson(answerMsgJs);
                Console.WriteLine(answerMsg.ToString());



            }

        }



    }
}
