using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Presentation.Test.Abstraction;

public class WebAppFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
        builder.ConfigureLogging((logger) => {
            logger.ClearProviders();
        });

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

			services.AddDbContext<AppDbContext>(opt =>
			{
				opt.UseInMemoryDatabase(Guid.NewGuid().ToString());
                opt.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
			});
		});
	}
}
