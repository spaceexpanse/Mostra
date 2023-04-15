using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace NostrLib.NostrPrimitives;

public class Event
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("pubkey")]
    public string? PublicKey { get; set; }

    [JsonPropertyName("created_at")] 
    public long CreatedAt { get; set; }

    [JsonPropertyName("kind")] 
    public int Kind { get; set; }

    [JsonPropertyName("tags")] 
    public List<List<string>?>? Tags { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
    
    [JsonPropertyName("sig")]
    public string? Signature { get; set; }

    /// <summary>
    /// Constructor for a new Event.
    /// </summary>
    /// <param name="publicKey">PubKey of the user signing the event</param>
    /// <param name="kind">NIP kind of the event</param>
    /// <param name="tags">Tags for the event</param>
    /// <param name="content">Content payload of the event</param>
    public Event(string privateKey, int kind, List<List<string>?>? tags,
        string content)
    {
        Kind = kind;
        Tags = tags;
        Content = content;
        
        // Get current Unix Timestamp
        DateTime now = DateTime.UtcNow;
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        CreatedAt = (long)(now - unixEpoch).TotalSeconds;
        Id = GenerateId();
        var ecPrivateKey = NBitcoin.Secp256k1.ECPrivKey.Create(Encoding.UTF8.GetBytes(privateKey).AsSpan()[..32]);
        var ecPublicKey = ecPrivateKey.CreatePubKey();
        Signature = GenerateSignature();
    }

    /// <summary>
    /// Generates the post ID for the current event
    /// </summary>
    /// <returns>A string containing the SHA256 ID of the current event</returns>
    public string GenerateId()
    {
        if (PublicKey == null || Content == null) return string.Empty;
        var bytesToBeHashed =
            Encoding.UTF8.GetBytes($"""[0,{PublicKey.ToLower()},{CreatedAt},{Kind},{Tags},{Content}]""");
        var hashValue = SHA256.HashData(bytesToBeHashed);
        var hashString = string.Concat(hashValue.Select(item => item.ToString("x2"))).ToLowerInvariant();
        return hashString;
    }

    public string GenerateSignature(NBitcoin.Secp256k1.ECPrivKey privateKey)
    {
        if (Id != null)
        {
            var buffer = privateKey.SignBIP340(Encoding.UTF8.GetBytes(Id).AsSpan()[..32]).ToBytes();
            return string.Concat(buffer.Select(item => item.ToString("x2"))).ToLowerInvariant();
        }
        return string.Empty;
    }
}