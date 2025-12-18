using Azure;
using Azure.Data.Tables;
using System;

namespace MusicSchool.Models
{
    public class GalleryEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "gallery";
        public string RowKey { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
