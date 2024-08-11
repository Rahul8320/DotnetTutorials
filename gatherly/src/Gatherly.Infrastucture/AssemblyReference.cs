using System.Reflection;

namespace Gatherly.Infrastucture;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}