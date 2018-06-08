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

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    [Cmdlet(VerbsCommon.Get, DevSpacesControllerNoun, DefaultParameterSetName = ListDevSpacesControllerParameterSet)]
    [OutputType(typeof(PSController), typeof(IList<PSController>))]
    public class GetAzureRmDevSpacesController : DevSpacesCmdletBase
    {
        [Parameter(
            Position = 0,
            Mandatory = false,
            ParameterSetName = ListDevSpacesControllerParameterSet,
            HelpMessage = "Resource group name")]
        [Parameter(
            Position = 0,
            Mandatory = true,
            ParameterSetName = DevSpacesControllerNameParameterSet,
            HelpMessage = "Resource group name")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = DevSpacesControllerNameParameterSet,
            HelpMessage = "DevSpaces controller name.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The DevSpaces resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            ParameterSetName = InputObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public PSController InputObject { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            RunCmdLet(() => {
                switch (ParameterSetName)
                {
                    case ListDevSpacesControllerParameterSet:
                        ListDevSpacesController();
                        break;

                    case DevSpacesControllerNameParameterSet:
                        ShowDevSpacesController();
                        break;

                    case ResourceIdParameterSet:
                        string resourceGroup, name;
                        if (!ConversionUtils.TryParseResourceId(ResourceId, ConversionUtils.DevSpacesControllerResourceTypeName,  out resourceGroup, out name))
                        {
                            WriteError(new ErrorRecord(new PSArgumentException(Resources.InvalidDevSpacesControllerResourceIdErrorMessage, "ResourceId"), string.Empty, ErrorCategory.InvalidArgument, null));
                        }

                        ResourceGroupName = resourceGroup;
                        Name = name;
                        ShowDevSpacesController();
                        break;

                    case InputObjectParameterSet:
                        if(string.IsNullOrEmpty(InputObject.ResourceGroupName))
                        {
                            WriteError(new ErrorRecord(new PSArgumentException(Resources.InvalidDevSpacesControllerResourceGroupNameErrorMessage, "ResourceId"), string.Empty, ErrorCategory.InvalidArgument, null));
                        }

                        if (string.IsNullOrEmpty(InputObject.Name))
                        {
                            WriteError(new ErrorRecord(new PSArgumentException(Resources.InvalidDevSpacesControllerNameErrorMessage, "ResourceId"), string.Empty, ErrorCategory.InvalidArgument, null));
                        }

                        ResourceGroupName = InputObject.ResourceGroupName;
                        Name = InputObject.Name;
                        ShowDevSpacesController();
                        break;

                    default:
                        throw new ArgumentException(Resources.ParameterSetError);
                }
            });
        }

        private void ListDevSpacesController()
        {
            var controllers = Client.Controllers.ListAllPSController(ResourceGroupName);
            WriteObject(controllers, true);
        }

        private void ShowDevSpacesController()
        {
            var controller = Client.Controllers.GetPSController(ResourceGroupName, Name);
            WriteObject(controller);
        }
    }
}
