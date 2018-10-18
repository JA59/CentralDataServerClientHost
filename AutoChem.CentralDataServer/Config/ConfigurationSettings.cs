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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// User adjustable settings for the Central Data Service
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class ConfigurationSettings
    {
        private FolderSettings _FolderSettings;
        private EmailSettings _EmailSettings;
        private MetadataSettings _MetadataSettings;
        private List<DeviceSettings> _DeviceSettings;
        private ReportSettings _ReportSettings;
        private ProcessingSettings _ProcessingSettings;

        /// <summary>
        /// String used to indicate that a sub-folder has not ben selected
        /// </summary>
        public const string NotUsed = "...";

        /// <summary>
        /// Token for a user id
        /// </summary>
        public const string UserID = "{User Name}";

        /// <summary>
        /// Token for a project name
        /// </summary>
        public const string ProjectID = "{Project Name}";

        /// <summary>
        /// Token for experiment id
        /// </summary>
        public const string ExperID = "{Experiment Name}";

        /// <summary>
        /// Token for a project name
        /// </summary>
        public const string ExperimentSource = "{Experiment Source}";

        /// <summary>
        /// Token for a project name
        /// </summary>
        public const string Instrument = "{Instrument}";
        
        /// <summary>
        /// Token for experiment File Name
        /// </summary>
        public const string ExperFileName = "Experiment File";

        /// <summary>
        /// Token for default experiment File Name
        /// </summary>
        public const string DefaultExperFileName = ExperID + " started at {HH-mm-ss} on {MM-dd-yyyy}";

        /// <summary>
        /// Token for user defined field
        /// </summary>
        public const string UserDefID = "{User Defined Field}";
        
        /// <summary>
        /// Default date format
        /// </summary>
        public const string DefaultDateFormat = "{MM-yyyy}";

        /// <summary>
        /// Example to use when displaying a user name
        /// </summary>
        public const string ExampleUserName = "john.doe";
        
        /// <summary>
        /// Example to use when displaying a project name
        /// </summary>
        public const string ExampleProjectName = "Project";

        /// <summary>
        /// Example to use when displaying an instrument address
        /// </summary>
        public const string ExampleInstrumentAddress = "10.0.0.101";

        /// <summary>
        /// Example to use when displaying an instrument type
        /// </summary>
        public const string ExampleInstrumentType = "EasyMax 102";

        /// <summary>
        /// Example to use when displaying an instrument FW version
        /// </summary>
        public const string ExampleInstrumentVersion = "5.2.1.0";

        /// <summary>Defines the Sample ID Token value where the sample counter gets inserted.</summary>
        public const string SampleIdTokenCounter = "{Counter}";

        /// <summary>Defines the Sample ID Token value where the Experiment name gets inserted.</summary>
        public const string SampleIdTokenExperimentName = "{Experiment Name}";

        /// <summary>Defines the Sample ID Token value where the Project name gets inserted.</summary>
        public const string SampleIdTokenProjectName = "{Project Name}";

        /// <summary>Defines the Sample ID Token value where the User name gets inserted.</summary>
        public const string SampleIdTokenUserName = "{User Name}";

        /// <summary>Specifies the default when ic Data Center not being used </summary>
        public const string DefaultSampleIdFormat = "Sample-{Counter}";

        /// <summary>Specifies the default for a new iC Data Center configuration </summary>
        public const string InitialSampleIdFormat = "{Experiment Name} {Counter}";


        
        /// <summary>
        /// Creates an empty ConfgiruationSettings;
        /// </summary>
        public ConfigurationSettings()
        {
            _FolderSettings = new FolderSettings();
            _EmailSettings = new EmailSettings();
            _MetadataSettings = new MetadataSettings();
            _DeviceSettings = new List<DeviceSettings>();
            _ReportSettings = new ReportSettings();
            _ProcessingSettings = new ProcessingSettings();
        }

        /// <summary>
        /// Creates an empty ConfgiruationSettings, except for
        /// folder settings and metadata settings.
        /// </summary>
        public ConfigurationSettings(FolderSettings folderSettings, MetadataSettings metaDataSettings)
        {
            _FolderSettings = folderSettings;
            _EmailSettings = new EmailSettings();
            _MetadataSettings = metaDataSettings;
            _DeviceSettings = new List<DeviceSettings>();
            _ReportSettings = new ReportSettings();
            _ProcessingSettings = new ProcessingSettings();
        }

        /// <summary>
        /// Root URL for the Central Data Service
        /// </summary>
        [DataMember]
        public string UrlRoot { get; set; }

        /// <summary>
        /// Folder containing the database file
        /// </summary>
        [DataMember]
        public string DatabaseFolder { get; set; }

        /// <summary>
        /// The settings for folders for storing experiment files.
        /// </summary>
        [DataMember]
        public FolderSettings FolderSettings
        {
            get
            {
                if (_FolderSettings == null)
                {
                    _FolderSettings = new FolderSettings();
                }
                return _FolderSettings;
            }
            set { _FolderSettings = value; }
        }

        /// <summary>
        /// The settings for sending e-mails
        /// </summary>
        [DataMember]
        public EmailSettings EmailSettings
        {
            get
            {
                if (_EmailSettings == null)
                {
                    _EmailSettings = new EmailSettings();
                }
                return _EmailSettings;
            }
            set { _EmailSettings = value; }
        }

        /// <summary>
        /// The metadata settings for experiments (Name format, User defined field, ...)
        /// </summary>
        [DataMember]
        public MetadataSettings MetadataSettings
        {
            get
            {
                if (_MetadataSettings == null)
                {
                    _MetadataSettings = new MetadataSettings();
                }
                return _MetadataSettings;
            }
            set { _MetadataSettings = value; }
        }

        /// <summary>
        /// Settings for generating reports
        /// </summary>
        [DataMember]
        public ReportSettings ReportSettings
        {
            get
            {
                if (_ReportSettings == null)
                {
                    _ReportSettings = new ReportSettings();
                }
                return _ReportSettings;
            }
            set { _ReportSettings = value; }
        }

        /// <summary>
        /// Returns a collection of device settings with settings specific to various devices.
        /// </summary>
        [DataMember]
        public ICollection<DeviceSettings> DeviceSettings
        {
            get { return DeviceSettingsList; }
            set
            {
                if (value != null)
                {
                    DeviceSettingsList.Clear();
                    DeviceSettingsList.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Settings for processing experiments
        /// </summary>
        [DataMember]
        public ProcessingSettings ProcessingSettings
        {
            get
            {
                if (_ProcessingSettings == null)
                {
                    _ProcessingSettings = new ProcessingSettings();
                }
                return _ProcessingSettings;
            }
            set { _ProcessingSettings = value; }
        }

        /// <summary>
        /// Ensure the list is initialized which it may not be during deserialization.
        /// </summary>
        private List<DeviceSettings> DeviceSettingsList
        {
            get
            {
                if (_DeviceSettings == null)
                {
                    _DeviceSettings = new List<DeviceSettings>();
                }
                return _DeviceSettings;
            }
        }

        /// <summary>
        /// Returns true if a device settings class has been created for the specified type.
        /// </summary>
        public bool AreDeviceSetingsDefined<T>() where T : DeviceSettings
        {
            return DeviceSettings.OfType<T>().Any();
        }

        /// <summary>
        /// Returns the device settings of the specified type.  Not that if an instance of the type has not been created.
        /// It is created and returned.
        /// </summary>
        public T GetDeviceSettings<T>() where T : DeviceSettings
        {
            var settings = DeviceSettings.OfType<T>().FirstOrDefault();

            if (settings == null)
            {
                settings = Activator.CreateInstance<T>();
                AddDeviceSettings(settings);
            }

            return settings;
        }

        /// <summary>
        /// Add a set of device settings to the collection.
        /// </summary>
        public void AddDeviceSettings(DeviceSettings deviceSettings)
        {
            DeviceSettingsList.Add(deviceSettings);
        }

        private const string XmlFileName = "ConfigurationSettings.xml";



#if !SILVERLIGHT
        /// <summary>
        /// Full file name to the configuration file
        /// </summary>
        public static string ConfigurationFileFullName
        {
            get { return Path.Combine(FolderHelper.AppDataPath, XmlFileName); }
        }
        
        #region Serialization
        /// <summary>
        /// Load settings from XML
        /// </summary>
        /// <returns></returns>
        public static ConfigurationSettings LoadSettings()
        {
            var path = ConfigurationFileFullName;
            if(!File.Exists(path))
            {
                ConfigurationSettings settings = CreateDefaultSettings();
                settings.SaveSettings();
                return settings;
            }
            using (FileStream fs = File.OpenRead(path))
            {
                using (var reader = new XmlTextReader(fs))
                {
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof (ConfigurationSettings));
                    ConfigurationSettings settings;

                    try
                    {
                        settings = (ConfigurationSettings)serializer.ReadObject(reader);
                    }
                    catch (Exception exception)
                    {
                        Trace.TraceError("Failed to load configuration settings and using default settings instead. {0}", exception);
                        settings = CreateDefaultSettings();
                    }
                    return settings;
                }
            }
        }

        private static ConfigurationSettings CreateDefaultSettings()
        {
            ConfigurationSettings settings = new ConfigurationSettings
            {
                UrlRoot = "http://{0}:80",
                DatabaseFolder = FolderHelper.AppDataPath
            };

            settings.EmailSettings.EmailHTMLTemplatePath = string.Format("{0}\\{1}", FolderHelper.AppDataPath, EmailSettings.DefaultTemplateFileName);
            settings.FolderSettings.ExperimentsRootFolder = string.Format("{0}\\{1}", FolderHelper.AppDataAllVersionsPath, "Experiments");

            return settings;
        }

        /// <summary>
        /// Save as XML to standard application data location.
        /// </summary>
        public void SaveSettings()
        {
            SaveSettings(ConfigurationFileFullName);
        }

        /// <summary>
        /// Saves the configuration to the specified path.
        /// </summary>
        internal void SaveSettings(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            using (var streamWriter = new StreamWriter(fs))
            using (var writer = new XmlTextWriter(streamWriter))
            {
                writer.Formatting = Formatting.Indented;
                DataContractSerializer serializer =
                    new DataContractSerializer(typeof(ConfigurationSettings));
                serializer.WriteObject(writer, this);
            }
        }
        #endregion
#endif
    }
}
