using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognitionApi.Models
{
    [Serializable]
    public class PersonPhoto : IAuditable
    {
        public PersonPhoto()
        {
            Persons = new HashSet<Person>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PhotoId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RowId { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }

        [StringLength(20)]
        public string Mime { get; set; }

        public virtual ICollection<Person> Persons { get; set; }

        [Column(TypeName = "date")]
        public DateTime UpdatedDate { get; set; }
    }
}