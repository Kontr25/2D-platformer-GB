using UnityEngine;

namespace _Scripts.Environment
{
    public class ParalaxManager
    {
        private const float _coef = 0.3f;
        
        private readonly Camera _camera;
        private readonly Transform _backTransform;
        private readonly Vector3 _backStartPosition;
        private readonly Vector3 _cameraStartPosition;

        public ParalaxManager(Camera camera, Transform backTransform)
        {
            _camera = camera;
            _backTransform = backTransform;
            _backStartPosition = backTransform.position;
            _cameraStartPosition = camera.transform.position;
        }

        public void Update()
        {
            _backTransform.position = _backStartPosition + (_camera.transform.position - _cameraStartPosition) * _coef;
        }
    }
}