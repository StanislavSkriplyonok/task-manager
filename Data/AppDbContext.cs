using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;

namespace TaskManager.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<TypeItem> Types { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "To Do" },  
            new Status { Id = 2, Name = "In Progress" },
            new Status { Id = 3, Name = "Completed" },
            new Status { Id = 4, Name = "Cancelled" }
        );

        modelBuilder.Entity<TypeItem>().HasData(
            new TypeItem { Id = 1, Name = "Work" },
            new TypeItem { Id = 2, Name = "Study" },
            new TypeItem { Id = 3, Name = "Entertainment" },
            new TypeItem { Id = 4, Name = "Other" }
        );


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

        // User: unique constraints + enum stored as string
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>(); // stores "User"/"Admin" instead of 0/1
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }


    private void AddTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITrackable && e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            ((ITrackable)entry.Entity).CreationTime = DateTimeOffset.UtcNow;
        }
    }
}