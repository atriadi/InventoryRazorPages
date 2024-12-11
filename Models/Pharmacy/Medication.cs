using InventorySandbox.Helper;

namespace InventorySandbox.Models.Pharmacy
{
    public class Medication : BaseEntity.BaseCodeName
    {
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public EnumItemCategory Category { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}