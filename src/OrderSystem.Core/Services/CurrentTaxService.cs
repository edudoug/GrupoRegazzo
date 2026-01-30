public class CurrentTaxService : ITaxCalculator
{
    public decimal Calculate(decimal totalValue)
        => totalValue * 0.3m;
}