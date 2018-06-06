using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.DevSpaces.Models;
using Microsoft.Azure.Commands.DevSpaces.Utils;

namespace Microsoft.Azure.Commands.DevSpaces.Models
{
    public class PSController : PSControllerBase
    {
        public string TargetResourceGroupName { get; set; }

        public string TargetClusterName { get; set; }

        public PSController(Controller controller) : base(controller)
        {
            string targetResourceGroupName, targetClusterName;

            if(!ConversionUtils.TryParseResourceId(controller.TargetContainerHostResourceId, ConversionUtils.ManagedClusterResourceTypeName, out targetResourceGroupName, out targetClusterName))
            {
                TargetResourceGroupName = targetResourceGroupName;
                TargetClusterName = targetClusterName;
            }
        }

        //public Controller ToController()
        //{

        //}
    }
}
