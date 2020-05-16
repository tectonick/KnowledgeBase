using KnowledgeBase.Models;
using System;

namespace KnowledgeBase.Logic
{
    public interface IScheduler
    {
        void AddRepeat(Topic topic, DateTime dateToRepeat);
        void Relearn(Topic topic);
        void Schedule(Topic topic);
    }
}