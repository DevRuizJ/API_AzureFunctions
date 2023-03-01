using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entity
{
    public class AcquiescenceEntity
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "document_type")]
        public string DocumentType { get; set; }

        [JsonProperty(propertyName: "document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty(propertyName: "customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty(propertyName: "customer_lastname")]
        public string CustomerLastname { get; set; }

        [JsonProperty(propertyName: "acquiescence_date")]
        public DateTime AcquiescenceDate { get; set; } 

        [JsonProperty(propertyName: "authorization")]
        public bool Authorization { get; set; }   

        [JsonProperty(propertyName: "acquiescence_validity")]
        public DateTime AcquiescenceValidity { get; set; } 

        [JsonProperty(propertyName: "acquiescence_version")]
        public string AcquiescenceVersion { get; set; }

        [JsonProperty(propertyName: "source_app")]
        public string SourceApp { get; set; }

        [JsonProperty(propertyName: "app_ip")]
        public string AppIP { get; set; }

        [JsonProperty(propertyName: "system_user")]
        public string SystemUser { get; set; }

        [JsonProperty(propertyName: "process_date")]
        public DateTime ProcessDate { get; set; } 
    }
}
