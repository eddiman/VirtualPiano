using System;
using UnityEngine;
using UnityEngine.Events;

namespace Essentials
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform>
    {
    }
    public class OnTriggerEvents: MonoBehaviour
    {
        public string colliderTag;
        public TransformEvent onTriggerEnter;
        public TransformEvent onTriggerStay;
        public TransformEvent onTriggerExit;


        private void OnTriggerEnter(Collider other)
        {
            if (colliderTag == "")
            {
                onTriggerEnter.Invoke(other.transform);
            }
            else if (other.gameObject.CompareTag(colliderTag))
            {
                onTriggerEnter.Invoke(other.transform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (colliderTag == "")
            {
                onTriggerStay.Invoke(other.transform);
            }
            else if (other.gameObject.CompareTag(colliderTag))
            {
                onTriggerStay.Invoke(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (colliderTag == "")
            {
                onTriggerExit.Invoke(other.transform);
            }
            else if (other.gameObject.CompareTag(colliderTag))
            {
                onTriggerExit.Invoke(other.transform);
            }
        }
    }
}
