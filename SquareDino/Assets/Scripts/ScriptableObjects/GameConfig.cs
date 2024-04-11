using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Player
{
    [CreateAssetMenu(fileName = "NewGameConfig", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;

        public float MoveSpeed => _moveSpeed;
    }
}