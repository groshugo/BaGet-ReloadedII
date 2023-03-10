using System.Collections.Generic;
using System.Text.Json.Serialization;
using BaGet.Protocol.Internal;

namespace BaGet.Protocol.Models
{
    /// <summary>
    /// Represents a package dependency.
    ///
    /// See https://docs.microsoft.com/en-us/nuget/api/registration-base-url-resource#package-dependency
    /// </summary>
    public class LocationItem
    {
        /// <summary>
        /// The ID of the package dependency.
        /// </summary>
        [JsonPropertyName("country")]
        public string Id { get; set; }

        /// <summary>
        /// The allowed version range of the dependency.
        /// </summary>
        [JsonPropertyName("citys")]
        public List<string> Citys { get; set; }
    }
}
