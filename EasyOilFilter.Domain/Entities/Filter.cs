using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Filter: BaseEntity
    {

        public Filter()
        {
            _details = new List<FilterDetail>();
        }

        private IEnumerable<FilterDetail> _details;

        public decimal Price { get; set; }
        public FilterType Type { get; set; }
        public IReadOnlyCollection<FilterDetail> Details { get => _details.ToArray(); }
    }
}