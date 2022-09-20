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
    [SerializeField] private SpriteAnimationsConfig _CharacterSpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _EnemySpriteAnimationConfig;
    [SerializeField] private CannonView _cannonView;
    [SerializeField] private List<BulletView> _bullets;

    private ParalaxManager _paralaxManager;
    private SpriteAnimator _characterSpriteAnimator;
    private SpriteAnimator _wolfSpriteAnimator;
    private MainHeroWalker _mainHeroWalker;
    private AimingMuzzle _aimingMuzzle;
    private BulletsEmitter _bulletsEmitter;

    private void Start()
    {
        _paralaxManager = new ParalaxManager(_camera, _background.transform);
        _characterSpriteAnimator = new SpriteAnimator(_CharacterSpriteAnimationConfig);
        _mainHeroWalker = new MainHeroWalker(_characterView, _characterSpriteAnimator);
        _aimingMuzzle = new AimingMuzzle(_cannonView.transform, _characterView.transform);
        _bulletsEmitter = new BulletsEmitter(_bullets, _cannonView.MuzzleTransform);
        
        _wolfSpriteAnimator = new SpriteAnimator(_EnemySpriteAnimationConfig);
        _wolfSpriteAnimator.StartAnimation(_enemyView.SpriteRenderer, Track.idle, true, 10);
    }

    private void Update()
    {
        _paralaxManager.Update();
        _characterSpriteAnimator.Update();
        _mainHeroWalker.Update();
        _aimingMuzzle.Update();
        _bulletsEmitter.Update();
        _wolfSpriteAnimator.Update();
    }
}
