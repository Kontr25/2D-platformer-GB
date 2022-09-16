using System;
using _Scripts.Animations;
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

    private ParalaxManager _paralaxManager;
    private SpriteAnimator _characterSpriteAnimator;
    private SpriteAnimator _wolfSpriteAnimator;

    private void Start()
    {
        _paralaxManager = new ParalaxManager(_camera, _background.transform);
        
        _characterSpriteAnimator = new SpriteAnimator(_CharacterSpriteAnimationConfig);
        _characterSpriteAnimator.StartAnimation(_characterView.SpriteRenderer, Track.idle, true, 10);
        
        _wolfSpriteAnimator = new SpriteAnimator(_EnemySpriteAnimationConfig);
        _wolfSpriteAnimator.StartAnimation(_enemyView.SpriteRenderer, Track.idle, true, 10);
    }

    private void Update()
    {
        _paralaxManager.Update();
        _characterSpriteAnimator.Update();
        _wolfSpriteAnimator.Update();
    }
}
