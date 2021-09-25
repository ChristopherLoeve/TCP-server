using FootballPlayerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_server
{
    public class Server
    {
        private static List<FootballPlayer> _players = new List<FootballPlayer>() {
            new FootballPlayer(0, "Henrik", 1000, 15),
            new FootballPlayer(1, "Peter", 800, 4),
            new FootballPlayer(2, "Lars", 1250, 9),
            new FootballPlayer(3, "Daniel", 25, 16)
        };


        public Server()
        {

        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 2121);
            listener.Start();

            while (true)
            {
                TcpClient tempSocket = listener.AcceptTcpClient();
                Task.Run(() => DoClient(tempSocket));
            }
        }



        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.WriteLine("Valid requests: HentAlle, Hent, Gem\nType your request:");
                sw.Flush();
                String str = sr.ReadLine();
                switch (str.ToLower())
                {
                    case "hentalle":
                        sw.WriteLine("Send any line to confirm");
                        sw.Flush();
                        sr.ReadLine();
                        sw.WriteLine(GetPlayers());
                        sw.Flush();
                        break;


                    case "hent":
                        sw.WriteLine("Enter Id of player you wish to get");
                        sw.Flush();
                        try
                        {
                            int id = Convert.ToInt32(sr.ReadLine());
                            sw.WriteLine(GetPlayer(id));
                            sw.Flush();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            sw.WriteLine("Input wasn't a number");
                            sw.Flush();
                        }
                        break;


                    case "gem":
                        sw.WriteLine("Enter your player as a JSON string");
                        sw.Flush();
                        str = sr.ReadLine();
                        try
                        {
                            SavePlayer(str);
                            sw.Flush();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            sw.WriteLine("Not a valid JSON string");
                            sw.Flush();
                        }
                        break;

                    default:
                        sw.WriteLine("Not a valid response\n\n");
                        sw.Flush();
                        break;
                }
            }
        }

        private string GetPlayers()
        {
            return JsonSerializer.Serialize<List<FootballPlayer>>(_players);
        }

        private string GetPlayer(int id)
        {
            FootballPlayer player = _players.Find(p => p.Id == id);
            return JsonSerializer.Serialize<FootballPlayer>(player);
        }

        private void SavePlayer(string jsonObject)
        {
            _players.Add(JsonSerializer.Deserialize<FootballPlayer>(jsonObject));
        }
    }
}
