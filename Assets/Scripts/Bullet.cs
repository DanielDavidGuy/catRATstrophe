using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private int hitDamage;
    [SerializeField] private int health;
    [SerializeField] private float lifetime;
	private string target;
	protected Rigidbody2D rb;
	protected float speed;

	public void setTarget(string name,Vector3 dir,float force){
		target = name;
		rb = GetComponent<Rigidbody2D>();
		speed=force;
		transform.up=dir;
		StartCoroutine(startCountdown());
	}

	public IEnumerator startCountdown(){
		yield return new WaitForSeconds(lifetime);
		Destroy(this.gameObject);
    }
	void FixedUpdate(){
		rb.linearVelocity = transform.up*speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == target) {
			other.GetComponent<Enemy2D>().TakeDamage(hitDamage);
			Destroy(this.gameObject);
		}
	}
}
