using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities
{
    public class FilterDetail : BaseEntity
    {
        public Guid FilterId { get; set; }
        public string Manufacturer { get; set; }
        public string Code { get; set; }      
    }
}