using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : LevelElement {
	[SerializeField] private Enemy2D enemyPrefab;
	[SerializeField] private float spawnTime;
	
	private bool canSpawn;
	
	void Start() {
        canSpawn = true;
    }

    void FixedUpdate() {
        if (canSpawn) StartCoroutine(Spawn(Vector2.up));
    }

    protected virtual IEnumerator Spawn(Vector2 spawnDirection){
		Enemy2D newEnemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity);
		newEnemy.setDirection(spawnDirection);
		canSpawn = false;
		yield return new WaitForSeconds(spawnTime);
		canSpawn = true;
	}
}