using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class RagdollBehaviour : MonoBehaviour, IRagdoll
    {
        [SerializeField]
        private Rigidbody _mainRb;
        [SerializeField]
        private Collider _mainCollider;
        [SerializeField]
        private List<BodyPart> _bodyParts = new List<BodyPart>();

        private ITarget _originTarget;
        private BodyPart _lastHitBodyPart;

        public Action<BodyPart> OnBodyPartHit;

        public void Awake()
        {
            _originTarget = GetComponent<ITarget>();
            foreach (var bodyPart in _bodyParts)
                bodyPart.Init(this, _originTarget);

            OnBodyPartHit += SetLastHitBodyPart;
        }

        public void TurnOn()
        {
            _mainRb.useGravity = false;
            _mainRb.velocity = Vector3.zero;
            _mainCollider.enabled = false;

            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.Collider.isTrigger = false;
                bodyPart.Rigidbody.velocity = Vector3.zero;
                bodyPart.Rigidbody.isKinematic = false;
            }
        }

        public void TurnOff()
        {
            _mainRb.useGravity = true;
            _mainCollider.enabled = true;

            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.Collider.isTrigger = true;
                bodyPart.Rigidbody.isKinematic = true;
            }
        }
        public void ApplyForce(Vector3 forceOriginPosition)
        {
            var direction = _lastHitBodyPart.transform.position - forceOriginPosition;
            _lastHitBodyPart.Rigidbody.AddForce(direction * 5f, ForceMode.Impulse);
        }

        private void SetLastHitBodyPart(BodyPart bodyPart)
        {
            _lastHitBodyPart = bodyPart;
        }
    }
}
