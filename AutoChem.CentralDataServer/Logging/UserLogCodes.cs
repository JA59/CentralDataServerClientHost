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
**    Copyright © 2012 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Logging
{
    /// <summary>
    /// Codes used for log entries.  Note code may be reused across various messages in cases where the messages are mutually exclusive.
    /// That is only one of the messages with that code can be produced at any point in time.
    /// </summary>
    public static class UserLogCodes
    {
        /// <summary>
        /// Used for informational entries that are never superseded by similar entries e.g. start and stop of server, configuration changed.
        /// </summary>
        public static readonly int Uncoded = -1;
        /// <summary>
        /// Used for entries related to diskspace.
        /// </summary>
        public static readonly int Diskspace = 2;
        /// <summary>
        /// Used for entries related to errors with the root experiment folder.
        /// </summary>
        public static readonly int ExperimentsFolder = 3;
        /// <summary>
        /// Used for entries related to a particular experiment folder.
        /// </summary>
        public static readonly int SingleExperimentFolder = 4;
        /// <summary>
        /// Used for entries related to the e-mail system.
        /// </summary>
        public static readonly int Email = 5;
        /// <summary>
        /// Used for entries related to instrument firmware.
        /// </summary>
        public static readonly int Firmware = 6;
        /// <summary>
        /// Used for entries related to server user impersonation to access network resources.
        /// </summary>
        public static readonly int Impersonation = 7;
        /// <summary>
        /// Used for entries related to experiment collection.
        /// </summary>
        public static readonly int ExperimentCollection = 8;
        /// <summary>
        /// Used for entries related to compatibility for applications uploading experiments.
        /// </summary>
        public static readonly int ApplicationCompatibility = 9;
        /// <summary>
        /// Used for entries related to errors with the experiment drop folder.
        /// </summary>
        public static readonly int ExperimentDropFolder = 10;
        /// <summary>
        /// Used for entries related to the Microsoft Word template.
        /// </summary>
        public static readonly int WordTemplate = 11;
        /// <summary>
        /// Used for entries related to the report template rxe instruments (e.g. OptiMax and EasyMax).
        /// </summary>
        public static readonly int RxeInstrumentReportTemplate = 12;
        /// <summary>
        /// Used for entries related to experiment processing
        /// </summary>
        public static readonly int ExperimentProcessing = 13;
        /// <summary>
        /// Used for entries related to licensing
        /// </summary>
        public static readonly int Licensing = 14;
        /// <summary>
        /// Used for entries related to write to disk error
        /// </summary>
        public static readonly int WriteError = 15;
        /// <summary>
        /// Used for entries related to importing planned experiments from the Import folder
        /// </summary>
        public static readonly int ImportDropFolderNotWellFormed = 16;
        /// <summary>
        /// Used for entries related to importing planned experiments from the Import folder
        /// </summary>
        public static readonly int ImportDropFolderNoSchemaVersion = 17;
        /// <summary>
        /// Used for entries related to importing planned experiments from the Import folder
        /// </summary>
        public static readonly int ImportDropFolderUnsupportedSchemaVersion = 18;
        /// <summary>
        /// Used for entries related to importing planned experiments from the Import folder
        /// </summary>
        public static readonly int ImportDropFolderValidationFailed = 19;
        /// <summary>
        /// Used for entries related to importing planned experiments from the Import folder
        /// </summary>
        public static readonly int ImportDropFolder = 20;
        /// <summary>
        /// Used for entries related to reading the S88 process definition file
        /// </summary>
        public static readonly int S88ProcessDefinition = 21;
    }
}
