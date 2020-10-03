using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float moveSpeed = 10f;

    private float lifeTime;
    private float maxLifetime = 5f;

    private void OnEnable()
    {
        lifeTime = 0f;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        lifeTime += Time.deltaTime;
        if(lifeTime > maxLifetime)
        {
            PoolScript.Instance.ReturnToPool(this);
        }
    }
}
