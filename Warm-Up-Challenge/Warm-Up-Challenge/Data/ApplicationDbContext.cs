using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warm_Up_Challenge.Models;

namespace Warm_Up_Challenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Warm_Up_Challenge.Models.Form> Form { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Form>()
                .Property<bool>("IsDeleted");
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is IIsDeleted entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;
                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;
                }
            }
            return base.SaveChanges();
        }

    }
}
