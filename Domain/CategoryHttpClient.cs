using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class CategoryHttpClient : ICategoryHttpClient
    {
        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryEntity>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PostCategoryAsync(CategoryEntity category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutCategoryAsync(int categoryId, CategoryEntity category)
        {
            throw new NotImplementedException();
        }
    }
}
