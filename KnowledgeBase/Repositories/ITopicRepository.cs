using KnowledgeBase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetAllForUser(string userId);
        Task<Topic> GetById(int id);
        Task Add(Topic newTopic);
        Task Update(Topic updatedTopic);
        Task Delete(Topic theme);
    }

}
