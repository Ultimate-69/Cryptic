using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Welcome to CRYPTIC!");
GiveOptions();
Console.ReadKey();

void GiveOptions()
{
    Console.WriteLine("1. Encrypt");
    Console.WriteLine("2. Decrypt");
    Console.WriteLine("3. Quit");
    HandleOptionInput();
}

void HandleOptionInput()
{
    int choice = Console.ReadKey().KeyChar - '0';
    Console.WriteLine();
    switch (choice)
    {
        case 1:
            Console.WriteLine("Generating key..");
            byte[] keyBytes = new byte[32];
            RandomNumberGenerator.Fill(keyBytes);
            BigInteger fullKey = new BigInteger(keyBytes);
            
            // Create a shorter representation of the key
            string shortKey = ConvertToShorterKey(fullKey);
            
            Console.WriteLine("Done! Your Key is: ");
            Console.WriteLine(shortKey);
            Console.WriteLine("Without the key, you CANNOT decrypt your text. Do NOT lose this key.");
            Console.WriteLine("Please input the text you want to encrypt: ");
            String? textToEncrypt = Console.ReadLine();
            String encryptedText = Encrypt(textToEncrypt, fullKey);
            Console.WriteLine("Encrypted Text: " + encryptedText);
           
            GiveOptions();
            break;
        case 2:
            Console.WriteLine("Please input the text you want to decrypt: ");
            String? textToDecrypt = Console.ReadLine();
            Console.WriteLine("Please input the key to decrypt the text: ");
            String? shortKeyInput = Console.ReadLine();
            
            if (shortKeyInput == null)
            {
                Console.WriteLine("Invalid key.");
                Environment.Exit(0);
            }
            BigInteger keyInt = ConvertFromShorterKey(shortKeyInput);
            
            String decryptedText = Decrypt(textToDecrypt, keyInt);
            Console.WriteLine("Decrypted Text: ");
            Console.WriteLine(decryptedText);
            GiveOptions();
            break;
        case 3:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid option. Please choose from the above only.");
            GiveOptions();
            break;
    }
}

string ConvertToShorterKey(BigInteger key)
{
    // Convert to Base64 for compact representation
    byte[] bytes = key.ToByteArray();
    return Convert.ToBase64String(bytes);
}

BigInteger ConvertFromShorterKey(string shorterKey)
{
    byte[] bytes = Convert.FromBase64String(shorterKey);
    return new BigInteger(bytes);
}

String Encrypt(string? text, BigInteger key)
{
    if (text == null) return "ERROR";

    byte[] textBytes = Encoding.UTF8.GetBytes(text);
    byte[] keyBytes = key.ToByteArray();

    byte[] encryptedBytes = new byte[textBytes.Length];
    for (int i = 0; i < textBytes.Length; i++)
    {
        encryptedBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
    }

    return Convert.ToBase64String(encryptedBytes);
}

String Decrypt(string? text, BigInteger key)
{
    if (text == null) return "ERROR";

    byte[] encryptedBytes = Convert.FromBase64String(text);
    byte[] keyBytes = key.ToByteArray();
    
    byte[] decryptedBytes = new byte[encryptedBytes.Length];
    for (int i = 0; i < encryptedBytes.Length; i++)
    {
        decryptedBytes[i] = (byte)(encryptedBytes[i] ^ keyBytes[i % keyBytes.Length]);
    }

    return Encoding.UTF8.GetString(decryptedBytes);
}