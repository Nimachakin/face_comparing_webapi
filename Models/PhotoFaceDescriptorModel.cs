using System;

namespace FaceRecognitionApi.Models
{
    public class PhotoFaceDescriptorModel
    {
        public Guid PhotoId { get; set; }
        public string Descriptor { get; set; }
    }
}