using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.DevSpaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Commands.DevSpaces.Properties;
using Microsoft.Azure.Management.DevSpaces.Generated;
using Microsoft.Azure.Commands.DevSpaces.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Commands.DevSpaces.Utils;
using Microsoft.Azure.Commands.Aks.Generated;
using Microsoft.Azure.Commands.Aks.Generated.Models;
//using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.Internal.Resources.Models;

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    [Cmdlet(VerbsCommon.New, DevSpacesControllerNoun)]
    public class NewAzureRmDevSpacesController : DevSpacesCmdletBase
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "Resource Group Name.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            HelpMessage = "DevSpaces Controller Name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Position = 2,
            Mandatory = true,
            HelpMessage = "Target Resource Group Name.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string TargetResourceGroupName { get; set; }

        [Parameter(
            Position = 3,
            Mandatory = true,
            HelpMessage = "Target Cluster Name.")]
        [ValidateNotNullOrEmpty]
        public string TargetClusterName { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            RunCmdLet(() =>
            {
                string devSpacesNotSupportedReason = String.Empty;
                ManagedCluster cluster = ContainerClient.ManagedClusters.Get(TargetResourceGroupName, TargetClusterName);
                if(!cluster.IsDevSpacesSupported(out devSpacesNotSupportedReason))
                {
                    throw new Exception(devSpacesNotSupportedReason);
                }

                ManagedClusterAccessProfile accessProfile = ContainerClient.ManagedClusters.GetAccessProfiles(TargetResourceGroupName, TargetClusterName, "clusterUser");
                if (accessProfile == null || string.IsNullOrEmpty(accessProfile.KubeConfig))
                {
                    throw new Exception(String.Format(Resources.CanNotFetchKubeConfig, TargetClusterName));
                }

                GenericResource resource = RmClient.Resources.Get(TargetResourceGroupName, "Microsoft.ContainerService", "", "managedClusters", TargetClusterName, "2018-03-31");
                if (!resource.IsDevSpacesSupported(out devSpacesNotSupportedReason))
                {
                    throw new Exception(devSpacesNotSupportedReason);
                }

                Controller createControllerParam = cluster.GetNewDevSpaceControllerParam(accessProfile, resource.Properties);
                Client.Controllers.BeginCreate(ResourceGroupName, Name, createControllerParam);
            });
        }
    }
}
