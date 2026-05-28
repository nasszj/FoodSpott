using System.Collections.Generic;

namespace Interfaces.Interface
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategories();
    }
}
