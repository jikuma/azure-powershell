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

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    [Cmdlet(VerbsCommon.Get, "AzureRmDevSpaces", DefaultParameterSetName = ResourceGroupParameterSet)]
    [OutputType(typeof(PSController))]
    public class GetAzureRmDevSpaces : DevSpacesCmdletBase
    {
        private const string ResourceGroupParameterSet = "ResourceGroupParameterSet";

        [Parameter(
            Position = 0,
            Mandatory = false,
            ParameterSetName = ResourceGroupParameterSet,
            HelpMessage = "Resource group name")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            RunCmdLet(() => {
                switch (ParameterSetName)
                {
                    case ResourceGroupParameterSet:
                        var controllers = Client.Controllers.List();
                        List<PSController> list = new List<PSController>();
                        foreach (Controller controller in controllers)
                        {
                            list.Add(new PSController(controller));
                        }

                        WriteObject(list, true);
                        break;
                    default:
                        throw new ArgumentException(Resources.ParameterSetError);
                }
            });
        }

    }
}
