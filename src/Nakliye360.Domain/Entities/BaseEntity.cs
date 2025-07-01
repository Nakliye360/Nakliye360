using System.ComponentModel.DataAnnotations.Schema;

namespace Nakliye360.Domain.Entities
{
    public class BaseEntity
    {
        
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
