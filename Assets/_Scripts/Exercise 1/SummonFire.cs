using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFire : MonoBehaviour
{
    public GameObject firePrefab;
    private GameObject fireObject;
    public float scaleFactor = 0.01f;
    public Transform imageTarget;

    public void SpawnFire()
    {
        if (fireObject == null)
        {
            fireObject = Instantiate(firePrefab, imageTarget);
            fireObject.transform.localScale = Vector3.one * scaleFactor;
        }
        else
        {
            RemoveFire();
        }
    }

    public void RemoveFire()
    {
        if (fireObject != null)
        {
            Destroy(fireObject);
        }
    }
}
