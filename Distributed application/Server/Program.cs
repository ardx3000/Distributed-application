using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Connection;
using Server.DataBase;
using Server.DataBase.Repository;
using Server.Logic;
using Server.Services;
using System.Text.RegularExpressions;

namespace Server
{
    class Program
    {
        //TODO Create a way for the client to login and have persistance.
        private static MenuUI _menu;

        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Create a scope to resolve services
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Resolve MenuUI service
                _menu = services.GetRequiredService<MenuUI>();

                // SENSITIVE DATA ONLY HARDCODED FOR DEMONSTRATION.
                byte[] key = { 0xd5, 0xa3, 0xd0, 0xc4, 0xcf, 0x72, 0xff, 0x6d, 0x64, 0xd1, 0xb8, 0xfd, 0x62, 0x4d, 0xc1, 0x43 };
                byte[] iv = { 0xb0, 0xa1, 0xc2, 0xd3, 0xe4, 0xf5, 0x67, 0x78, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00 };
                //-----------------------------------------------------------------------------------------------------------------

                var socketServer = new SocketServer(9999, key, iv);
                socketServer.DataReceived += Server_DataReceived;
                socketServer.Start();
                Console.WriteLine("(SERVER) Server is starting and listening to connections....");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Please use one of the follwoing commands");
                _menu.Help();

                while (true)
                {
                    Console.WriteLine("-------------------------------------------------------------");
                    string userInput = Console.ReadLine();
                    _menu.LocalOptions(userInput);
                }
            }

            // Run the host
            host.Run();
        }

        //TODO update the method to parse string and act on different commands 

        private static void Server_DataReceived(object sender, string data)
        {
            Console.WriteLine($"Data received: {data}");

            _menu.ServerOptions(data);
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlite("Data Source=C:\\Users\\ardx3\\Documents\\GitHub\\Distributed-application\\Distributed application\\Server\\DataBase\\DataBase.db"));

                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<ILogsRepository, LogsRepository>();
                    services.AddScoped<IItemRepository, ItemRepository>();

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<ILogsService, LogsService>();
                    services.AddScoped<IItemService, ItemService>();

                    services.AddScoped<LoginManagementLogic>();
                    services.AddScoped<LocalLogic>();
                    services.AddScoped<ItemsManagementLogic>();

                    services.AddSingleton<MenuUI>();
                });
    }
}
