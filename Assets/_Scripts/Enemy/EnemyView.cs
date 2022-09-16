using UnityEngine;
namespace _Scripts.Enemy
{
    public class EnemyView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }
    }
}