using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class FinishAction : MonoBehaviour
    {
        public Action<FinishType> Finish;
    
        [SerializeField] private List<GameObject> _finishableObjects;

        private float _sessionTime = 0f;

        private void Start()
        {
            Finish += Activate;
        }

        private void OnDestroy()
        {
            Finish = null;
        }

        private void Activate(FinishType finishType = FinishType.None)
        {
            if (_finishableObjects.Count <= 0) 
                return;
            if (_finishableObjects.Count > 0)
            {
                switch (finishType)
                {
                    case FinishType.Win:

                        foreach (var obj in _finishableObjects)
                        {
                            if (obj.TryGetComponent(out IFinishable finishable))
                                finishable.StartActionOnWin();
                        }
                        break;

                    case FinishType.Lose:
                        foreach (var obj in _finishableObjects)
                        {
                            if (obj.TryGetComponent(out IFinishable finishable))
                                finishable.StartActionOnLose();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public enum FinishType
        {
            None,
            Win,
            Lose
        }
    }
}