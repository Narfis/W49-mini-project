
public enum Countries
{
    USA,
    Sweden,
    Germany,
}

class Price
{
    private double Value { get; set; }
    private string Currency { get; set; }
    public Price(double value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public double GetValue()
    {
        return Value;
    }

    public string GetCurrency()
    {
        return Currency;
    }
}
class Asset
{
    protected Price? Price { get; set; }
    protected DateTime PurchaseDate { get; set; }
    protected string? Manufacturer { get; set; }
    protected string? Model { get; set; }
    protected Countries Country { get; set; }

    public DateTime GetPurchaseDate()
    {
        return PurchaseDate;
    }

    public string GetManufacturer()
    {
        return Manufacturer;
    }

    public string GetModel()
    {
        return Model;
    }
    public Countries GetCountry()
    {
        return Country;
    }

    public Price GetPrice()
    {
        return Price;
    }
}

class Smartphone : Asset
{
    public Smartphone(Price price, DateTime purchaseDate, string manufacturer, string model, Countries country)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        Manufacturer = manufacturer;
        Model = model;
        Country = country;
    }
}

class Computer : Asset
{
    public Computer(Price price, DateTime purchaseDate, string manufacturer, string model, Countries country)
    {
        Price = price;
        PurchaseDate = purchaseDate;
        Manufacturer = manufacturer;
        Model = model;
        Country = country;
    }
}

class AssetManager
{
    private List<Asset> AssetList = new List<Asset>();
    public void AddAsset(Asset asset)
    {
        AssetList.Add(asset);
    }

    private void SetColor(Asset asset)
    {
        switch (asset.GetPurchaseDate())
        {
            case DateTime date when date < DateTime.Now.AddMonths(-36):
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
            case DateTime date when date < DateTime.Now.AddMonths(-33):
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case DateTime date when date < DateTime.Now.AddMonths(-30):
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }

    public void PrettyPrint()
    {
        LiveCurrency.FetchRates();

        var sortedAssets = AssetList.OrderBy(asset => asset.GetCountry().ToString()).ThenBy(asset => asset.GetPurchaseDate());

        const int padding = -15;

        Console.WriteLine(
            $"{"Office", padding}" +
            $"{"Asset", padding}" +
            $"{"Brand", padding}" +
            $"{"Model", padding}" +
            $"{"Price (USD)", padding}" +
            $"{"Price (Local)", padding}" +
            $"{"Purchase Date"}"
        );

        foreach (var asset in sortedAssets)
        {
            SetColor(asset);
            double convertedValue = LiveCurrency.Convert(asset.GetPrice().GetValue(), asset.GetPrice().GetCurrency(), "USD");

            Console.WriteLine(
                $"{asset.GetCountry(),padding}" +
                $"{asset.GetType().ToString(), padding}" +
                $"{asset.GetManufacturer(), padding}" +
                $"{asset.GetModel(),padding}" +
                $"{convertedValue,padding:F2}" +
                $"{asset.GetPrice().GetValue(),padding}" +
                $"{asset.GetPurchaseDate()}"
            );
        }
        Console.ResetColor();
    }
}