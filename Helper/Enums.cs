using System.ComponentModel.DataAnnotations;

namespace InventorySandbox.Helper
{
    public enum EnumItemCategory : short
    {
        [Display(Name = "Alat Kesehatan")]
        MedicalDevices = 0,
        [Display(Name = "Obat")]
        Medicine = 1
    }
}
