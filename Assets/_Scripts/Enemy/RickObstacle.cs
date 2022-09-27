using System;
using System.Collections;
using _Scripts.Character;
using UnityEngine;
using DG.Tweening;

namespace _Scripts.Enemy
{
    public class RickObstacle : MonoBehaviour
    {
        [SerializeField] private Transform _rickTransform;
        [SerializeField] private FinishAction _finishAction;

        [SerializeField]
        private float _delay;
        private WaitForSeconds _wait;

        private void Start()
        {
            _wait = new WaitForSeconds(_delay);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out CharacterView Player))
            {
                Debug.Log("KEK");
                StartCoroutine(DefeatWithDelay());
            }
        }

        private IEnumerator DefeatWithDelay()
        {
            _rickTransform.DOScale(1f, 1f);
            _rickTransform.DOMoveY(_rickTransform.position.y + 1.6f, 1f);
            yield return _wait;
            _finishAction.Finish(FinishAction.FinishType.Lose);
        }
    }
}