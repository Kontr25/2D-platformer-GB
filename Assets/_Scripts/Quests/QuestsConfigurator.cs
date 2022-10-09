using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Quests
{
    public class QuestsConfigurator: MonoBehaviour
    {
        [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
        
        [SerializeField] private QuestObjectView[] _questObjects;
        
        [SerializeField] private QuestObjectView _singleQuestView;

        private Quest _singleQuest;
        private List<IQuestStory> _questStories;

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories =
            new Dictionary<QuestType, Func<IQuestModel>>()
            {
                {QuestType.Switch, () =>  new SwitchQuestModel()}
            };
        
        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories =
            new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>()
            {
                {QuestStoryType.Common, questCollection => new QuestStory(questCollection)} 
            };

        private void Start()
        {
            _singleQuest = new Quest(_singleQuestView, new SwitchQuestModel());
            _singleQuest.Reset();

            _questStories = new List<IQuestStory>();

            foreach (var questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig questStoryConfig)
        {
            var quests = new List<IQuest>();
            foreach (var questConfig in questStoryConfig.Questst)
            {
                var quest = CreateQuest(questConfig);
                
                if(quest == null) continue;
                quests.Add(quest);
            }

            return _questStoryFactories[questStoryConfig.QStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig questConfig)
        {
            var questId = questConfig.ID;
            var questView = _questObjects.FirstOrDefault(value => value.ID == questId);

            if (questView == null) return null;

            if (_questFactories.TryGetValue(questConfig.QType, out var factory))
            {
                var questModel = factory.Invoke();
                return new Quest(questView, questModel);
            }

            return null;
        }

        private void OnDestroy()
        {
            _singleQuest.Dispose();
            _questStories.Clear();
        }
    }
}