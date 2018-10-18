#region File Header

/*
**  COPYRIGHT:
**  This software program is furnished to the user under license by
**  Mettler-Toledo AutoChem, Inc.  Use thereof is subject to applicable
**  U.S. and international law. This software program may not be reproduced,
**  transmitted, or disclosed to third parties, in whole or in part, in any
**  form or by any manner, electronic or mechanical, without the express
**  written consent of Mettler-Toledo AutoChem, Inc. except to the extent
**  provided for by applicable license.
**
**  Copyright © 2017 by Mettler-Toledo AutoChem, Inc.  All rights reserved.
**/



#endregion


using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChem.Core.Controls
{
    /// <summary>
    /// Class that holds constant characters for the mask
    /// </summary>
    public class PatternCharacters
    {
        /// <summary>
        /// The placeholder character inserted when a pattern has been specified.
        /// </summary>
        public const char PlaceHolder = '_';

        /// <summary>
        /// char IEnumarable of chars defined in the MaskCharsEnum
        /// </summary>
        public static IEnumerable<char> MaskChars { get { return Enum.GetValues(typeof(MaskCharsEnum)).Cast<MaskCharsEnum>().Select(value => (char)value); } }
    }
    /// <summary>
    /// Special characters in the mask
    /// </summary>
    public enum MaskCharsEnum
    {

        /// <summary>
        /// any letter
        /// </summary>
        RequiredCharacter = '@',
        /// <summary>
        /// any digit
        /// </summary>
        RequiredDigit = '#',
        /// <summary>
        /// any character
        /// </summary>
        RequiredCharacterOrDigit = '?',
        /// <summary>
        /// optional: any character
        /// </summary>
        OptionalCharacter = '>',
        /// <summary>
        /// optional: digit
        /// </summary>
        OptionalDigit = '<',
        /// <summary>
        /// optional: digit or character
        /// </summary>
        OptionalCharacterOrDigit = ';',
    }

    /// <summary>
    /// Special characters in the system mask
    /// </summary>
    public enum SystemMaskCharsEnum
    {

        /// <summary>
        /// L -> MaskedTextProvider Required Letter (a-z, A-Z)
        /// </summary>
        RequiredCharacter = 'L',
        /// <summary>
        /// 0 -> MaskedTextProvider Required Digit (0-9)
        /// </summary>
        RequiredDigit = '0',
        /// <summary>
        /// A -> MaskedTextProvider required Alphanumeric (0-9, a-z, A-Z)
        /// </summary>
        RequiredCharacterOrDigit = 'A',
        /// <summary>
        /// ? -> MaskedTextProvider Optional letter (a-z, A-Z)
        /// </summary>
        OptionalCharacter = '?',
        /// <summary>
        /// 9 -> MaskedTextProvider Optional digit (0-9) or space
        /// </summary>
        OptionalDigit = '9',
        /// <summary>
        /// a -> MaskedTextProvider Optional Alphanumeric (0-9, a-z, A-Z)
        /// </summary>
        OptionalCharacterOrDigit = 'a',
    }

}
