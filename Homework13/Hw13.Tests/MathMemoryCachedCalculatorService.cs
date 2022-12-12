using System.Collections.Generic;
using System.Threading.Tasks;
using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services;

namespace Hw13.Tests;

public class MathMemoryCachedCalculatorService : IMathCalculatorService
{
    private readonly Dictionary<string, double> _cache = new();
    private readonly IMathCalculatorService _simpleCalculator;

    public MathMemoryCachedCalculatorService(IMathCalculatorService simpleCalculator)
    {
        _simpleCalculator = simpleCalculator;
    }
    
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        if (_cache.TryGetValue(expression, out var value))
            return new CalculationMathExpressionResultDto
            {
                IsSuccess = true,
                Result = value
            };

        var newCalculation = await _simpleCalculator.CalculateMathExpressionAsync(expression);
		
        if (newCalculation.IsSuccess)
            _cache[expression] = newCalculation.Result;

        return newCalculation;
    }
}