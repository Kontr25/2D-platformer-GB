using System;
using System.Collections.Generic;
using _Scripts.Animations;
using _Scripts.Cannon;
using _Scripts.Character;
using _Scripts.Enemy;
using _Scripts.Environment;
using Pathfinding;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _background;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private ObstacleView _fireballView;
    [SerializeField] private EnemyView _batView;
    [SerializeField] private SpriteAnimationsConfig _CharacterSpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _EnemySpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _FireBallSpriteAnimationConfig;
    [SerializeField] private SpriteAnimationsConfig _BatSpriteAnimationConfig;
    [SerializeField] private CannonView _cannonView;
    [SerializeField] private List<BulletView> _bullets;
    
    [Header("Protector AI")]
    [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
    [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
    [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
    [SerializeField] private Transform[] _protectorWaypoints;

    private ContactsPoller _contactsPoller;
    private ParalaxManager _paralaxManager;
    private SpriteAnimator _characterSpriteAnimator;
    private SpriteAnimator _wolfSpriteAnimator;
    private SpriteAnimator _fireballSpriteAnimator;
    private SpriteAnimator _batSpriteAnimator;
    private MainHeroWalker _mainHeroWalker;
    private MainHeroPhysicWalker _mainHeroPhysicWalker;
    private AimingMuzzle _aimingMuzzle;
    private BulletsEmitter _bulletsEmitter;
    private SimplePatrolAI _simplePatrolAI;
    private ProtectorAI _protectorAI;
    private ProtectedZone _protectedZone;

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
        _batSpriteAnimator = new SpriteAnimator(_BatSpriteAnimationConfig);
        _wolfSpriteAnimator.StartAnimation(_enemyView.SpriteRenderer, Track.idle, true, 10);
        _fireballSpriteAnimator.StartAnimation(_fireballView.SpriteRenderer, Track.idle, true, 25);
        _batSpriteAnimator.StartAnimation(_batView.SpriteRenderer, Track.idle, true, 10);
        
        _protectorAI = new ProtectorAI(_characterView, new PatrolAIModel(_protectorWaypoints), _protectorAIDestinationSetter, _protectorAIPatrolPath);
        _protectorAI.Init();
      
        _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector>{ _protectorAI });
        _protectedZone.Init();
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
        _batSpriteAnimator.Update();
    }

    private void FixedUpdate()
    {
        _mainHeroPhysicWalker.FixedUpdate();
    }

    private void OnDestroy()
    {
        _protectorAI.Deinit();
        _protectedZone.Deinit();
    }
}
