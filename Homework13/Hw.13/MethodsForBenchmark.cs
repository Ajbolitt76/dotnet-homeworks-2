using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Hw13;

[ExcludeFromCodeCoverage]
public class MethodsForBenchmark
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Simple(int s) => s + s;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual int Virtual(int s) => s + s;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Static(int s) => s + s;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public T Generic<T>(T s) => s;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Dynamic(dynamic s) => s + s;
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Reflection(int s) => s + s;
}

[ExcludeFromCodeCoverage]
public class MethodsForBenchmark2 : MethodsForBenchmark
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override int Virtual(int s) => s + s;
}