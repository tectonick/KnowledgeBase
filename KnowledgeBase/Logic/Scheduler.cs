using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic
{
    public class Scheduler : IScheduler
    {
        List<int> dayIntervals;
        public Scheduler()
        {
            dayIntervals = new List<int> { 1, 5, 25, 120, 2 * 365 }; //Pimsleur's graduated-interval default
        }
        public Scheduler(List<int> intervals)
        {
            dayIntervals = intervals;
        }
        public void Schedule(Topic topic)
        {
            DateTime learned = topic.DateLearned;
            foreach (var dayInterval in dayIntervals)
            {
                topic.RepeatDates.Add(new DateModel() { Date = learned.AddDays(dayInterval) });
            }
        }
        public void Relearn(Topic topic)
        {
            DateTime learned = DateTime.Now;
            learned=learned.AddMilliseconds(-learned.Millisecond);
            learned = learned.AddSeconds(-learned.Second);
            topic.RepeatDates.Clear();
            Schedule(topic);
        }

        public void AddRepeat(Topic topic, DateTime dateToRepeat)
        {
            
            if (topic.RepeatDates==null)
            {
                topic.RepeatDates = new List<DateModel>();
                
            }
            topic.RepeatDates.Add(new DateModel() { Date = dateToRepeat });
        }

    }
}
