using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace SIEG_API.DTO
{
    public class J_PriceListDTO
    {
        public int sID { get; set; }
        public int pID { get; set; }
        public int? pPrice { get; set; }
        public int? pCount { get; set; }
        public string pSize { get; set; }

    }
}
