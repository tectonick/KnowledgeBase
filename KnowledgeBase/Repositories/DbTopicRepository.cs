using KnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Repositories
{
    class DbTopicRepository : ITopicRepository
    {
        MyDbContext _dbContext;
        public DbTopicRepository(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        public Topic Add(Topic newTopic)
        {
            _dbContext.Topics.Add(newTopic);
            _dbContext.SaveChanges();
            return newTopic;
        }

        public Topic Delete(Topic topic)
        {
            _dbContext.Topics.Remove(topic);
            _dbContext.SaveChanges();
            return topic;
        }

        public List<Topic> GetAllForUser(string userId)
        {
            return _dbContext.Topics.Where(th => th.UserId == userId).Include(th => th.RepeatDates).ToList();
        }

        public Topic GetById(int id)
        {
            return _dbContext.Topics.Include(th => th.RepeatDates).ToList().Find(a => a.Id == id);
        }

        public Topic GetByName(string name)
        {
            return _dbContext.Topics.Include(th => th.RepeatDates).FirstOrDefault(th => th.Name == name);
        }

        public Topic Update(Topic updatedTopic)
        {
            
            var oldTopic = _dbContext.Topics.Include(th => th.RepeatDates).ToList().Find(a => a.Id == updatedTopic.Id);
            var datesToDelete = oldTopic.RepeatDates.Select(rd => rd).Except(updatedTopic.RepeatDates);
            foreach (var date in datesToDelete)
            {
                _dbContext.DateModels.Remove(date);
            }
            
            oldTopic.CopyFrom(updatedTopic);
            _dbContext.SaveChanges();
            return updatedTopic;
        }

    }

}
