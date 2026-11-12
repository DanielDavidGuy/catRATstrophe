using UnityEngine;
using System.Collections.Generic;

public class Enemy2D : MonoBehaviour {
	public static HashSet<Enemy2D> Enemies = new HashSet<Enemy2D>();
    [SerializeField]protected float health, moveSpeed;
    private float currentHealth;
	protected Rigidbody2D rb;
	
	void Awake() {
		Enemies.Add(this);
	}

    void Start() {
        currentHealth = health;
		rb = GetComponent<Rigidbody2D>();
    }
	
	public void setDirection(Vector3 dir){
		transform.up = dir;
	}

    void FixedUpdate(){
		rb.linearVelocity = transform.up * moveSpeed;
	}

    public void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) {
			Destroy(gameObject);
        }
    }
	
	void OnDestroy() {
		Enemies.Remove(this);
	}
}