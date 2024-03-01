using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
			//builder.Entity<Subject>().Property(s => s.SubjectId).ValueGeneratedOnAdd();
			//builder.Entity<Class>().Property(c => c.ClassId).ValueGeneratedOnAdd();
			builder.Entity<TempTimetable>().Property(t => t.ClassRoom).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Note).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.ClassType).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.EduProgram).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Week).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Experiment).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Status).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.SubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.ESubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.School).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.SubjectId).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Time).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Difficulty).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Shift).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.OpenStage).IsRequired(false);

			builder.Entity<Class>().Property(c => c.Experiment).IsRequired(false);
			builder.Entity<Class>().Property(c => c.Note).IsRequired(false);
			builder.Entity<Class>().Property(c => c.Enrolled).IsRequired(false);
			builder.Entity<Class>().Property(c => c.OpenStage).IsRequired(false);

			builder.Entity<ClassTime>().HasKey(c => new { c.ClassId, c.DateCount });

			builder.Entity<ClassTb>().HasKey(c => new { c.ClassId, c.TimetableId });

			builder.Entity<ClassUser>().HasKey(c => new { c.ClassId, c.UserId });
		}
		public void ClearTemptables()
		{
			Database.ExecuteSqlRaw("DELETE FROM TempTable");
		}
		public DbSet<PTimetable> Timetable { set; get; }
		public DbSet<Class> Class { set; get; }
		public DbSet<ClassTime> ClassTime { set; get; }
		public DbSet<Subject> Subject { set; get; }
		public DbSet<ClassTb> ClassTb { set; get; }
		public DbSet<ClassUser> ClassUser { set; get; }
		public DbSet<TempTimetable> TempTable { set; get; }
	}
}
