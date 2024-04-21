

public class Currency
{
    public decimal ExchangeRate { get; set; }
    public string CurrencyID { get; set; }

    public string Name { get; set; }

    public string Date { get; set; }


    public Currency(decimal exchangeRate, string currencyID, string Name, string Date)
    {
        this.ExchangeRate = exchangeRate;
        this.CurrencyID = currencyID;
        this.Name = Name;
        this.Date = Date;
    }

    public Currency(decimal exchangeRate, string currencyID, string Name)
    {
        this.ExchangeRate = exchangeRate;
        this.CurrencyID = currencyID;
        this.Name = Name;
        this.Date = "";
    }
}
public class CurrencyList
{
    public List<Currency> currencies { get; set; } = new List<Currency>();

    public void InsertCurrency(Currency currency)
    {
        bool currencyExists = currencies.Exists(c => c.CurrencyID == currency.CurrencyID && c.Date == currency.Date);
        if (!currencyExists)
        {
            currencies.Add(currency);
        }
    }

    public void DeleteCurrency(string currencyID)
    {
        currencies.RemoveAll(c => c.CurrencyID == currencyID);
    }

    public Currency GetCurrency(string currencyID)
    {
        var foundCurrency = currencies.Find(c => c.CurrencyID.ToLower() == currencyID.ToLower());
        if (foundCurrency == null)
        {
            throw new Exception($"Currency '{currencyID}' not found.");
        }
        return foundCurrency;
    }
}