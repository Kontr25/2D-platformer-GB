using UnityEngine;

namespace _Scripts.Quests
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "Quest/QuestConfig")]
    public class QuestConfig: ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private QuestType _questType;

        public int ID => _id;

        public QuestType QType => _questType; 
    }

    public enum QuestType
    {
        Switch
    }
}