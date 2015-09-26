﻿using SYDQ.Core;
using SYDQ.Repository.EF;
using SYDQ.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacBooter.Run();
            DbInterception.Add(new EFIntercepterLogging());

            User newUser = new User()
            {
                Username = "guoxin.qiu",
                EmailAddress = "guoxin.qiu@pactera.com",
                CreatedUtc = DateTime.Now,
                Roles = new List<Role>() { new Role { Name = "Admin" } },
                Messages = new List<EmailMessage>() { new EmailMessage { Body = "aaa", Subject = "oye", ToUserKey = 1 } }
            };

            IUserService userService = AutofacBooter.GetInstance<IUserService>();
            userService.AddUser(newUser);

            var usersFromDb = userService.GetAllUsers();
            foreach (User u in usersFromDb)
            {
                Console.WriteLine("---------start-----------");
                Console.WriteLine(u.ToString());
                Console.WriteLine("Messages count: " + u.Messages.Count());
                Console.WriteLine("Roles count: " + u.Roles.Count());
                Console.WriteLine("---------end-----------");
            }

            Console.WriteLine(Environment.NewLine + "press any key to exit.");
            Console.ReadKey();
        }
    }
}
