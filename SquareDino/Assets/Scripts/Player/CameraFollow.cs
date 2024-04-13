using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SquareDino.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _basicCamera;
        [SerializeField]
        private CinemachineVirtualCamera _aimCamera;

        private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Init(Transform target)
        {
            _basicCamera.Follow = target;
            _basicCamera.LookAt = target;

            _aimCamera.Follow = target;
            _aimCamera.LookAt = target;
        }

        public void SetActiveAimMode(bool isActive)
        {
            _basicCamera.gameObject.SetActive(!isActive);
            _aimCamera.gameObject.SetActive(isActive);
        }
    }
}
