using System.Collections.Generic;
using System.Text.RegularExpressions;
using Server.DataBase.Entity;
using Server.DataBase.Repository;

namespace Server.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Items GetItem(int id)
        {
            return _unitOfWork.Items.Get(id);
        }

        public IEnumerable<Items> GetAllItems()
        {
            return _unitOfWork.Items.GetAll();
        }

        public void CreateItem(Items item)
        {
            AddOrUpdateItem(item);
        }

        public void UpdateItem(Items item)
        {
            AddOrUpdateItem(item);
        }

        public void DeleteItem(int id)
        {
            var item = _unitOfWork.Items.Get(id);
            if (item != null)
            {
                _unitOfWork.Items.Delete(item);
                _unitOfWork.Complete();
            }
        }

        // Unified method for adding or updating an item
        public void AddOrUpdateItem(Items item)
        {
            // Normalize the name for checking existence
            string normalizedProductName = NormalizeProductName(item.Name);
            var existingItem = _unitOfWork.Items.GetByNormalizedProductName(normalizedProductName);

            if (existingItem == null)
            {
                // Items does not exist, so create it
                item.TotalPrice = item.Quantity * item.PricePerUnit; // Optionally handled by DB
                _unitOfWork.Items.Add(item);
            }
            else
            {
                // Items exists, so update it
                existingItem.Quantity += item.Quantity; // Increase the quantity
                existingItem.PricePerUnit = item.PricePerUnit; // Optionally update the price
                //existingItem.TotalPrice = existingItem.Quantity * existingItem.PricePerUnit; // Optionally handled by DB

                _unitOfWork.Items.Update(existingItem);
            }
            _unitOfWork.Complete();
        }

        private string NormalizeProductName(string name)
        {
            return Regex.Replace(name.ToLower().Trim(), @"\s+", "");
        }
    }
}
