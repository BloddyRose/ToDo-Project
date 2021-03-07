using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ToDo_Project.Data
{
    public partial class TasksModel : DbContext
    {
        public TasksModel()
            : base("name=DbTasks")
        {
        }

        public virtual DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
