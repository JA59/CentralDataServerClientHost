using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// Contains the e-mail related settings.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class EmailSettings
    {
        /// <summary>
        /// Default name for the email template
        /// </summary>
        public const string DefaultTemplateFileName = "EmailTemplate.html";
       
        /// <summary>
        /// SMTPServer URL
        /// </summary>
        [DataMember]
        public string SMTPServerURL { get; set; }

        /// <summary>
        /// Email account used to send with security
        /// </summary>
        [DataMember]
        public string SendersEmail { get; set; }

        /// <summary>
        /// Email HTML template path
        /// </summary>
        [DataMember]
        public string EmailHTMLTemplatePath { get; set; }

        /// <summary>
        /// Email account for all experiments notification
        /// </summary>
        [DataMember]
        public string AllExperimentsEmail { get; set; }

        /// <summary>
        /// Email account for sending error server messages
        /// </summary>
        [DataMember]
        public string ErrorNotificationEmail { get; set; }

        /// <summary>
        /// SMTP required encryption
        /// </summary>
        [DataMember]
        public bool SequirityRequired { get; set; }

        /// <summary>
        /// SMTP user name
        /// </summary>
        [DataMember]
        public string SMTPUserId { get; set; }

        /// <summary>
        /// SMTP user Password
        /// </summary>
        [DataMember]
        public string SMTPUserPassword { get; set; }

        /// <summary>
        /// EmailDomain
        /// </summary>
        [DataMember]
        public string EmailDomain { get; set; }
        
        /// <summary>
        /// Send email notifications to user when value is true
        /// </summary>
        [DataMember]
        public bool SendNotifications { get; set; }

        /// <summary>
        /// Test Email Address
        /// </summary>
        [DataMember]
        public string TestEmailAddress { get; set; }

        /// <summary>
        /// Set true to enable email server
        /// </summary>
        [DataMember]
        public bool EnableEmailCommunication { get; set; }

    }
}
