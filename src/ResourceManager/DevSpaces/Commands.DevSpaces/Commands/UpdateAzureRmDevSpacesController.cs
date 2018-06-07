using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Graph.RBAC.Version1_6;
using Microsoft.Azure.Management.Authorization.Version2015_07_01;
using Microsoft.Azure.Management.DevSpaces.Generated;
using Microsoft.Azure.Management.Internal.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;
using CloudException = Microsoft.Rest.Azure.CloudException;
using System.Management.Automation;
using System.IO;
namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    public class UpdateAzureRmDevSpacesController : DevSpacesCmdletBase
    {

    }
}
