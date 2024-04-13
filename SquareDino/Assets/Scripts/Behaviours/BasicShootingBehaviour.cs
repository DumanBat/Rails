using SquareDino.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class BasicShootingBehaviour : MonoBehaviour, IShootable
    {
        private Camera _mainCamera;
        private Weapon _weapon;

        public void Init(Weapon weapon, Camera camera)
        {
            _mainCamera = camera;
            _weapon = weapon;
        }

        public void Shoot()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                _weapon.Fire(hit.point);
            }
        }
    }
}
