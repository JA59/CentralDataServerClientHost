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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// Metadata settings concerning folders and how experiments are organized into folders.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class MetadataSettings
    {
        /// <summary>
        /// Creates a new instance of MetadataSettings
        /// </summary>
        public MetadataSettings()
        {
            UsingUserDefinedField = false;
            UserDefinedField = "Project";
            UserDefinedFieldInputMask = string.Empty;
            UserDefinedFieldInputConfigurationMask = string.Empty;
            UserDefinedFieldInputMaskAdvanced = string.Empty;
            UsingUserDefinedFieldInputMask = false;
            EnforceStartOfExperiment = false;
            SampleIdFormat = ConfigurationSettings.InitialSampleIdFormat;
        }

        /// <summary>
        /// Boolean set if we are using an input mask for the experiment ID
        /// </summary>
        [DataMember]
        public bool UsingExperimentIdInputMask { get; set; }

        /// <summary>
        /// String that contains the experiment ID input mask as entered on the configuration screen (main part)
        /// </summary>
        [DataMember]
        public string ExperimentIdInputConfigurationMask { get; set; }

        /// <summary>
        /// String that contains the experiment ID input mask as entered on the configuration screen (suffix)
        /// </summary>
        [DataMember]
        public string ExperimentIdInputConfigurationMaskSuffix { get; set; }

        /// <summary>
        /// String that contains the traditional (old style) experiment ID input mask
        /// USED BY OLDER APPLICATIONS THAT DO NOT SUPPORT VARIABLE LENGTH OPTIONAL CHARACTERS
        /// </summary>
        [DataMember]
        public string ExperimentIdInputMask { get; set; }

        /// <summary>
        /// String that contains the advanced (new style) experiment ID input mask
        /// MAIN PART AND SUFFIX COMBINED
        /// USED BY APPLICATIONS THAT SUPPORT VARIABLE LENGTH OPTIONAL CHARACTERS, BUT NOT SEPARATE MAIN PART AND SUFFIX
        /// </summary>
        [DataMember]
        public string ExperimentIdInputMaskAdvanced { get; set; }

        /// <summary>
        /// String that contains the advanced (new style) experiment ID input mask (main part only)
        /// MAIN PART ONLY
        /// USED BY APPLICATIONS THAT SUPPORT VARIABLE LENGTH OPTIONAL CHARACTERS WITH SEPARATE MAIN PART AND SUFFIX
        /// </summary>
        [DataMember]
        public string ExperimentIdInputMaskAdvancedMainPartOnly { get; set; }

        /// <summary>
        /// String that contains the advanced (new style) experiment ID input mask (suffix only)
        /// SUFFIX ONLY
        /// USED BY APPLICATIONS THAT SUPPORT VARIABLE LENGTH OPTIONAL CHARACTERS WITH SEPARATE MAIN PART AND SUFFIX
        /// </summary>
        [DataMember]
        public string ExperimentIdInputMaskAdvancedSuffixOnly { get; set; }

        /// <summary>
        /// Boolean set if the user should enter their name when starting an experiment.
        /// </summary>
        [DataMember]
        public bool UserEntersNameWhenStartingExperiment { get; set; }

        /// <summary>
        /// Are we using a user defined field
        /// </summary>
        [DataMember]
        public bool UsingUserDefinedField { get; set; }

        /// <summary>
        /// String that contains the user defined field
        /// </summary>
        [DataMember]
        public string UserDefinedField { get; set; }

        /// <summary>
        /// Boolean set if we are using an input mask for the user defined field
        /// </summary>
        [DataMember]
        public bool UsingUserDefinedFieldInputMask { get; set; }

        /// <summary>
        /// String that contains the user defined field input mask
        /// </summary>
        [DataMember]
        public string UserDefinedFieldInputConfigurationMask { get; set; }

        /// <summary>
        /// String that contains the traditional (old style) user defined field input mask
        /// </summary>
        [DataMember]
        public string UserDefinedFieldInputMask { get; set; }

        /// <summary>
        /// String that contains the advanced (new style) user defined field input mask
        /// </summary>
        [DataMember]
        public string UserDefinedFieldInputMaskAdvanced { get; set; }

        /// <summary>
        /// Boolean set if we require an experiment to be running when performing operations in direct mode
        /// </summary>
        [DataMember]
        public bool EnforceStartOfExperiment { get; set; }

        /// <summary>
        /// Sample name format when taking samples.
        /// </summary>
        [DataMember]
        public string SampleIdFormat { get; set; }

        /// <summary>
        /// Are the ELN Planned experiments extensions enabled (XML File Input)?
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        public bool ELNPlannedExperimentsEnabled { get; set; }

        /// <summary>
        /// Are the S88 extensions enabled?
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        public bool S88Enabled { get; set; }

        /// <summary>
        /// Clones currentitem
        /// </summary>
        /// <returns></returns>
        public MetadataSettings Clone()
        {
            MetadataSettings clone = new MetadataSettings();
            clone.UserDefinedField = UserDefinedField;
            clone.UserDefinedFieldInputMask = UserDefinedFieldInputMask;
            clone.UserDefinedFieldInputConfigurationMask = UserDefinedFieldInputConfigurationMask;
            clone.UserDefinedFieldInputMaskAdvanced = UserDefinedFieldInputMaskAdvanced;
            clone.UserEntersNameWhenStartingExperiment = UserEntersNameWhenStartingExperiment;
            clone.UsingExperimentIdInputMask = UsingExperimentIdInputMask;
            clone.UsingUserDefinedField = UsingUserDefinedField;
            clone.UsingUserDefinedFieldInputMask = UsingUserDefinedFieldInputMask;
            clone.ExperimentIdInputConfigurationMask = ExperimentIdInputConfigurationMask;
            clone.ExperimentIdInputConfigurationMaskSuffix = ExperimentIdInputConfigurationMaskSuffix;
            clone.ExperimentIdInputMask = ExperimentIdInputMask;
            clone.ExperimentIdInputMaskAdvanced = ExperimentIdInputMaskAdvanced;
            clone.ExperimentIdInputMaskAdvancedMainPartOnly = ExperimentIdInputMaskAdvancedMainPartOnly;
            clone.ExperimentIdInputMaskAdvancedSuffixOnly = ExperimentIdInputMaskAdvancedSuffixOnly;
            clone.EnforceStartOfExperiment = EnforceStartOfExperiment;
            clone.SampleIdFormat = SampleIdFormat;
            clone.ELNPlannedExperimentsEnabled = ELNPlannedExperimentsEnabled;
            clone.S88Enabled = S88Enabled;
            return clone;
        }

        /// <summary>
        /// Deserialization override to supply default values where needed
        /// </summary>
        /// <param name="c"></param>
        [System.Runtime.Serialization.OnDeserialized]
        public void OnDeserialized(System.Runtime.Serialization.StreamingContext c)
        {
            SampleIdFormat = (SampleIdFormat ?? ConfigurationSettings.InitialSampleIdFormat); // ConfigurationSettings.InitialSampleIdFormat is the default value
        }
    }
}
