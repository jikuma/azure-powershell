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

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    [Cmdlet(VerbsCommon.New, DevSpacesControllerNoun, DefaultParameterSetName = ListDevSpacesControllerParameterSet)]
    [OutputType(typeof(PSControllerBase))]
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
                
            });
        }
    }
}
