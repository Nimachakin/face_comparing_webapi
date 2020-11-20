using System;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionApi.Models
{
    public partial class AspNetUserLogin
    {
        [Required]
        public string LoginProvider { get; set; }
        [Required]
        public string ProviderKey { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public AspNetUser User { get; set; }
    }
}
