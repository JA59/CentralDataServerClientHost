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
**    Copyright © 2009 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;

namespace AutoChem.Core.ServiceModel
{
    /// <summary>
    /// Indicates that the resynchronized version of the method should 
    /// asynchronously handle the resynchronization of the method.  
    /// This applies to the silverlight version of the operation contracts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncResyncAttribute : Attribute
    {
    }
}
