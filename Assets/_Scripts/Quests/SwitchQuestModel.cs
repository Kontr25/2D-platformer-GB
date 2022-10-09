using _Scripts.Character;
using UnityEngine;

namespace _Scripts.Quests
{
    public class SwitchQuestModel: IQuestModel
    {
        public bool TryComplete(GameObject activator)
        {
            return activator.GetComponent<CharacterView>();
        }
    }
}