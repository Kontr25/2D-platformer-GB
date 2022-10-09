using System;
using _Scripts.Cannon;
using _Scripts.Enemy;
using _Scripts.Environment;
using UnityEngine;

namespace _Scripts.Character
{
    public class CharacterView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Settings")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _animationsSpeed;
        [SerializeField] private float _jumpStartSpeed;
        [SerializeField] private float _movingTresh = 0.1f;
        [SerializeField] private float _flyTresh = 0.3f;
        [SerializeField] private float _groundLevel = 0.1f;
        [SerializeField] private float _acceleration = -10f;
        [SerializeField] private Collider2D _characterCollider2D;
        [SerializeField] private Rigidbody2D _characetrRigidbody2D;
        [SerializeField] private FinishAction _finishAction;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float WalkSpeed => _walkSpeed;
        public float AnimationsSpeed => _animationsSpeed;
        public float JumpStartSpeed=> _jumpStartSpeed;
        public float MovingTresh => _movingTresh;
        public float FlyTresh=> _flyTresh;
        public float GroundLevel => _groundLevel;
        public float Acceleration => _acceleration;
        public Collider2D ObstacleCollider { get; set; }
        public Collider2D Collider2D => _characterCollider2D;
        public Rigidbody2D Rigidbody2D => _characetrRigidbody2D;
        public  bool Dead { get; set; }

        public FinishAction FinishAction => _finishAction;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Obstacle obstacle) || col.TryGetComponent(out BulletView bullet))
            {
                Debug.Log("KEK");
                ObstacleCollider = col;
            }

            if (col.TryGetComponent(out VictoryPlace victoryPlace))
            {
                _finishAction.Finish(FinishAction.FinishType.Win);
            }
        }
    }
}