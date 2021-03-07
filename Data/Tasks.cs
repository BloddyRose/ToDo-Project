namespace ToDo_Project.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Tasks
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [StringLength(150)]
        public string TaskTitle { get; set; }

        [Required]
        [StringLength(350)]
        public string TaskDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime TaskDate { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskStatus { get; set; }
    }
}
