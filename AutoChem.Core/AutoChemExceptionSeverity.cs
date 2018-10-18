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
using System.Text;

namespace AutoChem.Core
{
    /// <summary>
    /// AutoChemExceptionSeverity is used to enumerate the severity of an AutoChemException.
    /// </summary>
    [Serializable]
    public enum AutoChemExceptionSeverity
    {
        /// <summary>
        /// Exception is unexpected.
        /// </summary>
        UnexpectedError,
        /// <summary>
        /// Exception should be presented as an error.
        /// </summary>
        Error,
        /// <summary>
        /// Excpetion should be presented as a warning.
        /// </summary>
        Warning,
        /// <summary>
        /// Excpetion should be presented as informational.
        /// </summary>
        Information
    }
}
