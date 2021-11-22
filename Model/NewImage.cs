using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImagesApi.Model;
public class NewImage
{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        public IFormFile Data { get; set; }
        [Required]
        [MaxLength(255), MinLength(1)]
        public string Title { get; set; }
        public string AltText { get; set; }


}