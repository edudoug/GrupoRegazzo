public class TaxReformService : ITaxCalculator
{
    public decimal Calculate(decimal totalValue)
        => totalValue * 0.2m;
}