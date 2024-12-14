using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour {

    private Rigidbody2D rb;

    [SerializeField] private int damage;
    [SerializeField] private float speed, offset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.GetComponent<HealthSystem>().OnDamage(damage);
        Destroy(gameObject);
    }

    public void MoveProjectile(Vector3 target) {

        rb.velocity = target * speed;

        Vector3 direction = transform.position - target;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed);
    }
}
