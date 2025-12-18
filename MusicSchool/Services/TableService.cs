using Azure.Data.Tables;
using MusicSchool.Models;

namespace MusicSchool.Services
{
    public class TableService
    {
        private readonly TableClient _table;

        public TableService(string connectionString)
        {
            _table = new TableClient(connectionString, "GallerydataMeta");
            _table.CreateIfNotExists();
        }

        public void Add(GalleryEntity entity)
        {
            _table.AddEntity(entity);
        }

        public IEnumerable<GalleryEntity> GetAll()
        {
            return _table.Query<GalleryEntity>(x => x.PartitionKey == "gallery" && x.IsActive);
        }

        public void Delete(string rowKey)
        {
            _table.DeleteEntity("gallery", rowKey);
        }
    }
}
