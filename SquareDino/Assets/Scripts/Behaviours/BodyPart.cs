using SquareDino.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class BodyPart : MonoBehaviour, ITargetPart
    {
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private Rigidbody _rb;

        private RagdollBehaviour _originRagdoll;
        private ITarget _originTarget;

        public Collider Collider => _collider;
        public Rigidbody Rigidbody => _rb;

        public ITarget OriginTarget => _originTarget;

        public void Init(RagdollBehaviour ragdoll, ITarget target)
        {
            _originRagdoll = ragdoll;
            _originTarget = target;
        }

        public void SetHitData(float force)
        {
            _originRagdoll.OnBodyPartHit?.Invoke(this, force);
        }
    }
}
