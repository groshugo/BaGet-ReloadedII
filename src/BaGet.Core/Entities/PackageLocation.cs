using System.Collections.Generic;

namespace BaGet.Core.Entities
{
    // Add a new model class to represent the additional package metadata.
    public class PackageLocation
    {
        public int Key { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<string> Cities { get; set; }

        public Package Package { get; set; }
    }
}
