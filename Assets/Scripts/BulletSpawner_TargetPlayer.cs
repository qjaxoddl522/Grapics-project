using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner_TargetPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;
    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (GameManager.isStart)
        {
            timeAfterSpawn += Time.deltaTime;
            transform.LookAt(target);
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;

                GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation);

                bullet.transform.LookAt(target);

                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
    }
}
