namespace Cryptung;

public interface ICryptungService
{
    string Encrypt(string input);
    string Decrypt(string input);
    string Recrypt(string input, string oldKey);
}
