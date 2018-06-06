using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.DevSpaces.Utils
{
    public static class ConversionUtils
    {
        public const string DevSpacesControllerResourceTypeName = "Microsoft.DevSpaces/controllers";
        public const string ManagedClusterResourceTypeName = "Microsoft.Containerservice/managedclusters";

        public static bool TryParseResourceId(string resourceId, string type, out string resourceGroupName, out string name)
        {
            resourceGroupName = string.Empty;
            name = string.Empty;
            var parsed = false;

            if (!string.IsNullOrEmpty(resourceId))
            {
                var resourceIdentifier = new ResourceIdentifier(resourceId);
                resourceGroupName = resourceIdentifier.ResourceGroupName;

                if (string.Equals(resourceIdentifier.ResourceType, type, StringComparison.OrdinalIgnoreCase))
                {
                    name = resourceIdentifier.ResourceName;
                    parsed = true;
                }
            }

            return parsed;
        }
    }
}
