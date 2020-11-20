using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models
{
    public partial class PhotoFace : IAuditable
    {
        [Required]
        public Guid PhotoId { get; set; }
        [Required]
        public Guid RowId { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public string Mime { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime UpdatedDate { get; set; }
    }
}
