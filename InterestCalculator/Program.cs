// System workflow
// Choose the type of calculation you want to perform
// get wizard that will guide you through the process based on chosen calculation
// get the result of the calculation

while (true)
{
    Console.WriteLine("Choose the type of calculation you want to perform: (Simple Interest = 1, Compound Interest = 2) or 'exit' to quit.");
    string input = Console.ReadLine() ?? "";
    if (input == "exit")
    {
        break;
    }

    switch (input)
    {
        case "1":
            SimpleInterest();

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            break;
        case "2":
            CompoundInterest();

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            break;
        default:
            Console.WriteLine("Invalid input. Please enter a valid number.");
            Console.WriteLine(" ");
            break;
    }
}

void CompoundInterest()
{
    double principal;
    double annualRate;
    CompoundTypes compoundType;
    double timePeriod;

    principal = promptPrincipal();
    annualRate = promptAnnualRate();
    compoundType = promptCompoundType();
    timePeriod = promptTimePeriod();

    double result = InterestCalculator.CalculateCompoundInterest(principal, annualRate, timePeriod, compoundType);
    Console.WriteLine($"Principal: {principal}");
    Console.WriteLine($"Annual rate: {annualRate}");
    Console.WriteLine($"compound type: {compoundType}");
    Console.WriteLine($"Time period: {timePeriod} years");
    Console.WriteLine($"Compound interest: {double.Round(result, 2)}");
    Console.WriteLine($"Total: {double.Round(principal + result, 2)}");
}

void SimpleInterest()
{
    double principal;
    double annualRate;
    double timePeriod;

    principal = promptPrincipal();
    annualRate = promptAnnualRate();
    timePeriod = promptTimePeriod();

    double result = InterestCalculator.CalculateSimpleInterest(principal, annualRate, timePeriod);
    Console.WriteLine($"Principal: {principal}");
    Console.WriteLine($"Annual rate: {annualRate}");
    Console.WriteLine($"Time period: {timePeriod} years");
    Console.WriteLine($"Interest: {double.Round(result, 2)}");
    Console.WriteLine($"Total: {double.Round(principal + result, 2)}");

}

double promptPrincipal()
{
    double principal;
    while (true)
    {
        Console.WriteLine("Enter the principal amount:");
        string principalAmount = Console.ReadLine() ?? "";
        if (double.TryParse(principalAmount, out principal))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
    return principal;
}

double promptAnnualRate()
{
    double annualRate;
    while (true)
    {
        Console.WriteLine("Enter the annual interest rate:");
        string inputAnnualRate = Console.ReadLine() ?? "";
        if (double.TryParse(inputAnnualRate, out annualRate))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
    return annualRate;
}

double promptTimePeriod()
{
    double timePeriod;
    while (true)
    {
        Console.WriteLine("Time period in years:");
        string inputTimePeriod = Console.ReadLine() ?? "";
        if (double.TryParse(inputTimePeriod, out timePeriod))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
    return timePeriod;
}

CompoundTypes promptCompoundType()
{
    CompoundTypes compoundType = CompoundTypes.Annually;
    while (true)
    {
        Console.WriteLine($"Enter how the compound rate: ({CompoundTypes.Annually}, {CompoundTypes.SemiAnnually}, {CompoundTypes.Quarterly}, {CompoundTypes.Monthly}, {CompoundTypes.Daily})");
        string inputCompoundRate = Console.ReadLine() ?? "";
        inputCompoundRate = inputCompoundRate.ToLower();
        bool compoundTypeSelected = true;
        switch (inputCompoundRate)
        {
            case "annually":
                compoundType = CompoundTypes.Annually;
                break;
            case "semi annually":
            case "semiannually":
            case "semi-annually":
                compoundType = CompoundTypes.SemiAnnually;
                break;
            case "quarterly":
                compoundType = CompoundTypes.Quarterly;
                break;
            case "monthly":
                compoundType = CompoundTypes.Monthly;
                break;
            case "daily":
                compoundType = CompoundTypes.Daily;
                break;
            default:
                compoundTypeSelected = false;
                Console.WriteLine("Invalid input. Please enter a valid compound rate.");
                break;
        }
        if (compoundTypeSelected)
        {
            break;
        }
    }
    return compoundType;
}


class SimpleInterestCalculator
{
    public double CalculateInterest(double principal, double rate, double time)
    {
        return principal * rate * time / 100;
    }
}
class InterestCalculator
{
    static public double CalculateSimpleInterest(double principal, double rate, double time)
    {
        return principal * rate * time / 100;
    }
    static public double CalculateCompoundInterest(double principal, double rate, double time, CompoundTypes compoundType)
    {
        double compoundInterest = 0;
        switch (compoundType)
        {
            case CompoundTypes.Annually:
                compoundInterest = principal * Math.Pow(1 + rate / 100, time) - principal;
                break;
            case CompoundTypes.SemiAnnually:
                compoundInterest = principal * Math.Pow(1 + rate / 200, time * 2) - principal;
                break;
            case CompoundTypes.Quarterly:
                compoundInterest = principal * Math.Pow(1 + rate / 400, time * 4) - principal;
                break;
            case CompoundTypes.Monthly:
                compoundInterest = principal * Math.Pow(1 + rate / 1200, time * 12) - principal;
                break;
            case CompoundTypes.Daily:
                compoundInterest = principal * Math.Pow(1 + rate / 36500, time * 365) - principal;
                break;
        }
        return compoundInterest;
    }
};
public enum CompoundTypes
{
    Annually,
    SemiAnnually,
    Quarterly,
    Monthly,
    Daily
};

