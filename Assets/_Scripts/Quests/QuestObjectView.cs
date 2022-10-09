using System;
using _Scripts.Character;
using UnityEngine;

namespace _Scripts.Quests
{
    public class QuestObjectView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _completeSprite;
        [SerializeField] private int _id;

        public int ID => _id;

        private Sprite _defaulSprite;
        public Action<CharacterView> OnLevelObjectContact;

        private void Awake()
        {
            _defaulSprite = _spriteRenderer.sprite;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out CharacterView player))
            {
                OnLevelObjectContact?.Invoke(player);
            }
        }

        public void ProcessComplete()
        {
            _spriteRenderer.sprite = _completeSprite;
        }
        
        public void ProcessActivate()
        {
            _spriteRenderer.sprite = _defaulSprite;
        }
    }
}