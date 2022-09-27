using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class LoseWindow : MonoBehaviour, IFinishable
    {
        [SerializeField] private Transform _endPoint;
        public void StartActionOnLose()
        {
                transform.DOMove(_endPoint.position, .5f);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
        public void StartActionOnWin()
        {
        }
    }
}