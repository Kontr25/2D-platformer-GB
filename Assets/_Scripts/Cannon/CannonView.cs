using UnityEngine;

namespace _Scripts.Cannon
{
    public class CannonView : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;

        public Transform MuzzleTransform => _muzzleTransform;
    }

}