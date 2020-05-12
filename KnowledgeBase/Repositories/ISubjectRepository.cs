using KnowledgeBase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetByName(string name);
        Subject GetById(int id);
        Subject Add(Subject newSubject);
        Subject Update(Subject updatedSubject);
        Subject Delete(Subject subject);
    }
}
