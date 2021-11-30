using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public float spawnCooldown;
    private Coroutine spawning;

    private void Update()
    {
        if (spawning != null)
            return;
        spawning = StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform);
        yield return new WaitForSeconds(spawnCooldown);
        spawning = null;
    }
}
