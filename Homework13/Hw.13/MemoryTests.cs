using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Hw13;

[MemoryDiagnoser]
[MedianColumn]
[MinColumn]
[MaxColumn]
[MeanColumn]
[StdDevColumn]
[ExcludeFromCodeCoverage]
[ShortRunJob]
public class MemoryTests
{
    private MethodsForBenchmark _benchmarkMethod = null!;
    private int _testString = 1000;
    private static MethodInfo? _reflectionMethod;
        
    [GlobalSetup]
    public void Setup()
    {
        _benchmarkMethod = new MethodsForBenchmark2();
        _reflectionMethod = typeof(MethodsForBenchmark2).GetMethod(nameof(MethodsForBenchmark2.Reflection));			
    }

    [Benchmark(Description = "Simple")]
    public void TestSimpleMethod()
    {
        _benchmarkMethod.Simple(_testString);
    }
        
    [Benchmark(Description = "Virtual")]
    public void TestVirtualMethod()
    {
        _benchmarkMethod.Virtual(_testString);
    }
        
    [Benchmark(Description = "Static")]
    public void TestStaticMethod()
    {
        MethodsForBenchmark.Static(_testString);
    }
        
    [Benchmark(Description = "Generic")]
    public void TestGenericMethod()
    {
        _benchmarkMethod.Generic(_testString);
    }
        
    [Benchmark(Description = "Dynamic")]
    public void TestDynamicMethod()
    {
        _benchmarkMethod.Dynamic(_testString);
    }
        
    [Benchmark(Description = "Reflection")]
    public void TestReflectionMethod()
    {
        _reflectionMethod?.Invoke(_benchmarkMethod, new object?[] { _testString });
    } 
}