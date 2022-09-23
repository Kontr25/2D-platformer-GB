using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Character
{
    public class ContactsPoller
    {
        private const float _collisionThresh = 0.5f;
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private int _contactsCount;
        private readonly Collider2D _collider2D;
        private CharacterView _characterView;
        public bool IsGrounded { get; private set; }
        public bool HasLeftContacts { get; private set; }
        public bool HasRightContacts { get; private set; }
        public ContactsPoller(CharacterView characterView)
        {
            _collider2D = characterView.Collider2D;
            _characterView = characterView;
        }
        public void Update()
        {
            IsGrounded = false;
            HasLeftContacts = false;
            HasRightContacts = false;
            _contactsCount = _collider2D.GetContacts(_contacts);
            for (int i = 0; i < _contactsCount; i++)
            {
                var normal = _contacts[i].normal;
                var rigidBody = _contacts[i].rigidbody;
                if (normal.y > _collisionThresh) IsGrounded = true;
                if (normal.x > _collisionThresh && rigidBody == null)
                    HasLeftContacts = true;
                if (normal.x < -_collisionThresh && rigidBody == null)
                    HasRightContacts = true;
            }

            CheckContactWithObstacle();
        }

        private void CheckContactWithObstacle()
        {
            if (_characterView.ObstacleCollider == null) return;
            else _characterView.Dead = true;
        }
    }
}