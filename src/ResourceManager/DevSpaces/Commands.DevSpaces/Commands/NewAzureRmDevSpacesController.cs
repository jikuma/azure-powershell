using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.DevSpaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Commands.DevSpaces.Properties;
using Microsoft.Azure.Management.DevSpaces;
using Microsoft.Azure.Commands.DevSpaces.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Commands.DevSpaces.Utils;
using Microsoft.Azure.Commands.Aks.Generated;
using Microsoft.Azure.Commands.Aks.Generated.Models;
//using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.Internal.Resources.Models;
using System.Collections;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    [Cmdlet(VerbsCommon.New, DevSpacesControllerNoun, SupportsShouldProcess = true)]
    [OutputType(typeof(PSController))]
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

        [Parameter(Mandatory = false,
            HelpMessage = "A hash table which represents resource tags.")]
        public Hashtable Tag { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            var msg = $"{Name} in {ResourceGroupName}";
            if (ShouldProcess(msg, Resources.CreatingADevSpacesController))
            {
                RunCmdLet(NewDevSpacesControllerAction);
            }
        }

        private void NewDevSpacesControllerAction()
        {
            string devSpacesNotSupportedReason = String.Empty;

            WriteVerbose(string.Format(Resources.FetchCluster, TargetClusterName, TargetResourceGroupName));
            ManagedCluster cluster = ContainerClient.ManagedClusters.Get(TargetResourceGroupName, TargetClusterName);
            if (!cluster.IsDevSpacesSupported(out devSpacesNotSupportedReason))
            {
                throw new Exception(devSpacesNotSupportedReason);
            }

            WriteVerbose(string.Format(Resources.FetchClusterAccessProfile, TargetClusterName, TargetResourceGroupName));
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
            createControllerParam.Tags = TagsConversionHelper.CreateTagDictionary(Tag, true);
            WriteVerbose(string.Format(Resources.CreatingDevSpaces, Name, ResourceGroupName));
            Controller controller = Client.Controllers.Create(ResourceGroupName, Name, createControllerParam);
            WriteObject(new PSController(controller));
        }
    }
}
