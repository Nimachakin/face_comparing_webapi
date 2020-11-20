using System;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionApi.Models
{
    public partial class AspNetUserRole
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid RoleId { get; set; }

        public AspNetRole Role { get; set; }
        public AspNetUser User { get; set; }
    }
}
