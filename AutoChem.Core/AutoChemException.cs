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
**    Copyright © 2006 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#if !SILVERLIGHT
using AutoChem.Core.Serialization;
#endif

namespace AutoChem.Core
{
    /// <summary>
    /// Defines a delegate for handling exceptions.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    public delegate void ExceptionHandler(Exception exception);

    /// <summary>
    /// Our basic exception for AutoChem
    /// </summary>
    [Serializable]
    public class AutoChemException :
#if !SILVERLIGHT
        ApplicationException, ISerializable
#else
        Exception
#endif
    {
#if !SILVERLIGHT
        private const int Version = 3;
#endif
        private const string DefaultMsg = "An unexpected error was encountered.";
        private AutoChemExceptionSeverity autoChemExceptionSeverity;

        /// <summary>
        /// Create a new instance of AutoChemException with a default message.
        /// </summary>
        public AutoChemException()
            : base(DefaultMsg)
        {
            this.autoChemExceptionSeverity = AutoChemExceptionSeverity.UnexpectedError;
        }

        /// <summary>
        /// Craetes a new instance of AutoChemException with a message.
        /// </summary>
        /// <param name="msg">The message to include with the exception</param>
        public AutoChemException(string msg)
            : base(msg)
        {
            this.autoChemExceptionSeverity = AutoChemExceptionSeverity.UnexpectedError;
        }

        /// <summary>
        /// Create a new AutoChemException with an inner exception and a default
        /// message.
        /// </summary>
        /// <param name="e">The inner exception</param>
        public AutoChemException(Exception e)
            : base(e.Message, e)
        {
            this.autoChemExceptionSeverity = AutoChemExceptionSeverity.UnexpectedError;
        }

        /// <summary>
        /// Create a new AutoChemException with a message and inner exception.
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="e">inner exception</param>
        public AutoChemException(string msg, Exception e)
            : base(msg, e)
        {
            this.autoChemExceptionSeverity = AutoChemExceptionSeverity.UnexpectedError;
        }

        /// <summary>
        /// Create a new AutoChemException with a severtiy and a default
        /// message.
        /// </summary>
        /// <param name="severity">The inner exception</param>
        public AutoChemException(AutoChemExceptionSeverity severity)
            : base(DefaultMsg)
        {
            this.autoChemExceptionSeverity = severity;
        }

        /// <summary>
        /// Create a new AutoChemException wtih a severtiy and a message.
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="severity">severity</param>
        public AutoChemException(string msg, AutoChemExceptionSeverity severity)
            : base(msg)
        {
            this.autoChemExceptionSeverity = severity;
        }

        /// <summary>
        /// Create a new AutoChemException wtih a severtiy, inner exception,
        /// and a default message.
        /// </summary>
        /// <param name="e">inner exception</param>
        /// <param name="severity">severity</param>
        public AutoChemException(Exception e, AutoChemExceptionSeverity severity)
            : base(e.Message, e)
        {
            this.autoChemExceptionSeverity = severity;
        }

        /// <summary>
        /// Create a new AutoChemException wtih a severtiy, inner exception,
        /// and a message.
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="e">inner exception</param>
        /// <param name="severity">severity</param>
        public AutoChemException(string msg, Exception e, AutoChemExceptionSeverity severity)
            : base(msg, e)
        {
            this.autoChemExceptionSeverity = severity;
        }

#if !SILVERLIGHT
        /// <summary>
        /// The is used for serialization.
        /// </summary>
        /// <param name="info">serialization information</param>
        /// <param name="context">serialization context</param>
        protected AutoChemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            string prefix = SerializationUtility.GetSerializationPrefix(this, typeof(AutoChemException));

            int version;
            try
            {
                version = info.GetInt32(prefix + "Version");
            }
            catch
            {
                version = 0;
            }

            switch (version)
            {
                case 0:
                case 1:
                    autoChemExceptionSeverity = AutoChemExceptionSeverity.UnexpectedError;
                    break;
                case 2:
                    autoChemExceptionSeverity = (AutoChemExceptionSeverity)(int)info.GetValue(
                        prefix + "VLExceptionSeverity", typeof(int));
                    break;
                case 3:
                    autoChemExceptionSeverity = (AutoChemExceptionSeverity)(int)info.GetValue(
                        prefix + "AutoChemExceptionSeverity", typeof(int));
                    break;
                default:
                    throw new SerializationException("Unknown version of AutoChemException: " + version.ToString());
            }
        }

        /// <summary>
        /// The is used for serialization.
        /// </summary>
        /// <param name="info">serialization information</param>
        /// <param name="context">serialization context</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            string prefix = SerializationUtility.GetSerializationPrefix(this, typeof(AutoChemException));

            info.AddValue(prefix + "Version", Version);
            info.AddValue(prefix + "AutoChemExceptionSeverity", (int)autoChemExceptionSeverity);
        }
#endif

        /// <summary>
        /// Creates a more detailed message than cantained in the message.
        /// </summary>
        public virtual string FormattedMessage
        {
            // Until further sophistication is acheived, we'll just
            // return whatever is in the exeception and the inner exception.
            // Derived classes should override this to add more juicy tidbits of info.
            get { return ConcatExceptionMessages(this); }
        }

        /// <summary>
        /// Indicates the severity of the exception.
        /// </summary>
        public AutoChemExceptionSeverity AutoChemExceptionSeverity
        {
            get { return autoChemExceptionSeverity; }
        }

        /// <summary>
        /// Creates a single string from an exception and all of its inner exceptions. As each inner exception message
        /// is concatenated to its parent message, it is numbered and placed on a new line
        /// </summary>
        /// <param name="ex">the parent exception</param>
        /// <returns></returns>
        public static string ConcatExceptionMessages(Exception ex)
        {
            string text = "";

            int i = 1;
            Exception nextException = ex;

            do
            {
                string msg = nextException.Message.Replace("\n", "\r\n");
                text += string.Format("{0}) {1}\r\n", i++, msg);
                nextException = nextException.InnerException;
            }
            while (nextException != null);

            return text;
        }
    }
}
