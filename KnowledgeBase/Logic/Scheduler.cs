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
        public void Schedule(Theme theme)
        {
            DateTime learned = theme.DateLearned;
            foreach (var dayInterval in dayIntervals)
            {
                theme.RepeatDates.Add(new DateModel() { Date = learned.AddDays(dayInterval) });
            }
        }
        public void Relearn(Theme theme)
        {
            DateTime learned = DateTime.Now;
            learned=learned.AddMilliseconds(-learned.Millisecond);
            learned = learned.AddSeconds(-learned.Second);
            theme.RepeatDates.Clear();
            Schedule(theme);
        }

        public void AddRepeat(Theme theme, DateTime dateToRepeat)
        {
            
            if (theme.RepeatDates==null)
            {
                theme.RepeatDates = new List<DateModel>();
                
            }
            theme.RepeatDates.Add(new DateModel() { Date = dateToRepeat });
        }

    }
}
