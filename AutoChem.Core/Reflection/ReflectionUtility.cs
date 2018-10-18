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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Text.RegularExpressions;

namespace AutoChem.Core.Reflection
{
    /// <summary>
    /// Provides common reflection capabilities.
    /// </summary>
    public partial class ReflectionUtility
    {
        //private static Dictionary<string, Assembly> m_Assemblies;

        /// <summary>
        /// Loads the assemblies if they have not already been loaded.
        /// This should be called on the UI thread.
        /// </summary>
        //public static void LoadAssemblies()
        //{
        //    if (m_Assemblies == null)
        //    {
        //        // Create the dictionary in local variable so that the global variable is not set until it is populated.
        //        // Although, with the construction of the dictionary from the linq statement that should not be an
        //        // issue, but it is safe for future changes.
        //        var assemblies =
        //            (from part in Deployment.Current.Parts
        //             let streamResource = Application.GetResourceStream(new Uri(part.Source, UriKind.Relative))
        //             select new { Uri = part.Source, Assembly = part.Load(streamResource.Stream) }).ToDictionary(
        //                uriAndAssembly => uriAndAssembly.Uri, uriAndAssembly => uriAndAssembly.Assembly);

        //        m_Assemblies = assemblies;
        //    }
        //}

        /// <summary>
        /// Returns the assemblies from the application directory which match the search name  
        /// </summary>
        /// <param name="searchName">Filespec for the dll, eg. *audit*.dll</param>
        /// <returns>An array of assemblies</returns>
        //public static Assembly[] GetAssembliesOnDisk(string searchName)
        //{
        //    // Ensure the assemblies are loaded.
        //    //LoadAssemblies();

        //    // Turn the * based mathcing into a regular expression.
        //    // Make a dot just a dot and then make * into its regular expression form.
        //    Regex regex = new Regex(searchName.Replace(".", "[.]").Replace("*", ".*"), RegexOptions.IgnoreCase);

        //    var matchingAssemblies =
        //        from entry in m_Assemblies
        //        where regex.IsMatch(entry.Key)
        //        select entry.Value;

        //    return matchingAssemblies.ToArray();
        //}
    }
}
