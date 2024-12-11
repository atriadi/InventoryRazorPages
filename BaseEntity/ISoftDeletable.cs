namespace InventorySandbox.BaseEntity;

public interface ISoftDeletable
{
    bool? IsDeleted { get; set; }
}

