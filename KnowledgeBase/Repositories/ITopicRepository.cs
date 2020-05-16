using KnowledgeBase.Models;
using System.Collections.Generic;

namespace KnowledgeBase.Repositories
{
    public interface ITopicRepository
    {
        List<Topic> GetAllForUser(string userId);
        Topic GetByName(string name);
        Topic GetById(int id);
        Topic Add(Topic newTopic);
        Topic Update(Topic updatedTopic);
        Topic Delete(Topic theme);
    }

}
