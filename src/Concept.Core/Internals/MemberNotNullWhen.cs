// ReSharper disable CheckNamespace
#if NETSTANDARD2_1
namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    internal sealed class MemberNotNullWhenAttribute : Attribute
    {
        public bool ReturnValue { get; }
        public string[] Members { get; }

        public MemberNotNullWhenAttribute(bool returnValue, string member)
        {
            ReturnValue = returnValue;
            Members = [member];
        }

        public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
        {
            ReturnValue = returnValue;
            Members = members;
        }
    }
}
#endif