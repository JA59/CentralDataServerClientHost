using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Trace = System.Diagnostics.Debug;


namespace AutoChem.Core.Reflection
{
    partial class ReflectionUtility
    {
        /// <summary>
        /// Gets all instance fields whether public or not for the type including inherited types.
        /// </summary>
        public static FieldInfo[] GetAllInstanceFields(Type objectType)
        {
            FieldInfo[] fields = objectType.GetFields(BindingFlags.NonPublic |
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (objectType.BaseType != null && objectType.BaseType != typeof(object))
            {
                FieldInfo[] baseFields = GetAllInstanceFields(objectType.BaseType);

                FieldInfo[] tempFields = new FieldInfo[fields.Length + baseFields.Length];
                Array.Copy(fields, 0, tempFields, 0, fields.Length);
                Array.Copy(baseFields, 0, tempFields, fields.Length, baseFields.Length);
                fields = tempFields;
            }

            return fields;
        }

        /// <summary>
        /// Retrieves the list of specified types that implement TInterface from the specified assembly.
        /// </summary>
        public static IEnumerable<Type> GetTypesFromAssembly<TInterface>(Assembly assembly) where TInterface : class
        {
            return GetTypesFromAssembly(typeof(TInterface), assembly);
        }

        /// <summary>
        /// Retrieves the list of specified types from the specified assembly.
        /// </summary>
        /// <returns>ArrayList: List of types.</returns>
        public static Type[] GetTypesFromAssembly(Type interfaceType, Assembly assembly)
        {
            List<Type> typeList = new List<Type>();
            Type[] types = null;

            try
            {
                types = GetTypesFromAssembly(assembly);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception getting types for assembly '" + assembly.FullName + "': " + e.Message);
            }

            if (types != null)
            {
                foreach (Type ctype in types)
                {
                    if ((ctype != null) &&
                        ctype.IsClass &&
                        ctype.IsPublic &&
                        !ctype.IsAbstract)
                    {
                        if (interfaceType.IsAssignableFrom(ctype))
                        {
                            typeList.Add(ctype);
                        }
                    }
                }
            }

            return (typeList.ToArray());
        }


        /// <summary>
        /// Retrieves the list of specified class types from the specified assembly.
        /// </summary>
        /// <param name="className">string class name</param>
        /// <param name="assembly">Assembly to search for a class from</param>
        /// <returns>ArrayList: List of types. Most likely just one item</returns>
        public static Type[] GetClassTypeByNameFromAssembly(string className, Assembly assembly)
        {
            List<Type> typeList = new List<Type>();
            Type[] types = null;

            try
            {
                types = GetTypesFromAssembly(assembly);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception getting types for assembly '" + assembly.FullName + "': " + e.Message);
            }

            if (types != null)
            {
                foreach (Type ctype in types)
                {
                    if ((ctype != null) &&
                        ctype.IsClass &&
                        !ctype.IsAbstract)
                    {
                        if (ctype.Name == className)
                        {
                            typeList.Add(ctype);
                        }
                    }
                }
            }

            return (typeList.ToArray());
        }
        /// <summary>
        /// Retrieves the list of specified types from the specified assembly.
        /// </summary>
        /// <returns>ArrayList: List of types.</returns>
        public static Type[] GetTypesFromAssembly(Assembly assembly)
        {
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types;
                Trace.WriteLine("Exception getting types for assembly '" + assembly.FullName + "': " + ex.Message);
                foreach (Exception e in ex.LoaderExceptions)
                {
                    Trace.WriteLine("   " + e.ToString());
                }
            }

            return types;
        }

        /// <summary>
        /// Checks whether one type supports another type.
        /// This is the case where the type is a subclass or supports the interface
        /// of another type.
        /// </summary>
        /// <param name="typeToCheck">Type to check</param>
        /// <param name="typeToSupport">Type to support</param>
        /// <returns>Flag whether the type is supported</returns>
        public static bool TypeSupportsType(Type typeToCheck, Type typeToSupport)
        {
            // Null argument, return false
            if ((typeToCheck == null) || (typeToSupport == null))
            {
                return false;
            }
            else
            {
                return typeToSupport.IsAssignableFrom(typeToCheck);
            }
        }
    }
}
