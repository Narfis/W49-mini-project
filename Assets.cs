
public enum Countries
{
    USA,
    Sweden,
    Germany
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
}
class Asset
{
    protected Price? Price { get; set; }
    protected DateTime PurchaseDate { get; set; }
    protected string? Manufacturer { get; set; }
    protected string? Model { get; set; }
    protected Countries Country { get; set; }
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
}