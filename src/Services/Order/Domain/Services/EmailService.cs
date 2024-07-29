using Domain.Configurations;
using Domain.Models;
using MailKit.Net.Smtp;
using MimeKit;
using TesodevOrderApp.Shared.Domain.Constants;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailconfig)
        {
            _emailConfig = emailconfig;
        }

        public async Task SendEmailAsync(List<Order> orders, string name, string address)
        {
            foreach (var order in orders)
            {
                var message = CreateEmailMessage(order, name, address);

                using var client = new SmtpClient();
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailMessage(Order order, string name, string address)
        {
            var content = $"Order No: {order.Id}, Quantity: {order.Quantity}, Price: {order.Price}TL, Status: {order.Status}, Product: {order.Product.Name}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfig.Name, _emailConfig.From));
            message.To.Add(new MailboxAddress(name, address));
            message.Subject = Constants.MailMessage.MessageSubject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = content };

            return message;
        }
    }
}
