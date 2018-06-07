using Microsoft.Azure.Management.DevSpaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Commands.Aks.Generated.Models;
using Microsoft.Azure.Commands.DevSpaces.Commands;
using Microsoft.Azure.Commands.DevSpaces.Properties;

namespace Microsoft.Azure.Commands.DevSpaces.Utils
{
    public static class ManagedClusterExtension
    {
        public static Controller GetNewDevSpaceControllerParam(this ManagedCluster managedCluster, ManagedClusterAccessProfile accessProfile, dynamic armProperties)
        {
            var clusterLocation =  managedCluster.Location;
            dynamic httpApplicationRouting = armProperties?.addonProfiles?.httpApplicationRouting;
            var clusterDnsZone = httpApplicationRouting?.config?.HTTPApplicationRoutingZoneName;

            var createParameters = new Controller()
            {
                Location = clusterLocation,
                Sku = new Azure.Management.DevSpaces.Models.Sku(),
                Tags = null,
                HostSuffix = clusterDnsZone,
                TargetContainerHostResourceId = managedCluster.Id,
                TargetContainerHostCredentialsBase64 = accessProfile.KubeConfig
            };

            return createParameters;
        } 

        public static bool IsDevSpacesSupported(this ManagedCluster managedCluster,out string reason)
        {
            reason = string.Empty;
            if (new Version(managedCluster.KubernetesVersion) < new Version(DevSpacesConstants.MinimumKubernetesVersion))
            {
                reason = string.Format(Resources.NotSupportedTargetClusterVersion, managedCluster.Name, managedCluster.KubernetesVersion, DevSpacesConstants.MinimumKubernetesVersion);
                return false;
            }

            return true;
        }
    }
}
