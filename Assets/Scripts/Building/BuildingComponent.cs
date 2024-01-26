using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Earthquake.Building
{
    public class BuildingComponent : MonoBehaviour
    {
        [SerializeField] private GameObject m_Object;
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] private Collider m_Collider;

        public float weight
        {
            get; private set;
        }

        public Rigidbody GetRigidbody()
        {
            return m_Rigidbody;
        }

        public Collider GetCollider()
        {
            return m_Collider;
        }

        public GameObject GetObject()
        {
            return m_Object;
        }

        public virtual void ApplyPushForce(float value, Vector3 vector)
        {
        }
    }
}