using Server.DataBase.Entity;
using Server.Services;
using System.IO.Pipes;
using System.Text.Json;

namespace Server.Connection
{
    public class NamedPipeServer
    {
        private NamedPipeServerStream pipeServer;
        private readonly IUserService _userService;

        public NamedPipeServer(IUserService userService)
        {
            _userService = userService;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                try
                {
                    pipeServer = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 1);
                    Console.WriteLine("Named pipe server is waiting for connection...");

                    pipeServer.WaitForConnection();
                    Console.WriteLine("Named pipe server connected.");

                    using (StreamReader reader = new StreamReader(pipeServer))
                    using (StreamWriter writer = new StreamWriter(pipeServer) { AutoFlush = true })
                    {
                        while (true)
                        {
                            string data = reader.ReadLine();
                            if (data != null)
                            {
                                Console.WriteLine($"Received from Electron: {data}");
                                // Process data received from Electron as needed

                                var user = JsonSerializer.Deserialize<User>(data);
                                if (user != null)
                                {
                                    // Add user to the database or process it as needed
                                    _userService.CreateUser(user);
                                    writer.WriteLine($"User {user.Username} added successfully!");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Named pipe server error: {ex.Message}");
                }
                finally
                {
                    pipeServer?.Disconnect();
                    pipeServer?.Close();
                }
            });
        }

        public void Stop()
        {
            pipeServer?.Disconnect();
            pipeServer?.Close();
        }
    }
}
