using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightAPITest.Driver;

public class PlaywrightDriver : System.IDisposable
{
    private readonly Task<IAPIRequestContext?>? _requestContext;

    public PlaywrightDriver()
    {
        _requestContext = CreateApiContext();
    }

    public IAPIRequestContext? ApiRequestContext => _requestContext?.GetAwaiter().GetResult();

    public void Dispose()
    {
        _requestContext?.Dispose();
    }

    private async Task<IAPIRequestContext?> CreateApiContext()
    {
        var playwright = await Playwright.CreateAsync();

        return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = "https://reqres.in/",
            IgnoreHTTPSErrors = true
        });
    }
}