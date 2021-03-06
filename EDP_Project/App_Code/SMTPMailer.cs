﻿using System;
using System.Collections.Generic;
using System.Net;

/* Unmerged change from project '3_App_Code'
Before:
using System.Web;

using System.Net.Mail;
using System.Net;
After:
using System.Net;
using System.Net.Mail;
using System.Web;
*/
using System.Net.Mail;

namespace EDP_Project.App_Code
{
    public class SMTPMailer
    {
        MailMessage Msg = new MailMessage();
        SmtpClient Client = new SmtpClient();

        public Boolean addEmail(String value)
        {
            try
            {
                Msg.From = new MailAddress("191382S@mymail.nyp.edu.sg", "Wei Rong");
                Msg.To.Add(value);
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }
        public Boolean addCC(List<String> values)
        {
            Boolean BreakIt = false;
            foreach (String value in values)
            {
                try
                {
                    Msg.CC.Add(new MailAddress(value));
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    BreakIt = true;
                }
                if (BreakIt == true)
                {
                    break;
                }
            }

            if (BreakIt == true)
            {
                return !BreakIt;
            }
            else
            {
                return BreakIt;
            }

        }
        public Boolean addBCC(List<String> values)
        {
            Boolean BreakIt = false;
            foreach (String value in values)
            {
                try
                {
                    Msg.Bcc.Add(new MailAddress(value));
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    BreakIt = true;
                }
                if (BreakIt == true)
                {
                    break;
                }
            }

            if (BreakIt == true)
            {
                return !BreakIt;
            }
            else
            {
                return BreakIt;
            }

        }
        public Boolean addSubject(String value)
        {
            try
            {
                Msg.Subject = value;
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }
        public Boolean addBody(String value)
        {
            try
            {
                Msg.Body = value;
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }
        public void SetHTML(Boolean value)
        {
            Msg.IsBodyHtml = value;
        }

        public Boolean sendEmail()
        {
            Client.Port = 587;
            Client.Host = "smtp-mail.outlook.com";
            Client.EnableSsl = true;
            Client.UseDefaultCredentials = false;
            Client.Credentials = CredentialCache.DefaultNetworkCredentials;
            Client.Credentials = new NetworkCredential("191382S@mymail.nyp.edu.sg", "NY21k9M2H6L0b4a");
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Client.Send(Msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}