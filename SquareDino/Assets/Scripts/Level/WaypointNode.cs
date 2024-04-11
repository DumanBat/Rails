using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class WaypointNode : MonoBehaviour
    {
        [SerializeField]
        private bool _isStart;
        [SerializeField]
        private bool _isEnd;
        [SerializeField]
        private bool _isShootingPoint;

        public bool IsStart => _isStart;
        public bool IsEnd => _isEnd;
        public bool IsShootingPoint => _isShootingPoint;

        public bool IsReached { get; set; }

        public Vector3 GetPosition() => transform.position;
    }
}
