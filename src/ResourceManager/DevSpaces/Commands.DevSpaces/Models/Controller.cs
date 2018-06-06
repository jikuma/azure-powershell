// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DevSpaces.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [Rest.Serialization.JsonTransformation]
    public partial class Controller : TrackedResource
    {
        /// <summary>
        /// Initializes a new instance of the Controller class.
        /// </summary>
        public Controller()
        {
            Sku = new Sku();
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Controller class.
        /// </summary>
        /// <param name="hostSuffix">DNS suffix for public endpoints running in
        /// the Azure Dev Spaces Controller.</param>
        /// <param name="targetContainerHostResourceId">Resource ID of the
        /// target container host</param>
        /// <param name="targetContainerHostCredentialsBase64">Credentials of
        /// the target container host (base64).</param>
        /// <param name="id">Fully qualified resource Id for the
        /// resource.</param>
        /// <param name="name">The name of the resource.</param>
        /// <param name="type">The type of the resource.</param>
        /// <param name="tags">Tags for the Azure resource.</param>
        /// <param name="location">Region where the Azure resource is
        /// located.</param>
        /// <param name="provisioningState">Provisioning state of the Azure Dev
        /// Spaces Controller. Possible values include: 'Succeeded', 'Failed',
        /// 'Canceled', 'Updating', 'Creating', 'Deleting'</param>
        /// <param name="dataPlaneFqdn">DNS name for accessing DataPlane
        /// services</param>
        public Controller(string hostSuffix, string targetContainerHostResourceId, string targetContainerHostCredentialsBase64, Sku sku, string id = default(string), string name = default(string), string type = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), string location = default(string), string provisioningState = default(string), string dataPlaneFqdn = default(string))
            : base(id, name, type, tags, location)
        {
            ProvisioningState = provisioningState;
            HostSuffix = hostSuffix;
            DataPlaneFqdn = dataPlaneFqdn;
            TargetContainerHostResourceId = targetContainerHostResourceId;
            TargetContainerHostCredentialsBase64 = targetContainerHostCredentialsBase64;
            Sku = sku;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets provisioning state of the Azure Dev Spaces Controller.
        /// Possible values include: 'Succeeded', 'Failed', 'Canceled',
        /// 'Updating', 'Creating', 'Deleting'
        /// </summary>
        [JsonProperty(PropertyName = "properties.provisioningState")]
        public string ProvisioningState { get; private set; }

        /// <summary>
        /// Gets or sets DNS suffix for public endpoints running in the Azure
        /// Dev Spaces Controller.
        /// </summary>
        [JsonProperty(PropertyName = "properties.hostSuffix")]
        public string HostSuffix { get; set; }

        /// <summary>
        /// Gets DNS name for accessing DataPlane services
        /// </summary>
        [JsonProperty(PropertyName = "properties.dataPlaneFqdn")]
        public string DataPlaneFqdn { get; private set; }

        /// <summary>
        /// Gets or sets resource ID of the target container host
        /// </summary>
        [JsonProperty(PropertyName = "properties.targetContainerHostResourceId")]
        public string TargetContainerHostResourceId { get; set; }

        /// <summary>
        /// Gets or sets credentials of the target container host (base64).
        /// </summary>
        [JsonProperty(PropertyName = "properties.targetContainerHostCredentialsBase64")]
        public string TargetContainerHostCredentialsBase64 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public Sku Sku { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (HostSuffix == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "HostSuffix");
            }
            if (TargetContainerHostResourceId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TargetContainerHostResourceId");
            }
            if (TargetContainerHostCredentialsBase64 == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TargetContainerHostCredentialsBase64");
            }
            if (Sku == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Sku");
            }
        }
    }
}
