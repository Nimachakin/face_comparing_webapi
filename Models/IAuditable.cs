using System;
using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionApi.Models
{
    public interface IAuditable
    {
        [Required]
        DateTime UpdatedDate { get; set; }
    }
}