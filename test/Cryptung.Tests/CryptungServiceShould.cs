using Cryptung.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptung.Tests;

public class CryptungServiceShould
{
    [Fact]
    public void EncryptString()
    {
        var input =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
            "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non " +
            "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        var encryptionKey = "The quick brown fox jumps over the lazy dog";
        var cryptungService = new CryptungService(encryptionKey);

        var encryptedInput = cryptungService.Encrypt(input);

        Assert.NotNull(encryptedInput);
        Assert.NotEmpty(encryptedInput);
        Assert.NotEqual(input, encryptedInput);
    }
    
    [Fact]
    public void DecryptEncryptedObject()
    {
        var input =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
            "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non " +
            "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        var encryptionKey = "The quick brown fox jumps over the lazy dog";
        var cryptungService = new CryptungService(encryptionKey);
        var encryptedInput = cryptungService.Encrypt(input);
        var cryptungService2 = new CryptungService(encryptionKey);

        var decryptedInput = cryptungService2.Decrypt(encryptedInput);

        Assert.Equal(input, decryptedInput);
    }
    
    [Fact]
    public void HaveDifferentOutputForSameValueTwice()
    {
        var input =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
            "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non " +
            "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        var encryptionKey = "The quick brown fox jumps over the lazy dog.";
        var encryptionService = new CryptungService(encryptionKey);

        var encryptedInput1 = encryptionService.Encrypt(input);
        var encryptedInput2 = encryptionService.Encrypt(input);

        Assert.NotEqual(encryptedInput1, encryptedInput2);
    }
    
    [Fact]
    public void RecryptEncryptedString()
    {
        var input =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure " +
            "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non " +
            "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        var oldEncryptionKey = "The quick brown fox jumps over the lazy dog";
        var oldCryptungService = new CryptungService(oldEncryptionKey);
        var encryptedInput = oldCryptungService.Encrypt(input);

        var newEncryptionKey = "Waltz, bad nymph, for quick jigs vex";
        var newCryptungService = new CryptungService(newEncryptionKey);

        var newEncryptedInput = newCryptungService.Recrypt(encryptedInput, oldEncryptionKey);
        var decryptedInput = newCryptungService.Decrypt(newEncryptedInput);

        Assert.Equal(input, decryptedInput);
    }
}
