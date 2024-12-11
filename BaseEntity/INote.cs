using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.BaseEntity;

public interface INote
{
    [StringLength(2000)]
    public string? Note { get; set; }
}

