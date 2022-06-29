using System;
using System.ComponentModel.DataAnnotations;


namespace L15.Models
{
    public class Dog
    {
        public int Id { get; set; }

        public string MintNumber { get; set; }

        public DateTime MintDate { get; set; }
        [Required(ErrorMessage = "A weapon is required")]
        public string Weapon { get; set; }
        [Required(ErrorMessage = "Armor color is required")]
        public string ArmorColor { get; set; }

        public bool ForSale { get; set; }
        [Range(0, 630.50, ErrorMessage = "Prices between 0 and $630.50 only")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int RarityId { get; set; }

        public string Companion { get; set; }
        [Required(ErrorMessage = "Please enter Accessory")]
        [RegularExpression("(Eye Patch|Samurai Mask|Night Vision)", ErrorMessage = "Not a valid accessory")]
        public string Accessory { get; set; }
        [Required(ErrorMessage = "Please enter CallSign")]
        [RegularExpression("(CC-[POIUJM]{2}-[0-9]{3})", ErrorMessage = "Not a valid call sign")]
        public string CallSign { get; set; }
    }
}

