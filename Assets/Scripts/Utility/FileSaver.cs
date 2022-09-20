using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using UnityEngine;

public class FileSaver
{
    public void saveToFile(int symbol_id, string record)
    {
        string filename = symbol_id.ToString() + ".txt";
        Debug.Log("Save to file: " + filename);
        File.AppendAllText(filename, record);
    }
}

public class EmailSender
{
    const string address = "dummyemailforsymbols@gmail.com";
    const string password = "Abrakadabra01";
    MailMessage mail_message;
    List<string> attachment_filenames;

    public EmailSender()
    {
        mail_message = new MailMessage();
        attachment_filenames = new List<string>();

        mail_message.From = new MailAddress(address);
        mail_message.To.Add(address);
        mail_message.Subject = "Symbols";
        mail_message.Body = "attached symbols";
    }

    ~EmailSender()
    {
        Debug.Log("Delete files");
        //for (int i = 0; i < attachment_filenames.Count; i++)
        //{
        //    if (File.Exists(attachment_filenames[i]))
        //    {
        //        File.Delete(Directory.GetCurrentDirectory() + @"\" + attachment_filenames[i]);
        //        Debug.Log("Deleted file: " + attachment_filenames[i]);
        //    }
        //}
    }

    public void addAttachment(string filename)
    {
        System.Net.Mail.Attachment attachment;
        if (File.Exists(filename))
        {
            attachment = new System.Net.Mail.Attachment(filename);
            mail_message.Attachments.Add(attachment);
            attachment_filenames.Add(filename);
        }
    }

    public void sendEmail()
    {
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); 
        SmtpServer.Port = 465;
        SmtpServer.Credentials = new System.Net.NetworkCredential("dummyemailforsymbols@gmail.com", "Abrakadabra01");
        SmtpServer.EnableSsl = true;

        for (int i = 0; i < 12; i++)
        {
            addAttachment(i.ToString() + ".txt");
        }

        SmtpServer.Send(mail_message);
    }
}
