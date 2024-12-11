namespace InventorySandbox.Models.Pharmacy
{
    public class Sale : BaseEntity.BaseDomainDeep
    {
        public DateTime DocumentDate { get; set; }
        public int MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}