namespace InventorySandbox.Helper
{
    public class MessageHelper
    {
        // Property to store the status message
        public string StatusMessage { get; set; } = default!;

        // Constructor to initialize the status message (optional)
        public MessageHelper(string message = "")
        {
            StatusMessage = message;
        }

        // Method to set a success message (optional)
        public void SetSuccessMessage(string message)
        {
            StatusMessage = message;
        }

        // Method to set an error message (optional)
        public void SetErrorMessage(string message)
        {
            StatusMessage = message;
        }
    }
}
