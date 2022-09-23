using _Scripts.Animations;
using UnityEngine;

namespace _Scripts.Character
{
    public class MainHeroPhysicWalker
    {
        private const string _verticalAxisName = "Vertical";
        private const string _horizontalAxisName = "Horizontal";
        private const float _animationsSpeed = 10;
        private const float _walkSpeed = 150;
        private const float _jumpForse = 350;
        private const float _jumpThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _movingThresh = 0.1f;
        private bool _doJump;
        private float _goSideWay = 0;
        private readonly CharacterView _characterView;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly ContactsPoller _contactsPoller;
        
        public MainHeroPhysicWalker(CharacterView characterView, SpriteAnimator spriteAnimator, ContactsPoller contactsPoller)
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
            _contactsPoller = contactsPoller;
        }
        public void FixedUpdate()
        {
            if (!_characterView.Dead)
            {
                _doJump = Input.GetAxis(_verticalAxisName) > 0;
                _goSideWay = Input.GetAxis(_horizontalAxisName);
                _contactsPoller.Update();
                var walks = Mathf.Abs(_goSideWay) > _movingThresh;
                if(walks) _characterView.SpriteRenderer.flipX = _goSideWay < 0;
                var newVelocity = 0f;
                if (walks &&
                    (_goSideWay > 0 || !_contactsPoller.HasLeftContacts) &&
                    (_goSideWay < 0 || !_contactsPoller.HasRightContacts))
                {
                    newVelocity = Time.fixedDeltaTime * _walkSpeed *
                                  (_goSideWay < 0 ? -1 : 1);
                }
                _characterView.Rigidbody2D.velocity = _characterView.Rigidbody2D.velocity.Change(
                    x: newVelocity);
                if (_contactsPoller.IsGrounded && _doJump &&
                    Mathf.Abs(_characterView.Rigidbody2D.velocity.y) <= _jumpThresh)
                {
                    _characterView.Rigidbody2D.AddForce(Vector3.up * _jumpForse);
                }
                
                if (_contactsPoller.IsGrounded)
                {
                    var track = walks ? Track.run:Track.idle;
                    _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, track, true,
                        _animationsSpeed);
                }
                else if(Mathf.Abs(_characterView.Rigidbody2D.velocity.y) > _flyThresh)
                {
                    var track = Track.jump;
                    _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, track, true,
                        _animationsSpeed);
                }
            }
            else
            {
                var track = Track.dead;
                _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, track, false,_animationsSpeed);
                _characterView.Rigidbody2D.simulated = false;
                _characterView.FinishAction.Finish(FinishAction.FinishType.Lose);
            }
        }

    }
}