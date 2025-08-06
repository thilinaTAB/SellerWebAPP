using System.ComponentModel.DataAnnotations;

namespace Seller_Web_App.Models
{
    public class Material
    {
        public int MaterialID { get; set; }

        [Display(Name = "Material Name")]
        public string MaterialName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}