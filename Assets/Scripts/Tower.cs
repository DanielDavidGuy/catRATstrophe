using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : LevelElement {
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int health, damage;
    [SerializeField] private float shootingSpeed, reloadTime, shootForce;
    private bool canShoot;
	private Vector2 lookDirection;
	private Enemy2D target;

    void Start() {
        canShoot = true;
    }

    void FixedUpdate() {
		target = GetClosestEnemy();
		lookAtEnemy(target);
        if (canShoot && target != null) StartCoroutine(Shoot(lookDirection, shootForce));
    }
	
	protected Enemy2D GetClosestEnemy() {
		Vector3 pos = this.transform.position;
		float dist = float.PositiveInfinity;
		Enemy2D targ = null;
		foreach(Enemy2D enemy in Enemy2D.Enemies) {
			var d = (pos - enemy.transform.position).sqrMagnitude;
			if(d < dist) {
				targ = enemy;
				dist = d;
			}
		}
		return targ;
	}

	protected void lookAtEnemy(Enemy2D enemy)
	{
		if (enemy == null) return;
		lookDirection = enemy.transform.position - transform.position;
		transform.eulerAngles = new Vector3(0, 0, -90 + Mathf.Atan2(lookDirection.y, lookDirection.x) * 180 / Mathf.PI);
	}

    protected virtual IEnumerator Shoot(Vector2 shootDirection,float shootForce){
		Bullet newBullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
		newBullet.setTarget("Enemy",shootDirection,shootForce);
		newBullet.setDamage(damage);
		canShoot = false;
		yield return new WaitForSeconds(reloadTime);
		canShoot = true;
	}
}
