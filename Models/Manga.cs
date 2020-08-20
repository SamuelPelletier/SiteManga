using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    [Table("Manga")]
    public class Manga
    {
        public enum Colors
        {
            Red = 1,
            Green = 2,
            Blue = 3
        }


        public int Id { get; set; }
        [Required]
        public string Name { get ; set ; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public int Color { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        public virtual Editor Editor { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<MangaOrder> MangaOrders { get; set; }
    }
}
