using System;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionApi.Models
{
    public partial class AspNetUserClaim
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public AspNetUser User { get; set; }
    }
}
