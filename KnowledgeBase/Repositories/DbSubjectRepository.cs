using KnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    class DbSubjectRepository : ISubjectRepository
    {
        MyDbContext _dbContext;
        public DbSubjectRepository(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public async Task Add(Subject newSubject)
        {
            _dbContext.Subjects.Add(newSubject);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task Delete(Subject subject)
        {
            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<List<Subject>> GetAllForUser(string userId)
        {
            return await _dbContext.Subjects.Where(sub => sub.UserId == userId)
                .Include(x => x.Topics).ThenInclude(th => th.RepeatDates)
                .ToListAsync<Subject>();
             
        }

        public async Task<Subject> GetById(int id)
        {
            return await _dbContext.Subjects.Where(a => a.Id == id).Include(x => x.Topics).ThenInclude(th => th.RepeatDates).FirstOrDefaultAsync();
        }

        public async Task Update(Subject updatedSubject)
        {
            _dbContext.Subjects.Update(updatedSubject);
            await _dbContext.SaveChangesAsync();
           
        }
    }

}
