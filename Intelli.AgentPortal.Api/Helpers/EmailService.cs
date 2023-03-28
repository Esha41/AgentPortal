using Intelli.AgentPortal.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Api.Helpers
{
    /// <summary>
    /// The email sender.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="optionsAccessor">The configuration options accessor.</param>
        /// <param name="logger">For logging email sending issues.</param>
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        /// <summary>
        /// Gets the configuration options.
        /// </summary>
        public AuthMessageSenderOptions Options { get; }

        /// <summary>
        /// Sends the email async.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>A Task.</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        /// <summary>
        /// Executes the email sending using send grid.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="email">The email.</param>
        /// <returns>A Task.</returns>
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Options.SenderEmail, Options.SendGridUser);
            var to = new EmailAddress(email);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);

            return response;
        }

        /// <summary>
        /// Execute the email sending using send grid.
        /// Alternate Option: Uses sendgrid relay.
        /// </summary>
        /// <param name="apiKey">The api key.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="email">The email.</param>
        /// <returns>A Task.</returns>
        public Task Execute2(string apiKey, string subject, string message, string email)
        {
            try
            {
                var mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(email));

                // From
                mailMsg.From = new MailAddress(Options.SenderEmail, Options.SendGridUser);

                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                string text = message;
                string html = message;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                SmtpClient smtpClient = new("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new("apikey", apiKey);
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send email to {0}", email);
            }

            return Task.CompletedTask;
        }
    }
}