using Intelli.AgentPortal.Api.Services.Email.Impl;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Helpers
{
    public class SmtpEmailSender : IEmailSender
    {

        private readonly ILogger<SmtpEmailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="optionsAccessor">The configuration options accessor.</param>
        /// <param name="logger">For logging email sending issues.</param>
        public SmtpEmailSender(IOptions<SmtpEmailSettings> optionsAccessor,
            ILogger<SmtpEmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        /// <summary>
        /// Gets the configuration options.
        /// </summary>
        public SmtpEmailSettings Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute( email, subject, message);
        }

        private async Task Execute(string to, string subject, string body)
        {
            

            var client = new SmtpClient
            {
                Port = Options.Port,
                Host = Options.Host,
                EnableSsl = Options.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = Options.UseDefaultCredentials,
                Credentials = new NetworkCredential(Options.FromEmailUsername, Options.FromEmailPassword)
            };

            var mm = new MailMessage()
            {
                From = new MailAddress(Options.From, Options.DisplayName),
                BodyEncoding = UTF8Encoding.UTF8,
                IsBodyHtml = true,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                Subject = subject,
                Body = body
            };

            mm.To.Add(new MailAddress(to));
            if (!Options.EnableSsl) client.EnableSsl = false;
            try
            {
                _logger.LogInformation("Sending a new email with subject '{MailSubject}' to '{MailRecipients}'..." , mm.Subject, string.Join(",", to));
                await client.SendMailAsync(mm);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, " Unable to send email : {Error}", ex.ToString());

                throw;
            }

           
        }
    }
}
