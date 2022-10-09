using UnityEngine;

namespace _Scripts.Quests
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}