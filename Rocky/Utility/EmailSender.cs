﻿using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("2ae57c33b6cfa2c2bf1912b7d8660f7e", "973635421353cb32c5d200b708254cee");

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
             new JObject {
                        {
                        "From",
                        new JObject {
                        {"Email", "drendagon@gmail.com"},
                        {"Name", "Dren"}
                        }
                        }, {
                        "To",
                        new JArray {
                        new JObject {
                            {
                            "Email",
                            email
                            }, {
                            "Name",
                            "DotNetMastery"
                            }
                        }
                        }
                        }, {
                        "Subject",
                        subject
                        }, {
                        "HTMLPart",
                        body
                        }
             }
             });
            await client.PostAsync(request);
        }
    }
}
