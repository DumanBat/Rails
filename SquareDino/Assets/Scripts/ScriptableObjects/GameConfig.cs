using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    [CreateAssetMenu(fileName = "NewGameConfig", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private List<Level> _levels = new List<Level>();

        public float MoveSpeed => _moveSpeed;
        public List<Level> Levels => _levels;
    }
}