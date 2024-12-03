using System.Xml;


class Currency
{
    public static string USD = "USD";
    public static string SEK = "SEK";
    public static string EUR = "EUR";
    private string CurrencyCode { get; set; }
    private double ExchangeRateFromEUR { get; set; }
    public Currency(string currencyCode, double exchangeRateFromEUR)
    {
        CurrencyCode = currencyCode;
        ExchangeRateFromEUR = exchangeRateFromEUR;
    }

    public string GetCurrencyCode()
    {
        return CurrencyCode;
    }

    public double GetExchangeRateFromEUR()
    {
        return ExchangeRateFromEUR;
    }
}


    public class LiveCurrency // Class that handles fetching the exchange rates and converting currencies
{
    private static List<Currency> currencyList = new List<Currency>();


    public static void FetchRates()
    {
        string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document

        XmlTextReader reader = new XmlTextReader(url);
        while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                while (reader.MoveToNextAttribute()) 
                {
                    if (reader.Name == "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                    {
                        string currencyCode = reader.Value;
                        reader.MoveToNextAttribute();
                        double rate = double.Parse(reader.Value.Replace(".", ","));
                        currencyList.Add(new Currency(currencyCode, rate));
                    }
                }
            }
        }
    }

    public static double Convert(double input, string fromCurrency, string toCurrency) // Method that uses the fetched rates to convert between the given rates via Euro
    {
        double value = 0;
            
        if (fromCurrency == "EUR")
        {
            value = input * currencyList.Find(c => c.GetCurrencyCode() == toCurrency).GetExchangeRateFromEUR();
        }
        else if (toCurrency == "EUR")
        {
            value = input / currencyList.Find(c => c.GetCurrencyCode() == fromCurrency).GetExchangeRateFromEUR();
        }
        else
        {
            value = input / currencyList.Find(c => c.GetCurrencyCode()== fromCurrency).GetExchangeRateFromEUR();
            value *= currencyList.Find(c => c.GetCurrencyCode() == toCurrency).GetExchangeRateFromEUR();
        }

        return value;
    }
}
