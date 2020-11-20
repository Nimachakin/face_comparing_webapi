using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models
{
    public partial class Person : Entity, IAuditable
    {
        [Required]
        public bool Gender { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        public Guid? PhotoFaceId { get; set; }
        public Guid? PhotoProfileId { get; set; }
        public DateTime? RemoveDate { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime UpdatedDate { get; set; }
    }
}
