using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using static BaseContext.User;

namespace BaseContext;

public partial class UserDataBaseContext : DbContext
{
    private readonly IConfiguration _config;

    public UserDataBaseContext(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public UserDataBaseContext(DbContextOptions<UserDataBaseContext> options, IConfiguration config)
        : base(options)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("sex", new[] { "мужчина", "женщина", "иное" });

        //modelBuilder.HasPostgresEnum<SexEnum>();

    //var converter = new EnumToStringConverter<SexEnum>();

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Birthdaydate).HasColumnName("birthdaydate");
            entity.Property(e => e.Children).HasColumnName("children");
            //entity.Property(e => e.Sex).HasColumnName("sex");//.HasConversion(converter);
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasDefaultValueSql("'noname'::character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Secondname)
                .HasMaxLength(50)
                .HasColumnName("secondname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
