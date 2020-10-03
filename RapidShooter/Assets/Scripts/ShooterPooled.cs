using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPooled : MonoBehaviour
{
    [SerializeField] private float refireRate = 0.2f;

    private float fireTimer = 0;

    void Update()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer >= refireRate)
        {
            fireTimer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        var shot = PoolScript.Instance.Get();
        shot.transform.rotation = transform.rotation;
        shot.transform.position = transform.position;
        shot.gameObject.SetActive(true);
    }
}
