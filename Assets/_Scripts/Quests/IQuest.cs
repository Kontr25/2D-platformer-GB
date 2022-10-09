using System;

namespace _Scripts.Quests
{
    public interface IQuest
    {
        event Action<IQuest> Completed;
        bool IsCompleted { get; }
        void Reset();
    }
}