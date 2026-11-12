using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, LevelElement {
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int health, damage;
    [SerializeField] private float shootingSpeed, reloadTime, shootForce;
    private bool canShoot;

    void Start() {
        canShoot = true;
    }

    void FixedUpdate() {
        if (canShoot) StartCoroutine(Shoot(Vector2.down, shootForce));
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

    protected virtual IEnumerator Shoot(Vector2 shootDirection,float shootForce){
		Bullet newBullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
		newBullet.setTarget("Enemy",shootDirection,shootForce);
		newBullet.setDamage(damage);
		canShoot = false;
		yield return new WaitForSeconds(reloadTime);
		canShoot = true;
	}
}
