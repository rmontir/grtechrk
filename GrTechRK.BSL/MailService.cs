using GrTechRK.BSL.Interfaces;
using GrTechRK.Database.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace GrTechRK.BSL
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }

        public void SendAsyc(string to, Employee employee)
        {
            MailAddress recipient = new MailAddress(to);
            MailAddress sender = new MailAddress("admin@grtech.com.my");

            MailMessage message = new MailMessage(sender, recipient)
            {
                Subject = "New employee is registered",
                Body = $"Employee {employee.FirstName} {employee.LastName} is registered with email {employee.Email}"
            };

            SmtpClient client = new SmtpClient(_configuration["MailTrap:Host"], Convert.ToInt32(_configuration["MailTrap:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["MailTrap:Username"], _configuration["MailTrap:Password"]),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException)
            {
                throw new InvalidOperationException("Send mail is failed");
            }
        }
    }
}
