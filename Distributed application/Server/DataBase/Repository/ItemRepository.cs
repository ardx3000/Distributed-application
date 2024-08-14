using System.Linq;
using System.Text.RegularExpressions;
using Server.DataBase.Entity;

namespace Server.DataBase.Repository
{
    public class ItemRepository : BaseRepository<Items>, IItemRepository
    {
        public ItemRepository(ApplicationContext context) : base(context)
        {
        }

        public Items GetByNormalizedProductName(string normalizedProductName)
        {
            // Fetch all items into memory
            var items = _context.Items.ToList();

            // Apply normalization in memory
            return items.FirstOrDefault(item => NormalizeProductName(item.Name) == normalizedProductName);
        }
        public void Update(Items item)
        {
            _context.Set<Items>().Update(item);
        }

        private string NormalizeProductName(string name)
        {
            return Regex.Replace(name.ToLower().Trim(), @"\s+", "");
        }
    }
}
