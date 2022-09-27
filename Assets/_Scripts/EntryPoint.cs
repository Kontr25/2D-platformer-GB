using System;
using System.Collections.Generic;
using _Scripts.Animations;
using _Scripts.Cannon;
using _Scripts.Character;
using _Scripts.Enemy;
using _Scripts.Environment;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _background;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private ObstacleView _fireballView;
    [SerializeField] private SpriteAnimationsConfig _CharacterSpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _EnemySpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _FireBallSpriteAnimationConfig;
    [SerializeField] private CannonView _cannonView;
    [SerializeField] private List<BulletView> _bullets;

    private ContactsPoller _contactsPoller;
    private ParalaxManager _paralaxManager;
    private SpriteAnimator _characterSpriteAnimator;
    private SpriteAnimator _wolfSpriteAnimator;
    private SpriteAnimator _fireballSpriteAnimator;
    private MainHeroWalker _mainHeroWalker;
    private MainHeroPhysicWalker _mainHeroPhysicWalker;
    private AimingMuzzle _aimingMuzzle;
    private BulletsEmitter _bulletsEmitter;

    private void Start()
    {
        _contactsPoller = new ContactsPoller(_characterView);
        _paralaxManager = new ParalaxManager(_camera, _background.transform);
        _characterSpriteAnimator = new SpriteAnimator(_CharacterSpriteAnimationConfig);
        _mainHeroPhysicWalker = new MainHeroPhysicWalker(_characterView, _characterSpriteAnimator,_contactsPoller);
        //_mainHeroWalker = new MainHeroWalker(_characterView, _characterSpriteAnimator);
        _aimingMuzzle = new AimingMuzzle(_cannonView.transform, _characterView.transform);
        _bulletsEmitter = new BulletsEmitter(_bullets, _cannonView.MuzzleTransform);
        
        _wolfSpriteAnimator = new SpriteAnimator(_EnemySpriteAnimationConfig);
        _fireballSpriteAnimator = new SpriteAnimator(_FireBallSpriteAnimationConfig);
        _wolfSpriteAnimator.StartAnimation(_enemyView.SpriteRenderer, Track.idle, true, 10);
        _fireballSpriteAnimator.StartAnimation(_fireballView.SpriteRenderer, Track.idle, true, 25);
    }

    private void Update()
    {
        _paralaxManager.Update();
        _characterSpriteAnimator.Update();
        //_mainHeroWalker.Update();
        _aimingMuzzle.Update();
        _bulletsEmitter.Update();
        _wolfSpriteAnimator.Update();
        _fireballSpriteAnimator.Update();
    }

    private void FixedUpdate()
    {
        _mainHeroPhysicWalker.FixedUpdate();
    }
}
