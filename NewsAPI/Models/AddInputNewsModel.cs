using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Entities.Models
{
    public class AddInputNewsModel
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
