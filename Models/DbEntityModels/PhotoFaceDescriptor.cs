using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models 
{
    public class PhotoFaceDescriptor
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid PhotoFaceId { get; set; }
        public string Descriptor { get; set; }

        public PhotoFace PhotoFace { get; set; }
    }
}