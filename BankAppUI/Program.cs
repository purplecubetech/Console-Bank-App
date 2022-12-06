using System;
using BankAppCore.Implementations;
using BankAppCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace BankAppUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);
            services
               .AddSingleton<BankDisplay, BankDisplay>()
               .BuildServiceProvider()
               .GetService<BankDisplay>()
               .ShowBankDisplay();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IPrintTable, PrintTable>();
            services.AddScoped<IValidators, Validators>();
            services.AddTransient<IAccount, Accounts>();
            services.AddTransient<IBank, Bank>();


        }
    }
}
