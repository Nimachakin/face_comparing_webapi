using System;
using System.Xml.Serialization;

namespace FaceRecognitionApi.Models
{
    [Serializable]
    public class MetaDataPhoto
    {
        public Guid? PhotoId { get; set; }
        public Guid? RowId { get; set; }
        public string Description { get; set; }
        public string Mime { get; set; }
        [XmlIgnore]
        public string Path { get; set; }
    }
}