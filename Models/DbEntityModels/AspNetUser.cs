using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        [Required]
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public bool PhoneNumberConfirmed { get; set; }
        [Required]
        public bool TwoFactorEnabled { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LockoutEndDateUtc { get; set; }
        [Required]
        public bool LockoutEnabled { get; set; }
        [Required]
        public int AccessFailedCount { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Initials { get; set; }
        public string ContactPhone { get; set; }
        public Guid? RequestedRoleId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsBlocked { get; set; }
        public ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
