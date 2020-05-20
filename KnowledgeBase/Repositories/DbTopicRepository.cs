using KnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    class DbTopicRepository : ITopicRepository
    {
        MyDbContext _dbContext;
        public DbTopicRepository(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        public async Task Add(Topic newTopic)
        {
            _dbContext.Topics.Add(newTopic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Topic topic)
        {
            _dbContext.Topics.Remove(topic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Topic>> GetAllForUser(string userId)
        {
            return await _dbContext.Topics.Where(th => th.UserId == userId).Include(th => th.RepeatDates).ToListAsync();
        }

        public async Task<Topic> GetById(int id)
        {
            return await _dbContext.Topics.Where(a => a.Id == id).Include(th => th.RepeatDates).FirstOrDefaultAsync();
        }


        public async Task Update(Topic updatedTopic)
        {
            
            var oldTopic = _dbContext.Topics.Include(th => th.RepeatDates).ToList().Find(a => a.Id == updatedTopic.Id);
            var datesToDelete = oldTopic.RepeatDates.Select(rd => rd).Except(updatedTopic.RepeatDates);
            foreach (var date in datesToDelete)
            {
                _dbContext.DateModels.Remove(date);
            }
            
            oldTopic.CopyFrom(updatedTopic);
            await _dbContext.SaveChangesAsync();
        }

    }

}
