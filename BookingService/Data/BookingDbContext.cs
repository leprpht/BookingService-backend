using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<HousingInfo> Housings { get; set; } = null!;
    public DbSet<Stay> Stays { get; set; } = null!;
}