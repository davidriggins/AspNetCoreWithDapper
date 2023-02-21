using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace DapperFantom.Entities
{
    public class Article
    {
        [Dapper.Contrib.Extensions.Key]
        public int ArticleId { get; set; }
        public string Guid { get; set; }

        [Required(ErrorMessage = "Please select a Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a Name and Surname")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Please select a City")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Please enter an Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a correct email")]
        public string EMail { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter some Content")]
        public string Content { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Seen { get; set; }
        public int Status { get; set; }
        public int HomeView { get; set; }
        public int Hit { get; set; }
        public int CommentCount { get; set; }

    }
}
