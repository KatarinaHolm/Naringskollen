using Naringskollen.Models;

namespace Naringskollen.Repositories.IRepositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
    }
}