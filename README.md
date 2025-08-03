# Cryptung

Cryptung is a lightweight .NET library for secure string encryption and decryption using AES-256 encryption. It provides a simple interface for encrypting sensitive data with a password-based key derivation using SHA-256.

## Features

- Secure string encryption using AES-256
- Password-based key derivation with SHA-256
- Automatic initialization vector (IV) generation for each encryption
- Recryption capability to update encryption keys without decrypting to plaintext
- Simple, fluent API design

## Installation

You can install Cryptung via NuGet package manager:

```bash
# Using .NET CLI
dotnet add package Cryptung

# Using Package Manager
Install-Package Cryptung
```

## Usage

### Basic Encryption and Decryption

```csharp
using Cryptung;

// Initialize with your encryption key
var encryptionKey = "The quick brown fox jumps over the lazy dog";
var cryptungService = new CryptungService(encryptionKey);

// Encrypt a string
string plainText = "Sensitive data to protect";
string encrypted = cryptungService.Encrypt(plainText);

// Decrypt the string
string decrypted = cryptungService.Decrypt(encrypted);
Console.WriteLine(decrypted); // Outputs: "Sensitive data to protect"
```

### Key Rotation (Recryption)

When you need to update your encryption key without exposing the plaintext:

```csharp
// Original encryption service
var oldKey = "Old encryption key";
var oldService = new CryptungService(oldKey);
string encryptedData = oldService.Encrypt("Sensitive data");

// New encryption service with updated key
var newKey = "New encryption key";
var newService = new CryptungService(newKey);

// Recrypt the data (decrypts with old key and encrypts with new key internally)
string recryptedData = newService.Recrypt(encryptedData, oldKey);

// Now you can decrypt with the new key
string decrypted = newService.Decrypt(recryptedData);
```

## Security Notes

1. **Key Management**: The security of your encrypted data depends on the security of your encryption key. Store it securely, preferably in a dedicated secrets management system.

2. **Key Strength**: Use strong, random passwords for your encryption keys. The example keys in this documentation are for demonstration only.

3. **Initialization Vectors**: Cryptung automatically generates a new IV for each encryption operation, ensuring that identical plaintexts produce different ciphertexts.

4. **Algorithm**: Cryptung uses AES-256 in CBC mode with PKCS7 padding. The key is derived using SHA-256 hashing of the input password.

## Dependency Injection

Cryptung includes a dependency injection package for easy integration with ASP.NET Core:

```csharp
// In Startup.cs or Program.cs
services.AddCryptung("Your encryption key here");

// In your service
public class MyService
{
    private readonly ICryptungService _cryptungService;
    
    public MyService(ICryptungService cryptungService)
    {
        _cryptungService = cryptungService;
    }
    
    public void ProcessData(string sensitiveData)
    {
        string encrypted = _cryptungService.Encrypt(sensitiveData);
        // ...
    }
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
