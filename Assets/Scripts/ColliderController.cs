using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class ColliderController : MonoBehaviour
{
    public void DisableEnableWaitCollider(Transform transform)
    {
        Collider colliderComp = transform.gameObject.GetComponent<Collider>();

        if (colliderComp != null)
        {
            colliderComp.enabled = false;
            StartCoroutine(ColliderWaitCoroutine(colliderComp));
        }
    }

    IEnumerator ColliderWaitCoroutine(Collider collider)
    {
        yield return new WaitForSeconds(1);
        collider.enabled = true;
    }

}
