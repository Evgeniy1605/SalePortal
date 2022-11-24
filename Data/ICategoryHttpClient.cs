using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface ICategoryHttpClient
    {
        public Task<CategoryEntity> GetCategoryByIdAsync(int id);
        public Task<List<CategoryEntity>> GetCategoriesAsync();
        public Task PostCategoryAsync(CategoryEntity category);
        public Task<bool> DeleteCategoryAsync(int categoryId);
        public Task<bool> PutCategoryAsync(int categoryId, CategoryEntity category);
    }
}
