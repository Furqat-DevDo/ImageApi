using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ImagesApi.Entity
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(5242880)]
        public byte[] Data { get; set; }
        [Required]
        [MaxLength(255), MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20)]
        public string ContentType { get; set; }
        [MaxLength(255)]
        public string AltText { get; set; }
        public Image(string title, string contentType, string altText, byte[] data)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.ContentType = contentType;
            this.AltText = altText;
            Data = data;
        }
    }
}