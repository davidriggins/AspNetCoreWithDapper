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

        public PaginationModel CategoryPagination(Category category, int page)
        {
            PaginationModel paginationModel = new PaginationModel();
            int totalCount = articleService.GetCount(category.CategoryId);
            paginationModel.TotalCount = totalCount;

            decimal pageSize = Math.Ceiling(decimal.Parse(totalCount.ToString()) / 3);
            int pageCount = (int)Math.Round(pageSize);

            paginationModel.PageCount = pageCount;

            List<Article> articles = articleService.GetByCategoryId(category.CategoryId);
            paginationModel.ArticleList = articles;

            string pageHtml = "";

            if (pageCount > 1)
            {
                for (int i = 1; i < pageCount + 1; i++)
                {
                    string active = i == page ? "active" : "";

                    pageHtml += "<li class='page-item " + active + "'><a href='/Category/" + category.Slug + "/" + i + "' class='page-link'>" + i + "</a></li>";
                }
            }

            string html = $@"<nav class='blog-pagination justify-content-center d-flex'>
                               <ul class='pagination'>
                {pageHtml}
                               </ul>
                             </nav>";

            paginationModel.Html = html;

            return paginationModel;
        }


        public PaginationModel ArticlePagination(int page)
        {
            PaginationModel paginationModel = new PaginationModel();
            int totalCount = articleService.CountArticles();
            paginationModel.TotalCount = totalCount;

            decimal pageSize = Math.Ceiling(decimal.Parse(totalCount.ToString()) / 3);
            int pageCount = (int)Math.Round(pageSize);

            paginationModel.PageCount = pageCount;

            List<Article> articles = articleService.GetArticles(page);
            paginationModel.ArticleList = articles;

            string pageHtml = "";

            if (pageCount > 1)
            {
                for (int i = 1; i < pageCount + 1; i++)
                {
                    string active = i == page ? "active" : "";

                    pageHtml += "<li class='page-item " + active + "'><a href='/?page=/" + i + "' class='page-link'>" + i + "</a></li>";
                }
            }

            string html = $@"<nav class='blog-pagination justify-content-center d-flex'>
                               <ul class='pagination'>
                {pageHtml}
                               </ul>
                             </nav>";

            paginationModel.Html = html;

            return paginationModel;
        }
    }
}
