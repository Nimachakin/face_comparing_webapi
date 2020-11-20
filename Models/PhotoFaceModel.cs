using System;

namespace FaceRecognitionApi.Models
{
    public class PhotoFaceModel
    {
        public Guid PhotoId { get; set; }
        public string Mime { get; set; }
        public byte[] Photo { get; set; }
    }
}