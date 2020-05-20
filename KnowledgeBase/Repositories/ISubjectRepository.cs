using KnowledgeBase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllForUser(string userId);
        Task<Subject> GetById(int id);
        Task Add(Subject newSubject);
        Task Update(Subject updatedSubject);
        Task Delete(Subject subject);
    }
}
