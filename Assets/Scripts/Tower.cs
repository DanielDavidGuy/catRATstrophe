using UnityEngine;
using System.Collections;

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

    protected virtual IEnumerator Shoot(Vector2 shootDirection,float shootForce){
		Bullet newBullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
		newBullet.setTarget("Enemy",shootDirection,shootForce);
		canShoot = false;
		yield return new WaitForSeconds(reloadTime);
		canShoot = true;
	}
}
