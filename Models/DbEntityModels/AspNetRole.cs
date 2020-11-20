using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionApi.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Discriminator { get; set; }

        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
