/*
**
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2011 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoChem.Core.CentralDataServer
{
    /// <summary>
    /// A helper class with functions for working with services.
    /// </summary>
    public static class ServicesHelper
    {
        /// <summary>
        /// The base namespace used for webservice services and types.  Note this needs to be constant to work in attributes.
        /// </summary>
        private const string BaseNameSpace = "http://schemas.mt.com/AutoChem/CentralDataServer";

        /// <summary>
        /// The namespace used for webservice services.  Note this needs to be constant to work in attributes.
        /// </summary>
        public const string ServiceNameSpace = BaseNameSpace + "/Services";

        /// <summary>
        /// The namespace used for webservice types.  Note this needs to be constant to work in attributes.
        /// </summary>
        public const string TypeNameSpace = BaseNameSpace + "/Types";

        /// <summary>
        /// Takes a url and attempts to put it in the form expected by the system.
        /// </summary>
        public static string NormalizeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }
            url = url.Trim();

            if (!HasProtocol(url))
            {
                // Add http:// to the beginning also trim and backslashes from the start in case the user enters the machine like a UNC.
                url = "http://" + url.TrimStart('\\');
            }

            // trim off any / at the end as they will be appended as neccessary.
            url = url.TrimEnd('/');

            return url;
        }

        /// <summary>
        /// Takes a url and attempts to put it in the form expected by the system when using a Ping command.  
        /// Ping does not allow the url to have "http://" or "\\"
        /// </summary>
        public static string NormalizeUrlForPing(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }
            url = url.Trim();

            if (HasProtocol(url))
            {
                // Add http:// to the beginning also trim and backslashes from the start in case the user enters the machine like a UNC.
                url = url.Replace("http://", "");
                url = url.TrimStart('\\', '/');
            }

            // Remove the path because for ping we just want the server.
            var slashIndex = url.IndexOf('/');
            if (slashIndex != -1)
            {
                url = url.Substring(0, slashIndex);
            }

            return url;
        }

        private static bool HasProtocol(string url)
        {
            Regex hasProtocolRegex = new Regex("^[a-z]+[:][/][/]");
            return hasProtocolRegex.IsMatch(url);
        }

        /// <summary>
        /// Gets the default binding for WPF WCF communications.
        /// </summary>        
        public static Binding GetDefaultBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            binding.MaxReceivedMessageSize = 16000000;
            binding.ReaderQuotas.MaxArrayLength = 16000000;
            binding.ReceiveTimeout = TimeSpan.FromMinutes(15);
            binding.SendTimeout = TimeSpan.FromMinutes(15);

            return binding;
        }
    }
}
