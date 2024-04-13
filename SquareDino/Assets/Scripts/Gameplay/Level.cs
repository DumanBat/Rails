using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private List<WaypointNode> _nodes = new List<WaypointNode>();

        public List<WaypointNode> Nodes => _nodes;
    }
}
