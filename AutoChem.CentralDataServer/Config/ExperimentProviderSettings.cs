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

using AutoChem.Core.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// This is the paired down settings that the experiment providers such as iControl and iC IR need.
    /// That is it is a subset of the information contained in ConfigurationSettings.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    public class ExperimentProviderSettings
    {
        private MetadataSettings _MetadataSettings;

        /// <summary>
        /// Creates an empty ExperimentProviderSettings;
        /// </summary>
        public ExperimentProviderSettings()
        {
            _MetadataSettings = new MetadataSettings();
        }

        /// <summary>
        /// Creates an ExperimentProviderSettings from the full ConfigurationSettings
        /// </summary>
        public ExperimentProviderSettings(ConfigurationSettings settings)
        {
            MetadataSettings = settings.MetadataSettings;
            SendEmailNotifications = settings.EmailSettings.EnableEmailCommunication && settings.EmailSettings.SendNotifications; 
            EmailDomain = settings.EmailSettings.EmailDomain;
            ExperimentsRootUnc = settings.FolderSettings.ExperimentsRootUnc;
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
            set
            {
                _MetadataSettings = value;
                if (_MetadataSettings != null)
                {
                    _MetadataSettings = _MetadataSettings.Clone();
                    if (!string.IsNullOrEmpty(_MetadataSettings.ExperimentIdInputConfigurationMask))
                    {
                        _MetadataSettings.ExperimentIdInputMask = ReplacePromptCharWithMask(_MetadataSettings.ExperimentIdInputMask, false);
                        _MetadataSettings.ExperimentIdInputMaskAdvanced = ReplacePromptCharWithMask(_MetadataSettings.ExperimentIdInputMaskAdvanced, true);
                        _MetadataSettings.ExperimentIdInputMaskAdvancedMainPartOnly = ReplacePromptCharWithMask(_MetadataSettings.ExperimentIdInputMaskAdvancedMainPartOnly, true);
                        _MetadataSettings.ExperimentIdInputMaskAdvancedSuffixOnly = ReplacePromptCharWithMask(_MetadataSettings.ExperimentIdInputMaskAdvancedSuffixOnly, true);
                    }
                    if (!string.IsNullOrEmpty(_MetadataSettings.UserDefinedFieldInputMask))
                    {
                        _MetadataSettings.UserDefinedFieldInputMask = ReplacePromptCharWithMask(_MetadataSettings.UserDefinedFieldInputMask, false);
                        _MetadataSettings.UserDefinedFieldInputMaskAdvanced = ReplacePromptCharWithMask(_MetadataSettings.UserDefinedFieldInputMaskAdvanced, true);
                    }
                }

            }
        }
        private string ReplacePromptCharWithMask(string textWithIllegalChars, bool allowOptionalCharacters)
        {
            char[] invalidFileNameChars = Path.GetInvalidPathChars().Concat(new[] { '\\', ':', '/', '&', '*', '{', '}', '[', ']', '\''}).Distinct().ToArray();
            if (!allowOptionalCharacters)
                invalidFileNameChars = invalidFileNameChars.Concat(new[] { (char)MaskCharsEnum.OptionalCharacterOrDigit, (char)MaskCharsEnum.OptionalCharacter, (char)MaskCharsEnum.OptionalDigit }).Distinct().ToArray();
            else
                invalidFileNameChars = invalidFileNameChars.Where(c => (c != (char)MaskCharsEnum.OptionalCharacterOrDigit)
                                                                    && (c != (char)MaskCharsEnum.OptionalCharacter) 
                                                                    && (c != (char)MaskCharsEnum.OptionalDigit)).ToArray();
            var newString = new StringBuilder();

            if (textWithIllegalChars != null)
            {
                foreach (var character in textWithIllegalChars)
                {
                    if (!invalidFileNameChars.Contains(character))
                    {
                        newString.Append(character);
                    }
                }                
            }


            return newString.ToString();
        }
        /// <summary>
        /// True if the system will send e-mail notifications for experiments.
        /// </summary>
        [DataMember]
        public bool SendEmailNotifications { get; set; }

        /// <summary>
        /// The domain for e-mail addresses
        /// </summary>
        [DataMember]
        public string EmailDomain { get; set; }

        /// <summary>
        /// The domain for e-mail addresses
        /// </summary>
        [DataMember]
        public string ExperimentsRootUnc { get; set; }

#if !SILVERLIGHT
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
                    new DataContractSerializer(typeof(ExperimentProviderSettings));
                serializer.WriteObject(writer, this);
            }
        }
#endif
    }
}
