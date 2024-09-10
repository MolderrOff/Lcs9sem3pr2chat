using System;
using System.Threading;
using System.Threading.Tasks;
using static Sem2Task2.Server;

namespace Sem2Task2
{
    internal class Program
    {
        //public static CancellationTokenSource cts = new CancellationTokenSource();
        //public static CancellationToken ct = cts.Token;
        static async Task Main(string[] args)
        {        

            if (args.Length == 0)
            {

                await msgServ();
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    await Client.SendMsg(args[0], i);

                    break;
                    //Console.WriteLine("Нажмите <Exit> (Program.cs) для выхода:");
                    //string endProg = Console.ReadLine();

                    //if (endProg == "Exit")
                    //{
                    //    //trClient.Join();
                        
                    //    break;
                    //}


                }


            }


        }
    }
}
