// See https://aka.ms/new-console-template for more information

// input types:
// [CURRENCY] [AMOUNT]
// [CURRENCY] [CURRENCY] [AMOUNT]
using System.Xml;
using System.Linq;

string URLString = "https://www.nationalbanken.dk/api/currencyratesxmlhistory?lang=da";
XmlTextReader reader = new XmlTextReader(URLString);
CurrencyList currencyList = new CurrencyList();
currencyList.InsertCurrency(new Currency(100, "DKK", "Danish Krone", ""));
const string HelpMessage = "Invalid input format. Use -h or --help for help.";
string date = "";
while (reader.Read())
{
    switch (reader.NodeType)
    {
        case XmlNodeType.Element: // The node is an element.
            decimal rate = 0;
            string currencyID = "";
            string name = "";

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "rate":
                        rate = Decimal.Parse(reader.Value);
                        break;
                    case "currency":
                        currencyID = reader.Value;
                        break;
                    case "name":
                        name = reader.Value;
                        break;
                    case "time":
                        date = reader.Value;
                        break;
                }
            }

            if (currencyID != "" && name != "")
            {
                Currency currency = new Currency(rate, currencyID, name, date);
                currencyList.InsertCurrency(currency);
            }
            break;
        case XmlNodeType.Text: //Display the text in each element.
            break;
        case XmlNodeType.EndElement: //Display the end of the element.
            break;
    }
}

Console.WriteLine(" --- Currency Converter --- ");
Console.WriteLine(" ");

while (true)
{

    Console.Write("Enter currency and amount: ");

    string readLine = Console.ReadLine() ?? "";
    if (String.IsNullOrEmpty(readLine))
    {
        continue;
    }
    switch (readLine)
    {
        case "exit":
            break;
        case "list":
            int longestName = currencyList.currencies.Max(c => c.Name.Length);
            foreach (Currency currency in currencyList.currencies)
            {
                Console.WriteLine(currency.CurrencyID.PadRight(5) + currency.Name.PadRight(longestName + 4) + currency.ExchangeRate.ToString().PadRight(10));
            }
            Console.WriteLine(" ");
            continue;
        case "-h":
        case "--help":
            Console.WriteLine("Usage: [CURRENCY] [AMOUNT]  will convert the amount to DKK");
            Console.WriteLine("Usage: [CURRENCY] [CURRENCY] [AMOUNT] will convert the amount from the first currency to the second currency");
            Console.WriteLine("Usage: exit");
            Console.WriteLine(" ");
            continue;
    }

    string[] inputLine = readLine.Split(" ");

    if (inputLine.Length < 2)
    {
        Console.WriteLine("Input is too short.");
        Console.WriteLine(HelpMessage);
        Console.WriteLine(" ");
        continue;
    }

    if (inputLine.Length == 2)
    {
        string currencyID = inputLine[0];
        decimal amount = 0;
        if (Decimal.TryParse(inputLine[1], out amount))
        {
            amount = Decimal.Parse(inputLine[1]);
            try
            {
                Currency currency = currencyList.GetCurrency(currencyID);
                Console.WriteLine("Exchange rate for " + currency.Name + " is " + currency.ExchangeRate);
                Console.WriteLine("Amount in DKK: " + decimal.Round(currency.ExchangeRate / 100 * amount, 2));
                Console.WriteLine(" ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(" ");
            }
        }
        else
        {
            Console.WriteLine(HelpMessage);
            Console.WriteLine(" ");
        }
        continue;

    }

    if (inputLine.Length == 3)
    {
        string currencyID1 = inputLine[0];
        string currencyID2 = inputLine[1];
        decimal amount = 0;
        if (Decimal.TryParse(inputLine[2], out amount))
        {
            amount = Decimal.Parse(inputLine[2]);
            try
            {
                Currency currency1 = currencyList.GetCurrency(currencyID1);
                Currency currency2 = currencyList.GetCurrency(currencyID2);
                decimal convertedAmount = decimal.Round(currency1.ExchangeRate * amount / currency2.ExchangeRate, 2);
                Console.WriteLine($"'{amount}' {currency1.CurrencyID} converts to '{convertedAmount}' {currency2.CurrencyID}");
                Console.WriteLine(" ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(" ");
            }
        }
        else
        {
            Console.WriteLine(HelpMessage);
            Console.WriteLine(" ");
        }
        continue;
    }
}




