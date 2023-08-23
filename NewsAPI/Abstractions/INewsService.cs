using NewsAPI.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using News.DAL.Entities;

namespace NewsAPI.Abstractions
{
    interface INewsService
    {
        public Task<List<NewsEntity>> GetAll();

        public Task<NewsEntity> Get(long newsId);

        public Task Add(int userId, AddInputNewsModel news);

        public Task Update(int userId, long newsId, UpdateInputNewsModel news);

        public Task Delete(int userId, long newsId);
    }
}
