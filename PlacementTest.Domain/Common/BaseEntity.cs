using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementTest.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
