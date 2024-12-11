using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySandbox.BaseEntity;

public interface IVersion
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Timestamp]
    public byte[]? Version { get; set; }
}
