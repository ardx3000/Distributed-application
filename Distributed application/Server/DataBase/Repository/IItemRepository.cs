using Server.DataBase.Entity;

namespace Server.DataBase.Repository
{
    public interface IItemRepository : IBaseRepository<Items>
    {
        Items GetByNormalizedProductName(string normalizedProductName);
        void Update(Items item);
    }
}
