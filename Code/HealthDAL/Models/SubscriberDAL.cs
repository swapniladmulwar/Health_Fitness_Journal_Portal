using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthDAL
{
    [Table("Subscribers")]
    public class SubscriberDAL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriberId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public ICollection<FileDAL> Files { get; set; }
    }
}
