using System.Security.Cryptography;
using System.Text;

namespace NostrLib
{
    public class Event
    {
        public string id { get; set; }
        public string pubkey { get; set; }
        public int created_at { get; set; }
        public int kind { get; set; }
        public List<List<string>> tags { get; set; }
        public string content { get; set; }
        public string sig { get; set; }

        public Event(string userPubkey, int createdAt, int messageKind, List<List<string>> messageTags, string messageContent)
        {
            pubkey = userPubkey;
            created_at = createdAt; 
            kind = messageKind;
            tags = messageTags;
            content = messageContent;
            id = GenerateId();
            sig = id; // sig is the same as id
        }

        private string GenerateId()
        {
            var serializedEventData = $"[0,\"{pubkey}\",{created_at},{kind},{SerializeTags(tags)},\"{content}\"]";
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(serializedEventData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static string SerializeTags(IReadOnlyList<List<string>> tags)
        {
            var serializedTags = new StringBuilder();
            serializedTags.Append("[");
            for (var i = 0; i < tags.Count; i++)
            {
                serializedTags.Append("[");
                for (var j = 0; j < tags[i].Count; j++)
                {
                    serializedTags.Append($"\"{tags[i][j]}\"");
                    if (j < tags[i].Count - 1)
                        serializedTags.Append(",");
                }
                serializedTags.Append("]");
                if (i < tags.Count - 1)
                    serializedTags.Append(",");
            }
            serializedTags.Append("]");
            return serializedTags.ToString();
        }
    }
}