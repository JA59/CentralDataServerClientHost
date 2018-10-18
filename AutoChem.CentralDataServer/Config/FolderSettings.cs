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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Applications;
using AutoChem.Core.CentralDataServer.Experiments;
using System.ComponentModel;
using System.Text.RegularExpressions;
using AutoChem.Core.CentralDataServer.Instruments;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// Stores the settings concerning folders and how experiments are organized into folders.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class FolderSettings
    {
        private List<String> _SubFolders;
        private List<String> _DropFolderFileExtensions;

        /// <summary>
        /// Default value for ImportDropFolderCountLimit
        /// </summary>
        public const int DefaultImportDropFolderCountLimit = 1000;

        /// <summary>
        /// Max value for ImportDropFolderDaysLimit
        /// </summary>
        public const int DefaultImportDropFolderDaysLimit = 30;

        /// <summary>
        /// Default value for ImportDropFolderCountLimit
        /// </summary>
        public const int MaxImportDropFolderCountLimit = 5000;

        /// <summary>
        /// Max value for ImportDropFolderDaysLimit
        /// </summary>
        public const int MaxImportDropFolderDaysLimit = 365;

        /// <summary>
        /// Creates a new FolderSettings
        /// </summary>
        public FolderSettings()
        {
            _SubFolders = new List<string>();
            _SubFolders.Add(ConfigurationSettings.ExperID); 
            _SubFolders.Add(ConfigurationSettings.ExperFileName);
            _DropFolderFileExtensions = new List<string>();
            FileLayoutString = ConfigurationSettings.DefaultExperFileName; // set a default value for the file layout string
        }

        /// <summary>
        /// Root folder for storing experiments
        /// </summary>
        [DataMember]
        public string ExperimentsRootFolder { get; set; }

        /// <summary>
        /// The root of the unc path to the location where the experiments are stored.
        /// </summary>
        [DataMember]
        public string ExperimentsRootUnc { get; set; }

        /// <summary>
        /// If true enables experiments to be stored in the drop folder in addition to the normal location.
        /// </summary>
        [DataMember]
        public bool IsExperimentsDropFolderEnabled { get; set; }

        /// <summary>
        /// An additional folder where experiments will be dropped without any folder hierarcy.
        /// </summary>
        [DataMember]
        public string ExperimentsDropFolder { get; set; }

        /// <summary>
        /// Specifies the extensions of the files that should be placed in the drop folder.
        /// </summary>
        [DataMember]
        public ICollection<String> DropFolderFileExtensions
        {
            get { return DropFolderFileExtensionsList; }
            set
            {
                if (value != null)
                {
                    DropFolderFileExtensionsList.Clear();
                    DropFolderFileExtensionsList.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Ensure the list is initialized which it may not be during deserialization.
        /// </summary>
        private List<string> DropFolderFileExtensionsList
        {
            get
            {
                if (_DropFolderFileExtensions == null)
                {
                    _DropFolderFileExtensions = new List<string>();
                }
                return _DropFolderFileExtensions;
            }
        }

        /// <summary>
        /// String that describes the format for experiment file names
        /// </summary>
        [DataMember]
        public string FileLayoutString { get; set; }

        /// <summary>
        /// An array of subfolders (up to 4) that describes the folder hierarchy beneath the ExperimentsRootFolder
        /// </summary>        
        [DataMember]
        public IList<String> SubFolders
        {
            get { return SubFoldersList; }
            set
            {
                if (value != null)
                {
                    Trace.Assert(value.Count <= 4, "Only 4 sub folders allowed.");
                    SubFoldersList.Clear();
                    SubFoldersList.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Ensure the list is initialized which it may not be during deserialization.
        /// </summary>
        private List<string> SubFoldersList
        {
            get
            {
                if (_SubFolders == null)
                {
                    _SubFolders = new List<string>();
                }
                return _SubFolders;
            }
        }

        /// <summary>
        /// Impersonation enabled
        /// </summary>
        [DataMember]
        public bool IsImpersonationEnabled { get; set; }

        /// <summary>
        /// Impersonation user id
        /// </summary>
        [DataMember]
        public string ImpersonationUserId { get; set; }

        /// <summary>
        /// Impersonation user Password
        /// </summary>
        [DataMember]
        public string ImpersonationPassword { get; set; }

        /// <summary>
        /// Impersonation domain
        /// </summary>
        [DataMember]
        public string ImpersonationDomain { get; set; }

        /// <summary>
        /// Overrides the default minimum diskspace free percent to generate a warning.  Normally this should be null.
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        public int? MinimumDiskspacePercentWarningOverride { get; set; }

        /// <summary>
        /// Overrides the default minimum diskspace free percent to generate an error.  Normally this should be null.
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        public int? MinimumDiskspacePercentErrorOverride { get; set; }

        /// <summary>
        /// An additional folder where S88 planned experiments will be dropped for import.
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        public string ImportDropFolder { get; set; }

        /// <summary>
        /// The maximum number of import files (planned experiments) to store.
        /// </summary>
        [DataMember]
        [DefaultValue(DefaultImportDropFolderCountLimit)]
        public int ImportDropFolderCountLimit { get; set; }
        
        /// <summary>
        /// The maximum age (in days) of import files (planned experiments) to store.
        /// </summary>
        [DataMember]
        [DefaultValue(DefaultImportDropFolderDaysLimit)]
        public int ImportDropFolderDaysLimit { get; set; }

        /// <summary>
        /// Delete planned experiment XML files after successful import
        /// </summary>
        [DataMember]
        public bool DeleteSuccessfullyImportedPlannedExperiments { get; set; }

        /// <summary>
        /// Rename planned experiment XML files after unsuccessful import
        /// </summary>
        [DataMember]
        public bool RenameFailedPlannedExperiments { get; set; }

        /// <summary>
        /// Gets the UNC folder for the experiment.
        /// </summary>
        public string GetPublicExperimentFolder(ExperimentInfo experimentInfo)
        {
            // If the UNC folder is not specified for some reason then use the main root folder.
            var folder = !string.IsNullOrWhiteSpace(ExperimentsRootUnc) ? ExperimentsRootUnc : ExperimentsRootFolder;
            string result = GetExperimentFolder(experimentInfo, folder);

            return result;
        }

        /// <summary>
        /// Given an ExperimentInfo (which contains the experiment id, user defined field, start date and user name), determine the
        /// path for an experiment based upon the folder configuration.
        /// </summary>
        public string GetExperimentFolder(ExperimentInfo experimentInfo, bool includeRootfolder)
        {
            string rootFolder = includeRootfolder ? ExperimentsRootFolder : "\\";
            string result = GetExperimentFolder(experimentInfo, rootFolder);

            if (!includeRootfolder  && result.Length > 1)
                result = result + "\\";

            return result;
        }

        private string GetExperimentFolder(ExperimentInfo experimentInfo, string rootFolder)
        {
            Trace.Assert(experimentInfo != null, "ExperimentInfo is null");
            Trace.Assert(experimentInfo.ExperimentReference != null, "ExperimentInfo.ExperimentReference is null");
            Trace.Assert(experimentInfo.ExperimentReference.Name != null, "ExperimentInfo.ExperimentReference.Name is null");
            Trace.Assert(experimentInfo.ExperimentReference.User != null, "ExperimentInfo.ExperimentReference.User is null");

            // start with the root folder ...
            string result = rootFolder;

            // ... and add each subfolder
            string temp = GetSubPath(SubFolders.ElementAtOrDefault(0), experimentInfo);
            if (!string.IsNullOrEmpty(temp))
                result = Path.Combine(result, temp);

            temp = GetSubPath(SubFolders.ElementAtOrDefault(1), experimentInfo);
            if (!string.IsNullOrEmpty(temp))
                result = Path.Combine(result, temp);

            temp = GetSubPath(SubFolders.ElementAtOrDefault(2), experimentInfo);
            if (!string.IsNullOrEmpty(temp))
                result = Path.Combine(result, temp);

            temp = GetSubPath(SubFolders.ElementAtOrDefault(3), experimentInfo);
            if (!string.IsNullOrEmpty(temp))
                result = Path.Combine(result, temp);
            return result;
        }

        /// <summary>
        /// Given an ExperimentInfo (which contains the experiment id, user defined field, start date and user name) and an extension, determine the
        /// name for an experiment based upon the folder configuration.
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string GetExperimentFileName(ExperimentInfo experiment, string extension)
        {
            Trace.Assert(experiment != null, "ExperimentInfo is null");
            Trace.Assert(experiment.ExperimentReference != null, "ExperimentInfo.ExperimentReference is null");
            Trace.Assert(experiment.ExperimentReference.Name != null, "ExperimentInfo.ExperimentReference.Name is null");
            Trace.Assert(experiment.ExperimentReference.User != null, "ExperimentInfo.ExperimentReference.User is null");
            
            string result = FileLayoutString;

            result = result.Replace(ConfigurationSettings.UserID, experiment.ExperimentReference.User);
            result = result.Replace(ConfigurationSettings.ProjectID, experiment.UserDefinedField ?? String.Empty); 
            result = result.Replace(ConfigurationSettings.ExperID, experiment.ExperimentReference.Name);
            
            // If we were given an instrument type, use it.
            if (!String.IsNullOrEmpty(experiment.InstrumentType))
                result = result.Replace(ConfigurationSettings.Instrument, experiment.InstrumentType);
            
            if (experiment.ExperimentReference != null && experiment.ExperimentReference.Source != null)
            {
                if (experiment.ExperimentReference.Source is InstrumentReference)
                {
                    result = result.Replace(ConfigurationSettings.ExperimentSource, ((InstrumentReference)(experiment.ExperimentReference.Source)).HostAddress);
                    result = result.Replace(ConfigurationSettings.Instrument, ((InstrumentReference)(experiment.ExperimentReference.Source)).InstrumentType);
                }
                else if (experiment.ExperimentReference.Source is ApplicationReference)
                {
                    result = result.Replace(ConfigurationSettings.ExperimentSource, ((ApplicationReference)(experiment.ExperimentReference.Source)).HostAddress);
                    result = result.Replace(ConfigurationSettings.Instrument, ((ApplicationReference)(experiment.ExperimentReference.Source)).Name);
                }
                
            }
            else
            {
                result = result.Replace(ConfigurationSettings.ExperimentSource, String.Empty);
                result = result.Replace(ConfigurationSettings.Instrument, String.Empty);
            }

            while ((result.Contains("{")) && (result.Contains("}")))
            {
                // assume everything in between is a date
                int startIndex = result.IndexOf("{");
                int endIndex = result.IndexOf("}");
                string dateFormatString = result.Substring(startIndex + 1, endIndex - startIndex - 1);
                result = result.Substring(0, startIndex) + experiment.StartTime.ToLocalTime().ToString(dateFormatString) + result.Substring(endIndex + 1);
            }

            if (!String.IsNullOrEmpty(extension))
                return string.Format("{0}.{1}", result, extension);

            return result;
        }

        /// <summary>
        /// Get a folder (subfolder) based upon a folder format and experiment information
        /// </summary>
        /// <param name="folderFormat"></param>
        /// <param name="experiment"></param>
        /// <returns>folder string, or String.Empty if there is a formatting problem</returns>
        private string GetSubPath(string folderFormat, ExperimentInfo experiment)
        {
            if (String.IsNullOrEmpty(folderFormat))
                return string.Empty; // no folder format specified

            if (folderFormat == ConfigurationSettings.NotUsed)
                return string.Empty; // folder format is "no folder"

            if (folderFormat == ConfigurationSettings.ExperFileName)
                return string.Empty; // folder format is the experiment file name

            string result = folderFormat;

            List<string> standardNONTimeConfigs = new List<string>(new[] { ConfigurationSettings.UserID, ConfigurationSettings.ProjectID, ConfigurationSettings.ExperID, ConfigurationSettings.ExperimentSource, ConfigurationSettings.Instrument });
            Regex regex = new Regex(@"\{[^}]*\}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            MatchCollection matchCollection = regex.Matches( result );
            foreach (Match match in matchCollection)
            {
                if (standardNONTimeConfigs.Contains(match.Value))
                {
                    string toReplaceWith = experiment.ExperimentReference.User;
                    switch(match.Value)
                    {
                        case ConfigurationSettings.ExperID:
                            toReplaceWith = experiment.ExperimentReference.Name;
                            break;
                        case ConfigurationSettings.ProjectID:
                            toReplaceWith = experiment.UserDefinedField ?? String.Empty;
                            break;
                        case ConfigurationSettings.ExperimentSource:
                            toReplaceWith = String.Empty;
                            if (experiment.ExperimentReference != null && experiment.ExperimentReference.Source != null)
                            {
                                if (experiment.ExperimentReference.Source is InstrumentReference)
                                {
                                    toReplaceWith = ((InstrumentReference)(experiment.ExperimentReference.Source)).HostAddress;
                                }
                                else if (experiment.ExperimentReference.Source is ApplicationReference)
                                {
                                    toReplaceWith = ((ApplicationReference)(experiment.ExperimentReference.Source)).HostAddress;
                                }
                            }
                            break;
                        case ConfigurationSettings.Instrument:
                            toReplaceWith = String.Empty;
                            if (!String.IsNullOrEmpty(experiment.InstrumentType))
                                toReplaceWith = experiment.InstrumentType;
                            else if (experiment.ExperimentReference != null && experiment.ExperimentReference.Source != null)
                            {
                                if (experiment.ExperimentReference.Source is InstrumentReference)
                                {
                                    toReplaceWith = ((InstrumentReference)(experiment.ExperimentReference.Source)).InstrumentType;
                                }
                                else if (experiment.ExperimentReference.Source is ApplicationReference)
                                {
                                    toReplaceWith = ((ApplicationReference)(experiment.ExperimentReference.Source)).Name;
                                }
                            }
                            break;
                    }
                    if (string.IsNullOrWhiteSpace(toReplaceWith))
                    {
                        toReplaceWith = "Unknown";
                    }  
                    result = result.Replace(match.Value, toReplaceWith);

                }
                else
                {
                    try
                    {
                        string formatedTime = experiment.StartTime.ToLocalTime().ToString(match.Value.Trim(new[]{'{','}'}));
                        result = result.Replace(match.Value, formatedTime);
                    }
                    catch (Exception ex)
                    {
                        GC.KeepAlive(ex); 
                    }
                }
            }
            char[] invalidFileNameChars = Path.GetInvalidPathChars().Concat(new[] { '\\', ':', '/', '*', '\'', '?' }).Distinct().ToArray();
            if (result.IndexOfAny(invalidFileNameChars) != -1)
            {
                result = invalidFileNameChars.Aggregate(new StringBuilder(result), (currentName, c) => currentName.Replace(c, '_')).ToString();
            }

            return result;
        }
    }
}
