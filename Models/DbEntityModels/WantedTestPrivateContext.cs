using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FaceRecognitionApi.Models
{
    public partial class WantedTestPrivateContext : DbContext
    {
        public WantedTestPrivateContext(DbContextOptions<WantedTestPrivateContext> options) :base(options) { }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhotoFace> PhotoFaces { get; set; }
        public DbSet<PhotoProfile> PhotoProfiles { get; set; }
        public DbSet<PhotoFaceDescriptor> PhotoFaceDescriptors { get; set; }

        // Unable to generate entity type for table 'dbo.IpAddresses'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FacesStorage;Username=postgres;Password=user123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContactPhone).IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Initials).IsUnicode(false);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("date");

                entity.Property(e => e.RegisterDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RemoveDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("date")
                    .HasDefaultValueSql("current_date");
            });

            modelBuilder.Entity<PhotoFace>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.HasIndex(e => e.RowId)
                    .HasName("UQ__Photo__FFEE743086D66C32")
                    .IsUnique();

                entity.Property(e => e.PhotoId);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Mime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RowId);

                entity.Property(e => e.UpdatedDate).HasColumnType("date")
                    .HasDefaultValueSql("current_date");
            });

            modelBuilder.Entity<PhotoFaceDescriptor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id);

                entity.Property(e => e.Descriptor)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhotoFace)
                    .WithMany()
                    .HasForeignKey(d => d.PhotoFaceId)
                    .HasConstraintName("FK_PhotoFaceDescriptors_PhotoFace");
            });

            modelBuilder.Entity<PhotoProfile>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.HasIndex(e => e.RowId)
                    .HasName("UQ_PK_PhotoProfile")
                    .IsUnique();

                entity.Property(e => e.PhotoId);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Mime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RowId);

                entity.Property(e => e.UpdatedDate).HasColumnType("date")
                    .HasDefaultValueSql("current_date");
            });
        }

        public List<PhotoFaceModel> LoadPhotoFilesTest()
        {
            var dbPhotos = Persons
                .Where(person => !person.IsDelete 
                       && person.PhotoFaceId != null)
                .Join(
                    PhotoFaces, 
                    person => person.PhotoFaceId, 
                    photo => photo.PhotoId, 
                    (person, photo) => new PhotoFaceModel() { 
                        PhotoId = photo.PhotoId, 
                        Mime = photo.Mime.Substring(photo.Mime.LastIndexOf('/') + 1), 
                        Photo = photo.Photo })
                .ToList();

            return dbPhotos;
        }

        public async Task<List<PhotoFaceDescriptor>> LoadPhotoDescriptorsTest()
        {
            var photoDescriptors = await Persons
                .Where(person => !person.IsDelete 
                       && person.PhotoFaceId != null)
                .Join(
                    PhotoFaceDescriptors, 
                    person => person.PhotoFaceId, 
                    photo => photo.PhotoFaceId, 
                    (person, photo) => photo)
                .ToListAsync();

            return photoDescriptors;
        }
    }
}
