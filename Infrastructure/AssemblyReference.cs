using System.Reflection;

namespace Infrastructure
{
    internal static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
