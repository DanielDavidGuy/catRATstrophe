using UnityEngine;

public class Enemy2D : MonoBehaviour {
    [SerializeField]protected float health, moveSpeed;
    private float currentHealth;
	protected Rigidbody2D rb;

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
}