using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Internal.Resources.Models;
using Microsoft.Azure.Commands.DevSpaces.Properties;

namespace Microsoft.Azure.Commands.DevSpaces.Utils
{
    public static class GenericResourceExtension
    {
        public static bool IsDevSpacesSupported(this GenericResource genericResource, out string reason)
        {
            reason = string.Empty;
            dynamic properties = genericResource?.Properties;
            dynamic httpApplicationRouting = properties?.addonProfiles?.httpApplicationRouting;
            if (httpApplicationRouting?.enabled != true)
            {
                reason = Resources.HttpRoutingNotEnabled;
                return false;
            }

            return true;
        }
    }
}
