using Newtonsoft.Json;
using System;

public class Log
{
    [JsonProperty("ClientId")]
    public int Id { get; set; }

    public string TokenNumber { get; set; }

    [JsonProperty("TokenNumberCreationTime")]
    public DateTime TokenTime { get; set; }
}
