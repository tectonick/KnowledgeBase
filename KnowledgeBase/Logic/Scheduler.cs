using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic
{
    public class Scheduler
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
            DateTime learned = theme.RepeatDates[0];
            foreach (var dayInterval in dayIntervals)
            {
                theme.RepeatDates.Add(learned.AddDays(dayInterval));
            }
        }
        public void Relearn(Theme theme)
        {
            DateTime learned = DateTime.Now;
            theme.RepeatDates.Clear();
            theme.RepeatDates.Add(learned);
            Schedule(theme);
        }

        public void AddRepeat(Theme theme, DateTime dateToRepeat)
        {
            int indexToInsert = theme.RepeatDates.FindIndex(date => DateTime.Compare(date, dateToRepeat) >= 0);
            theme.RepeatDates.Insert(indexToInsert, dateToRepeat);
        }

    }
}
