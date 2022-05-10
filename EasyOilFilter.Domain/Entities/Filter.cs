using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Filter
    {

        public Filter()
        {
            _details = new List<FilterDetail>();
        }

        private IEnumerable<FilterDetail> _details;

        public int Id { get; set; }
        public decimal Price { get; set; }
        public FilterType Type { get; set; }
        public IReadOnlyCollection<FilterDetail> Details { get => _details.ToArray(); }
    }
}