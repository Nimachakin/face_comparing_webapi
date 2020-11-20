using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models
{
    public interface IAuditable
    {
        [Required]
        [Column(TypeName = "date")]
        DateTime UpdatedDate { get; set; }
    }
}