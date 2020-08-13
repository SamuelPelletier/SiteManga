using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
