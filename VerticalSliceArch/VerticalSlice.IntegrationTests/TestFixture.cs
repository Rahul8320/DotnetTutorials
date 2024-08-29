using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;
using VerticalSlice.Api.Users;

namespace VerticalSlice.IntegrationTests;

public class TestFixture : IDisposable
{
    public AppDbContext Context { get; init; }
    public UserRepository UserRepository { get; init; }

    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new AppDbContext(options);
        UserRepository = new UserRepository(Context);
    }

    public async Task RemoveAllUsers()
    {
        Context.RemoveRange(Context.Users);
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Context.Dispose();
    }
}