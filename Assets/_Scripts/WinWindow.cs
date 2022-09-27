namespace _Scripts
{
    using DG.Tweening;
    using UnityEngine;
    public class WinWindow : MonoBehaviour, IFinishable
    {
        [SerializeField] private Transform _endPoint;
        public void StartActionOnWin()
        {
                transform.DOMove(_endPoint.position, .5f);
        }

        public void StartActionOnLose()
        {
        }
    }
}