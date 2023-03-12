using System.Collections.Generic;

namespace BaGet.Core.Entities
{
    // Add a new model class to represent the additional package metadata.
    public class PackageLocation
    {
        public int Id { get; set; }

        public string Sites { get; set; }

        public Package Package { get; set; }
    }
}
