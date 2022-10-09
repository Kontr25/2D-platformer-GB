using UnityEngine;

namespace _Scripts.Quests
{
    [CreateAssetMenu(fileName = "QuestStoryConfig", menuName = "Quest/QuestStoryConfig")]
    public class QuestStoryConfig : ScriptableObject
    {
        [SerializeField] private QuestConfig[] _questst;
        [SerializeField] private QuestStoryType questQStoryType;
        
        public QuestConfig[] Questst => _questst;
        public QuestStoryType QStoryType => questQStoryType;

        
    }

    public enum QuestStoryType
    {
        Common,
        Resettable
    }
}