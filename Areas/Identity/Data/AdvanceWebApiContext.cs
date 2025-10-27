using AdvanceWebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdvanceWebApi.Data;

public class AdvanceWebApiContext : IdentityDbContext<RegisterModel>
{
    public AdvanceWebApiContext(DbContextOptions<AdvanceWebApiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);         
    }
}
