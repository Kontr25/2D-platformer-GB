using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Quests
{
    [CreateAssetMenu(fileName = "QuestItemsConfig", menuName = "Quest/QuestItemsConfig")]
    public class QuestItemsConfig : ScriptableObject
    {
        [SerializeField] private int _questId;
        [SerializeField] private List<int> _questItemIdCollection;

        public int QuestId => _questId;

        public List<int> QuestItemIdCollection => _questItemIdCollection;
    }
}