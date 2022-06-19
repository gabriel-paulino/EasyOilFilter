namespace EasyOilFilter.Presentation.Dto
{
    public class ProductValidUomDto
    {
        public string? Id { get; set; }
        public List<UoMPriceDto> ValidsUoMPrice { get; set; } = new List<UoMPriceDto>();
    }

    public class UoMPriceDto
    {
        public UoMPriceDto(string uom, decimal price)
        {
            UoM = uom;
            Price = price;
        }

        public string UoM { get; set; }
        public decimal Price { get; set; }
    }
}