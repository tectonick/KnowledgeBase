using KnowledgeBase.Models;
using System;

namespace KnowledgeBase.Logic
{
    public interface IScheduler
    {
        void AddRepeat(Theme theme, DateTime dateToRepeat);
        void Relearn(Theme theme);
        void Schedule(Theme theme);
    }
}