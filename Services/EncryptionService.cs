namespace PizzaShop.Services;

using Microsoft.AspNetCore.DataProtection;

public class EncryptionService
{
    private readonly IDataProtector _protector;

    // Constructor to initialize the IDataProtector using dependency injection
    public EncryptionService(IDataProtectionProvider provider)
    {
        // 'MyPurpose' is a unique string that ensures different protection policies for different purposes
        _protector = provider.CreateProtector("MyPurpose");
    }

    // Method to encrypt plain text data
    public string EncryptData(string plainText)
    {
        return _protector.Protect(plainText);
    }

    // Method to decrypt the encrypted data
    public string DecryptData(string encryptedData)
    {
        try
        {
            return _protector.Unprotect(encryptedData);
        }
        catch (Exception ex)
        {
            // If decryption fails (e.g., data is tampered or invalid), handle the exception
            return $"Decryption failed: {ex.Message}";
        }
    }
}