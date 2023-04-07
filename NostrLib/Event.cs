using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NostrLib;

public class Event
{
    public string id { get; set; }
    public string pubkey { get; set; }
    public Int128 created_at { get; set; }
    public Int32 kind { get; set; }
    public List<(string, string)> tags { get; set; }
    public string content { get; set; }
    public string sig { get; set; }
}