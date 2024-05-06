using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AuthContext.Presentation.Auth;

public class DefaultTokenProvider<T> : DataProtectorTokenProvider<T>
    where T : class
{
    public DefaultTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<T>> logger) : base(dataProtectionProvider, options, logger)
    {
    }
}

