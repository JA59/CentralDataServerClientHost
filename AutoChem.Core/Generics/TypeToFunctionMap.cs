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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoChem.Core.Generics
{
    /// <summary>
    /// Defines the basics for a set of functions that can be dispacthed based on type.
    /// </summary>
    public abstract class TypeToFunctionMapBase<T>
    {
        /// <summary>
        /// The set of mappings.
        /// </summary>
        protected abstract IEnumerable<IFunctionMappingBase> Mappings { get; }

        /// <summary>
        /// Returns true if there is a function to handle the type of the specified argument.
        /// </summary>
        public bool HasFunction(T arg)
        {
            return Mappings.Any(mapping => mapping.DerivedType.IsInstanceOfType(arg));
        }

        /// <summary>
        /// The base interface for a mapping.
        /// </summary>
        protected interface IFunctionMappingBase
        {
            /// <summary>
            /// The derived type that the mapping supports
            /// </summary>
            Type DerivedType { get; }
        }

        /// <summary>
        /// The base functionality of a mapping.
        /// </summary>
        protected class FunctionMappingBase<TDerived> : IFunctionMappingBase
        {
            /// <summary>
            /// The derived type that the mapping supports
            /// </summary>
            public Type DerivedType { get { return typeof(TDerived); } }
        }
    }

    /// <summary>
    /// Creates a mapping for a function that takes a single argument of variable type and returns the specified type.
    /// And provides the ability to dispatch to a function based on the type of the variable.
    /// </summary>
    public class TypeToFunctionMap<T, TReturn> : TypeToFunctionMapBase<T>
    {
        private List<IFunctionMapping> m_Mappings;

        /// <summary>
        /// Creates a new TypeToFunctionMap
        /// </summary>
        public TypeToFunctionMap()
        {
            m_Mappings = new List<IFunctionMapping>();
        }

        /// <summary>
        /// Adds a function to handle the specified type
        /// </summary>
        public void AddFunction<TDerived>(Func<TDerived, TReturn> function) where TDerived : T 
        {
            m_Mappings.Add(new FunctionMapping<TDerived>(function));
        }

        /// <summary>
        /// Dispatches to a method based on type and returns the result.
        /// </summary>
        public TReturn Evaluate(T arg)
        {
            IFunctionMapping mappedFunction = m_Mappings.First(
                mapping => mapping.DerivedType.IsInstanceOfType(arg));

            return mappedFunction.Evaluate(arg);
        }

        /// <summary>
        /// The set of mappings.
        /// </summary>
        protected override IEnumerable<IFunctionMappingBase> Mappings
        {
            get
            {
#if SILVERLIGHT
                return m_Mappings.Cast<IFunctionMappingBase>();
#else
                return m_Mappings;
#endif
            }
        }

        private interface IFunctionMapping : IFunctionMappingBase
        {
            TReturn Evaluate(T arg);
        }

        private class FunctionMapping<TDerived> : FunctionMappingBase<TDerived>, IFunctionMapping where TDerived : T
        {
            public FunctionMapping(Func<TDerived, TReturn> function)
            {
                Function = function;
            }

            public Func<TDerived, TReturn> Function { get; private set; }

            public TReturn Evaluate(T arg)
            {
                return Function((TDerived)arg);
            }
        }
    }

    /// <summary>
    /// Creates a mapping for a function that takes a two arguments and returns the specified type.
    /// The mapping dispatches based on the type of the first argument.
    /// And provides the ability to dispatch to a function based on the type of the variable.
    /// </summary>
    public class TypeToFunctionMap<TArg1, TArg2, TReturn> : TypeToFunctionMapBase<TArg1>
    {
        private List<IFunctionMapping> m_Mappings;

        /// <summary>
        /// Creates a new TypeToFunctionMap
        /// </summary>
        public TypeToFunctionMap()
        {
            m_Mappings = new List<IFunctionMapping>();
        }

        /// <summary>
        /// Adds a function to handle the specified type for the first argument
        /// </summary>
        public void AddFunction<TDerived>(Func<TDerived, TArg2, TReturn> function) where TDerived : TArg1
        {
            m_Mappings.Add(new FunctionMapping<TDerived>(function));
        }

        /// <summary>
        /// Dispatches to a method based on type and returns the result.
        /// </summary>
        public TReturn Evaluate(TArg1 arg1, TArg2 arg2)
        {
            IFunctionMapping mappedFunction = m_Mappings.First(
                mapping => mapping.DerivedType.IsInstanceOfType(arg1));

            return mappedFunction.Evaluate(arg1, arg2);
        }

        /// <summary>
        /// The set of mappings.
        /// </summary>
        protected override IEnumerable<IFunctionMappingBase> Mappings
        {
            get
            {
#if SILVERLIGHT
                return m_Mappings.Cast<IFunctionMappingBase>();
#else
                return m_Mappings;
#endif
            }
        }

        private interface IFunctionMapping : IFunctionMappingBase
        {
            TReturn Evaluate(TArg1 arg1, TArg2 arg2);
        }

        private class FunctionMapping<TDerived> : FunctionMappingBase<TDerived>, IFunctionMapping where TDerived : TArg1
        {
            public FunctionMapping(Func<TDerived, TArg2, TReturn> function)
            {
                Function = function;
            }

            public Func<TDerived, TArg2, TReturn> Function { get; private set; }

            public TReturn Evaluate(TArg1 arg1, TArg2 arg2)
            {
                return Function((TDerived)arg1, arg2);
            }
        }
    }

    /// <summary>
    /// Creates a mapping for a function that takes a two arguments and returns the specified type.
    /// The mapping dispatches based on the type of the first argument.
    /// And provides the ability to dispatch to a function based on the type of the variable.
    /// </summary>
    public class TypeToFunctionMap<TArg1, TArg2, TArg3, TReturn> : TypeToFunctionMapBase<TArg1>
    {
        private List<IFunctionMapping> m_Mappings;

        /// <summary>
        /// Creates a new TypeToFunctionMap
        /// </summary>
        public TypeToFunctionMap()
        {
            m_Mappings = new List<IFunctionMapping>();
        }

        /// <summary>
        /// Adds a function to handle the specified type for the first argument
        /// </summary>
        public void AddFunction<TDerived>(Func<TDerived, TArg2, TArg3, TReturn> function) where TDerived : TArg1
        {
            m_Mappings.Add(new FunctionMapping<TDerived>(function));
        }

        /// <summary>
        /// Dispatches to a method based on type and returns the result.
        /// </summary>
        public TReturn Evaluate(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            IFunctionMapping mappedFunction = m_Mappings.First(
                mapping => mapping.DerivedType.IsInstanceOfType(arg1));

            return mappedFunction.Evaluate(arg1, arg2, arg3);
        }

        /// <summary>
        /// The set of mappings.
        /// </summary>
        protected override IEnumerable<IFunctionMappingBase> Mappings
        {
            get
            {
#if SILVERLIGHT
                return m_Mappings.Cast<IFunctionMappingBase>();
#else
                return m_Mappings;
#endif
            }
        }

        private interface IFunctionMapping : IFunctionMappingBase
        {
            TReturn Evaluate(TArg1 arg1, TArg2 arg2, TArg3 arg3);
        }

        private class FunctionMapping<TDerived> : FunctionMappingBase<TDerived>, IFunctionMapping where TDerived : TArg1
        {
            public FunctionMapping(Func<TDerived, TArg2, TArg3, TReturn> function)
            {
                Function = function;
            }

            public Func<TDerived, TArg2, TArg3, TReturn> Function { get; private set; }

            public TReturn Evaluate(TArg1 arg1, TArg2 arg2, TArg3 arg3)
            {
                return Function((TDerived)arg1, arg2, arg3);
            }
        }
    }
}
