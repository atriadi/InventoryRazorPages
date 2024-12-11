namespace InventorySandbox.Models.Pharmacy
{
    public class TransactionDetail : BaseEntity.BaseIdInt
    {
        public int TransactionId { get; set; }
        public virtual Transaction? Transaction { get; set; }
        public int MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}