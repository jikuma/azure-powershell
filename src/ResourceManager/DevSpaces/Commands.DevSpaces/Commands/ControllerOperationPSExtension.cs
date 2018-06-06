﻿using Microsoft.Azure.Commands.DevSpaces.Models;
using Microsoft.Azure.Management.DevSpaces.Generated;
using Microsoft.Azure.Management.DevSpaces.Models;
//using Microsoft.Azure.Management.DevSpaces.Generated;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.DevSpaces.Commands
{
    public static class ControllerOperationPSExtension
    {
        private static IPage<Controller> List(this IControllersOperations operations, string resourceGroupName)
        {
            if(string.IsNullOrEmpty(resourceGroupName))
            {
                return operations.List();
            }

            return operations.ListByResourceGroup(resourceGroupName);
        }

        private static IPage<Controller> ListByNextLink(this IControllersOperations operations, string nextLink, string resourceGroupName)
        {
            if (string.IsNullOrEmpty(resourceGroupName))
            {
                return operations.ListNext(nextLink);
            }

            return operations.ListByResourceGroupNext(nextLink);
        }


        public static IList<PSControllerBase> ListAllPSController(this IControllersOperations operations, string resourceGroupName)
        {
            List<PSControllerBase> list = new List<PSControllerBase>();
            var controllers = operations.List(resourceGroupName);

            foreach (Controller controller in controllers)
            {
                list.Add(new PSControllerBase(controller));
            }

            while (!string.IsNullOrEmpty(controllers.NextPageLink))
            {
                controllers = operations.ListByNextLink(resourceGroupName, controllers.NextPageLink);

                foreach (Controller controller in controllers)
                {
                    list.Add(new PSControllerBase(controller));
                }
            }

            return list;
        }

        public static PSControllerBase GetPSController(this IControllersOperations operations, string resourceGroupName, string name)
        {
            var controller = operations.Get(resourceGroupName, name);

            return new PSControllerBase(controller);
        }
    }
}
