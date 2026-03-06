//-----------------------------------------------------------------------
// <copyright file="PassportDecrypter.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;
using Telegram.Bot;

namespace TacosPortal.Interfaces;
#pragma warning disable CA2208, MA0015
#pragma warning disable CA1850, CA1835


public interface IDecryptedValue { }


public class PassportDataDecryptionException(string message) : Exception(message) { }


public class Decrypter
{
    private static byte[] DecryptDataBytes(byte[] data, byte[] secret, byte[] hash)
    {
        FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

        byte[] dataWithPadding;
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = dataKey;
            aes.IV = dataIv;
            aes.Padding = PaddingMode.None;
            using var decrypter = aes.CreateDecryptor();
            dataWithPadding = decrypter.TransformFinalBlock(data, 0, data.Length);
        }

        byte[] paddedDataHash;
        using (var sha256 = SHA256.Create())
            paddedDataHash = sha256.ComputeHash(dataWithPadding);

        for (int i = 0; i < hash.Length; i++)
            if (hash[i] != paddedDataHash[i])
                throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");

        byte paddingLength = dataWithPadding[0];
        if (paddingLength < 32) throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

        int actualDataLength = dataWithPadding.Length - paddingLength;
        if (actualDataLength < 1) throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

        var decryptedData = new byte[actualDataLength];
        Array.Copy(dataWithPadding, paddingLength, decryptedData, 0, actualDataLength);
        return decryptedData;
    }
    private static async Task DecryptDataStreamAsync(Stream data, byte[] secret, byte[] hash, Stream destination, CancellationToken cancellationToken)
    {
        FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Key = dataKey;
        aes.IV = dataIv;
        aes.Padding = PaddingMode.None;

        using var decrypter = aes.CreateDecryptor();
        using CryptoStream aesStream = new(data, decrypter, CryptoStreamMode.Read);
        using var sha256 = SHA256.Create();
        using CryptoStream shaStream = new(aesStream, sha256, CryptoStreamMode.Read);
        byte[] paddingBuffer = new byte[256];
        int read = await shaStream.ReadAsync(paddingBuffer, 0, 256, cancellationToken).ConfigureAwait(false);

        byte paddingLength = paddingBuffer[0];
        if (paddingLength < 32) throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

        int actualDataLength = read - paddingLength;
        if (actualDataLength < 1) throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

        await destination.WriteAsync(paddingBuffer, paddingLength, actualDataLength, cancellationToken).ConfigureAwait(false);



        const int defaultBufferSize = 81920;
        await shaStream.CopyToAsync(destination, defaultBufferSize, cancellationToken).ConfigureAwait(false);

        byte[] paddedDataHash = sha256.Hash!;
        for (int i = 0; i < hash.Length; i++)
            if (hash[i] != paddedDataHash[i])
                throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");
    }

    private static void FindDataKeyAndIv(byte[] secret, byte[] hash, out byte[] dataKey, out byte[] dataIv)
    {
        byte[] dataSecretHash;
        using (var sha512 = SHA512.Create())
        {
            byte[] secretAndHashBytes = new byte[secret.Length + hash.Length];
            Array.Copy(secret, 0, secretAndHashBytes, 0, secret.Length);
            Array.Copy(hash, 0, secretAndHashBytes, secret.Length, hash.Length);
            dataSecretHash = sha512.ComputeHash(secretAndHashBytes);
        }

        dataKey = new byte[32];
        Array.Copy(dataSecretHash, 0, dataKey, 0, 32);

        dataIv = new byte[16];
        Array.Copy(dataSecretHash, 32, dataIv, 0, 16);
    }

    public TelegramCredentials? DecryptCredentials(TelegramEncryptedCredentials encryptedCredentials, RSA key)
    {
        ArgumentNullException.ThrowIfNull(encryptedCredentials);
        ArgumentNullException.ThrowIfNull(key);
        if (encryptedCredentials.Data is null) throw new ArgumentNullException(nameof(encryptedCredentials.Data));
        if (encryptedCredentials.Secret is null) throw new ArgumentNullException(nameof(encryptedCredentials.Secret));
        if (encryptedCredentials.Hash is null) throw new ArgumentNullException(nameof(encryptedCredentials.Hash));

        byte[] data = Convert.FromBase64String(encryptedCredentials.Data);
        if (data.Length == 0) throw new ArgumentException("Data is empty.", nameof(encryptedCredentials.Data));
        if (data.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {data.Length}.");

        byte[] encryptedSecret = Convert.FromBase64String(encryptedCredentials.Secret);

        byte[] hash = Convert.FromBase64String(encryptedCredentials.Hash);
        if (hash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {hash.Length}.");

        byte[] secret = key.Decrypt(encryptedSecret, RSAEncryptionPadding.OaepSHA1);

        byte[] decryptedData = DecryptDataBytes(data, secret, hash);
        string json = Encoding.UTF8.GetString(decryptedData);
        return JsonSerializer.Deserialize<TelegramCredentials>(json, JsonBotAPI.Options);
    }
    public TValue? DecryptData<TValue>(string encryptedData, TelegramDataCredentials dataCredentials) where TValue : class, IDecryptedValue
    {
        ArgumentNullException.ThrowIfNull(encryptedData);
        ArgumentNullException.ThrowIfNull(dataCredentials);
        if (dataCredentials.Secret is null) throw new ArgumentNullException(nameof(dataCredentials.Secret));
        if (dataCredentials.DataHash is null) throw new ArgumentNullException(nameof(dataCredentials.DataHash));

        byte[] data = Convert.FromBase64String(encryptedData);
        if (data.Length == 0) throw new ArgumentException("Data is empty.", nameof(encryptedData));
        if (data.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {data.Length}.");

        byte[] dataSecret = Convert.FromBase64String(dataCredentials.Secret);

        byte[] dataHash = Convert.FromBase64String(dataCredentials.DataHash);
        if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

        byte[] decryptedData = DecryptDataBytes(data, dataSecret, dataHash);
        string json = Encoding.UTF8.GetString(decryptedData);
        return JsonSerializer.Deserialize<TValue>(json, JsonBotAPI.Options);
    }
    public byte[] DecryptFile(
        byte[] encryptedContent,
        TelegramFileCredentials fileCredentials
    )
    {
        ArgumentNullException.ThrowIfNull(encryptedContent);
        ArgumentNullException.ThrowIfNull(fileCredentials);
        if (fileCredentials.Secret is null) throw new ArgumentNullException(nameof(fileCredentials.Secret));
        if (fileCredentials.FileHash is null) throw new ArgumentNullException(nameof(fileCredentials.FileHash));
        if (encryptedContent.Length == 0) throw new ArgumentException("Data array is empty.", nameof(encryptedContent));
        if (encryptedContent.Length % 16 != 0) throw new PassportDataDecryptionException
                ($"Data length is not divisible by 16: {encryptedContent.Length}.");

        byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
        byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);
        if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

        return DecryptDataBytes(encryptedContent, dataSecret, dataHash);
    }
    public Task DecryptFileAsync(Stream encryptedContent, TelegramFileCredentials fileCredentials, Stream destination, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(encryptedContent);
        ArgumentNullException.ThrowIfNull(fileCredentials);
        ArgumentNullException.ThrowIfNull(destination);
        if (fileCredentials.Secret is null) throw new ArgumentNullException(nameof(fileCredentials.Secret));
        if (fileCredentials.FileHash is null) throw new ArgumentNullException(nameof(fileCredentials.FileHash));
        if (!encryptedContent.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(encryptedContent));
        if (encryptedContent.CanSeek && encryptedContent.Length == 0) throw new ArgumentException("Stream is empty.", nameof(encryptedContent));
        if (encryptedContent.CanSeek && encryptedContent.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {encryptedContent.Length}.");
        if (!destination.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(destination));

        byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
        byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);
        if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

        return DecryptDataStreamAsync(encryptedContent, dataSecret, dataHash, destination, cancellationToken);
    }
}