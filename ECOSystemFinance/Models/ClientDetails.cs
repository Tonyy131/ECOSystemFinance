using Newtonsoft.Json;
using System;

public class ClientDetails
{
    //public ClientDetails()
    //{
    //    this.ClientId = -1;
    //    this.GenderName = "";
    //    this.ClientName = "";
    //    this.UserName = "";
    //    this.MobileNumber = "";
    //    this.AddressName = "";
    //    this.CityName = "";
    //    this.StreetName = "";
    //    this.Building = "";
    //    this.FloorNumber = "";
    //    this.Apartement = "";
    //}
    [JsonProperty("ClientId")]
    public int ClientId { get; set; }
    [JsonProperty("GenderName")]
    public string GenderName { get; set; }

    [JsonProperty("ClientName")]
    public string ClientName { get; set; }

    [JsonProperty("UserName")]
    public string UserName { get; set; }
    // public string ClientPassword { get; set; }
    [JsonProperty("MobileNumber")]

    public string MobileNumber { get; set; }
    //public string TokenNumber { get; set; }
    //public Nullable<System.DateTime> TokenNumberCreationTime { get; set; }
    [JsonProperty("AddressName")]

    public string AddressName { get; set; }
    [JsonProperty("StreetName")]

    public string StreetName { get; set; }
    [JsonProperty("Building")]

    public string Building { get; set; }
    [JsonProperty("FloorNumber")]

    public string FloorNumber { get; set; }
    [JsonProperty("Apartement")]

    public string Apartement { get; set; }
    [JsonProperty("CityName")]

    public string CityName { get; set; }


}
