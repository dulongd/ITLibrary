﻿/*
File Name: EmailSender.cs
Description: Sending emails to the user after checked out the books.
Author: Savitha , Kavitha
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinalProject.Service
{
    public class EmailSender : IEmailSender { 
        public EmailOptions Options { get; set; }
    
        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
          Options = emailOptions.Value;
        }
        public Task SendEmailAsync(string email, string subject,string message)
        {
            return Excute(Options.SendGridKey, subject, message, email);
        }

        private Task Excute(string sendGridKey, string subject, string message, string email)
        {
           
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("danielledulong@gmail.com", "ItLibrary"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            
            msg.AddTo(new EmailAddress(email));

            try
            {
                return client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}
