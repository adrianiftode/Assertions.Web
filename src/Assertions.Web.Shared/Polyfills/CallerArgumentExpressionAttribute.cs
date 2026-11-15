#if !NET6_0_OR_GREATER
using System;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Internal polyfill of <c>CallerArgumentExpressionAttribute</c> for netstandard2.0,
    /// where the BCL does not have it. The compiler honours a polyfilled attribute across
    /// assembly boundaries, so consumers on net6.0+ use the BCL's type transparently.
    /// Wrapped so it is not compiled twice on net10.0/net11.0, which the test projects
    /// use when they compile this shared source.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}
#endif
