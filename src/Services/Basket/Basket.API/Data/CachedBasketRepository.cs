

namespace Basket.API.Data
{
    #region Decorator&Proxy&Adapter Explanation
    //public interface IJsonParser
    //{
    //    void Parse();
    //}
    //public class JsonParser : IJsonParser
    //{
    //    public void Parse()
    //    {
    //        Console.WriteLine("mostaf alogorithm");
    //    }
    //}
    //public class JsonAdaptee:IJsonConverter
    //{
    //    private readonly IJsonParser _jsonParser;

    //    public JsonAdaptee(IJsonParser jsonParser)
    //    {
    //        _jsonParser = jsonParser;
    //    }

    //    public void Convert()
    //    {
    //        _jsonParser.Parse();
    //    }
    //}
    //public class MyProgram
    //{
    //    void Main()
    //    {
    //        JsonParser jsonParser= new JsonParser();
    //        IJsonConverter jsonConverter = new JsonAdaptee(jsonParser);
    //        Adapter adapter = new Adapter(jsonConverter);
    //        adapter.Convert();
    //    }
    //}
    //public class DecoratorJsonLogger : IJsonConverter
    //{
    //    private readonly IJsonConverter _jsonConverter;

    //    public DecoratorJsonLogger(IJsonConverter jsonConverter)
    //    {
    //        _jsonConverter = jsonConverter;
    //    }
    //    public void Convert()
    //    {
    //        // check you are authenticate
    //        Console.WriteLine("convert to json log");
    //        _jsonConverter.Convert();
    //    }
    //}
    //// dll JsonConverter
    //public  interface IJsonConverter
    //{
    //    void Convert();
    //}
    //public class Adapter
    //{
    //    private readonly IJsonConverter _jsonConverter;

    //    public Adapter(IJsonConverter jsonConverter)
    //    {
    //        _jsonConverter = jsonConverter;
    //    }
    //    public void Convert()
    //    {
    //        Console.WriteLine("dll alogoritm");
    //        _jsonConverter.Convert();
    //    }


    //}
    #endregion

    // Here we implmented Proxy Pattern and Decorator Pattern 
    //---------------------------------------------------------
    // Proxy Pattern => by Adding a cached layer (CachedBasketRepository) as a gate which delegates calls to actual service (object)
    // but if i wanna make this i should inject (CachedBasketRepository ) real object not it`s abstraction then 
    // use it`s logic but i will work around this in IOC container by defining decaration provider by two ways  
    //-------------------------------
    // Decorator Pattern ==> Extend Functionality of BasketRepo by adding new  Functionality Cached Layer
    public class CachedBasketRepository (IBasketRepository basketRepository
        , ILogger<CachedBasketRepository>logger
        , IDistributedCache cache)
        : IBasketRepository
    {
        public  async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            // Delete from Database 
            await basketRepository.DeleteBasket(username, cancellationToken);
            // Delete from Cache
            await cache.RemoveAsync(username , cancellationToken);
            // Returning result of Deletion 
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            // Get basket From Cache as Text or byte[] 
            var cachedbasket = await cache.GetStringAsync(username, cancellationToken);
            // if found
            if (!string.IsNullOrEmpty(cachedbasket))
            // Deserilize to Shopping Cart object form 
                {
                logger.LogInformation("Writing From Redis Cache");
                var ShoppingCart =  JsonSerializer.Deserialize<ShoppingCart>(cachedbasket)!;
                return ShoppingCart;
                }
               
            // if not found => then get basket from Database access layer Basket Repo 
            var basket = await basketRepository.GetBasket(username, cancellationToken);
            // then Add it to Cached Basekt  Layer as byte [] 
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);   

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            // After storing to Database
            await basketRepository.StoreBasket(basket, cancellationToken);
            // Store it in Cache layer as Text or byte[]
            await cache.SetStringAsync(basket.UserName , JsonSerializer.Serialize(basket), cancellationToken);
            // then return the basket stored 
            return basket; 

        }
    }
}
