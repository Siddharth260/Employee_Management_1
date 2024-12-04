using Newtonsoft.Json;

namespace EmployeeManagement_1.Common
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string UId { get; set; }
        public string dType { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public bool Archived { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }

        public void initialize (bool isNew, string documentType, string createdBy, string createdByName)
        {
            dType = documentType;
            Id = Guid.NewGuid().ToString();
            Active = true;
            Archived = false;

            if (isNew)
            {
                UId = Id;
                CreatedBy = createdBy;
                CreatedByName = createdByName;
                CreatedOn = DateTime.Now;
                Version = 1;
                UpdatedBy = createdBy;
                UpdatedByName = createdByName;
                UpdatedOn = CreatedOn;
            }
            else
            {
                Version++;
                UpdatedBy = createdBy;
                UpdatedByName = createdByName;
                UpdatedOn = DateTime.Now;
            }
        }
    }
}