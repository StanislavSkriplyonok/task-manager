using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;

namespace MovieCatalog.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<TypeItem> Types { get; set; }
    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Note -> Type: one-to-many
        modelBuilder.Entity<Note>()
            .HasOne(n => n.Type)
            .WithMany(t => t.Notes)
            .HasForeignKey(n => n.TypeId)
            .OnDelete(DeleteBehavior.Restrict); // prevent deleting

        // Task -> Note: one-to-many
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Note)
            .WithMany(n => n.Tasks)
            .HasForeignKey(t => t.NoteId)
            .OnDelete(DeleteBehavior.Restrict); // prevent deleting

        // Task -> Status: one-to-many
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Status)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict); // prevent deleting
    }
}