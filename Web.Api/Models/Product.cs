using System.ComponentModel.DataAnnotations;

namespace Product.Api.Models
{
    public class Product
    {
        [PositiveNumber]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [PositiveNumber(ErrorMessage = "Price must be positive")]
        public Decimal Price { get; set; }
        [Required]
        [PositiveNumber (ErrorMessage ="Quantity must be positive")]
        public int Quantity { get; set; }
        public string Description { get; set; }

    }
}

public class PositiveNumberAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value != null 
            && (int.TryParse(value.ToString(), out var a) 
            && a >= 0);
    }
}