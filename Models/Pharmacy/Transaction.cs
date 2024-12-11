namespace InventorySandbox.Models.Pharmacy
{
    public class Transaction : BaseEntity.BaseDomainDetail
    {
        public Transaction()
        {
            TransactionDetails = new HashSet<TransactionDetail>();
        }

        public string DocumentNumber { get; set; } = default!;
        public DateTime DocumentDate { get; set; }

        public virtual ICollection<TransactionDetail>? TransactionDetails { get; set; }
    }
}