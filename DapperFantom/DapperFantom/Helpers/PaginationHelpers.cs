using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;

namespace DapperFantom.Helpers
{
    public class PaginationHelpers
    {
        private ArticleService articleService;

        public PaginationHelpers(IServiceProvider serviceProvider)
        {
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }

        public PaginationHelpers CategoryPagination(Category category, int page)
        {
            PaginationModel paginationModel = new PaginationModel();
            int totalCount = articleService.GetCount(category.CategoryId);
            paginationModel.TotalCount = totalCount;


            return paginationModel;
        }
    }
}
