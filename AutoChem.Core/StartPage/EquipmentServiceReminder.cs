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
**    Copyright © 2008 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Runtime.Serialization;

namespace AutoChem.Core.StartPage
{
    /// <summary>
    /// Service reminder that is used for a certain piece of hardware
    /// The piece of hardware that needs service must have a name or id for identification.
    /// </summary>
    [DataContract]
    [KnownType(typeof(ServiceReminder))]
    [KnownType(typeof(EquipmentServiceReminder))]
    public class EquipmentServiceReminder : ServiceReminder
    {
        [DataMember(Order = 0, Name = "EquipmentName", IsRequired = true)]
        private readonly string m_EquipmentName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType">Unique identifier for a type of service activity.</param>
        /// <param name="equipmentName">ID of a piece of hardware.</param>
        /// <param name="nextServiceDate">Due date for the next servicing.</param>
        /// <param name="serviceInterval">Interval between services in days.</param>
        /// <param name="preServiceDelta">Days prior to service date when an information reminder is required.</param>
        /// <param name="postServiceDelta">Days after service date until a warning reminder is required.</param>
        /// <param name="reminderText">Text to display in the reminder on the start page.</param>
        /// <param name="fullText">Full text to display on the service form.</param>
        /// <param name="helpFile">Help file containing associated help.</param>
        /// <param name="helpTopic">Help topic for associated help.</param>
        public EquipmentServiceReminder(string serviceType, string equipmentName,
            DateTime nextServiceDate, int serviceInterval, int preServiceDelta,
            int postServiceDelta, string reminderText, string fullText,
            string helpFile, string helpTopic)
            : base(serviceType,
                nextServiceDate, serviceInterval, preServiceDelta,
                postServiceDelta, reminderText, fullText,
                helpFile, helpTopic)
        {
            m_EquipmentName = equipmentName;
        }

        /// <summary>
        /// Identifier of the piece of hardware this reminder is for.
        /// </summary>
        public string EquipmentName
        {
            get { return m_EquipmentName; }
        }

        /// <summary>
        /// Unique identifier for the service reminder.
        /// </summary>
        public override string ID
        {
            get
            {
                return CreateID(ServiceType, EquipmentName);
            }
        }

        /// <summary>
        /// Method that creates the key based on <paramref name="serviceType"/> and <paramref name="equipmentName"/>
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="equipmentName"></param>
        /// <returns></returns>
        public static string CreateID(string serviceType, string equipmentName)
        {
            return string.Format("{0}_{1}", serviceType, equipmentName);
        }
    }
}
