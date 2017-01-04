using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Jobs
{
    public class SendNotification : IJob
    {
        private static ApplicationDbContext Db => new ApplicationDbContext(
            (new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(Program.CONNECTION_STRING)).Options);

        public async Task Execute(IJobExecutionContext context)
        {
            //await SendEmail();
            var listEvents = await GetComingEvents();
            List<Task> listTasks = new List<Task>();
            foreach (var e in listEvents)
                listTasks.Add(SendEmail(e.Account.FullName, e.Account.Email, e.EventName, e.Description, e.Start, e.Location));
            await Task.WhenAll(listTasks);
        }

        public async Task<List<Event>> GetComingEvents()
        {
            var now = DateTime.Now;
            //get event from now to future less than 1 minute
            return await Db.Events.Include(e => e.Account)
                .Where(e => e.Start >= now && e.Start.Subtract(now) < TimeSpan.FromMinutes(1))
                .ToListAsync();
        }

        public async Task SendEmail(string name, string email, string eventName, string eventDescription, DateTime time,string location)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ToDo", "thanhgola@yahoo.com"));
            emailMessage.To.Add(new MailboxAddress("thanh", "thanhgola@gmail.com"));
            emailMessage.Subject = "test mailkit";
            emailMessage.Body = new TextPart("plain") { Text = "hello gmail, this is yahoo mail" };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.mail.yahoo.com", 587);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(new NetworkCredential("thanhgola@yahoo.com", "titanrock"));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch
                {

                }
            }
        }
    }
}
