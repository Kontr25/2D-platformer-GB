using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Quests
{
    public class QuestStory : IQuestStory, IDisposable
    {
        private readonly List<IQuest> _questsCollection;

        public bool isDone => _questsCollection.All(value => value.IsCompleted);

        public QuestStory(List<IQuest> questsCollection)
        {
            _questsCollection = questsCollection;
            Subscribes();
            ReserQuest(0);
        }

        private void Subscribes()
        {
            foreach (var quest in _questsCollection)
                quest.Completed += OnQuestCompleted;
        }
        
        private void UnSubscribes()
        {
            foreach (var quest in _questsCollection)
                quest.Completed -= OnQuestCompleted;
        }

        private void OnQuestCompleted(IQuest quest)
        {
            var index = _questsCollection.IndexOf((quest));

            if (isDone) Debug.Log("Quest Complete!");
            else ReserQuest(++index);

        }

        private void ReserQuest(int index)
        {
            if(index < 0 || index >= _questsCollection.Count) return;

            var nextQuest = _questsCollection[index];
            
            if(nextQuest.IsCompleted)
                OnQuestCompleted((nextQuest));
            else
            {
                _questsCollection[index].Reset();
            }
        }

        public void Dispose()
        {
            UnSubscribes();
        }
    }
}