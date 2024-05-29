using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner_Rotation : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float spawnRate;
    private float timeAfterSpawn;
    private float x;

    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRate = 0.3f;
    }

    void Update()
    {
        if (GameManager.isStart)
        {
            timeAfterSpawn += Time.deltaTime;
            x += Time.deltaTime * 100f;

            transform.rotation = (Quaternion.Euler(0, x, 0));
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;

                GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(0, x, 0));

                spawnRate = 0.3f;
            }
        }
        else
        {
            x = 0;
        }
    }
}
