using UnityEngine;

namespace _Scripts.Character
{
    public class CharacterView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }
    }
}