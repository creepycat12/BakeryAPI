using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public enum AddressTypeEnum
{
    Delivery = 1,
    Distribution = 2,
    Invoice = 3
}

public class AddressPostViewModel
{
    public string AddressLine { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AddressTypeEnum AddressType { get; set; }
}
