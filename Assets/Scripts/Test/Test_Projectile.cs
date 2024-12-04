using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test_Projectile : MonoBehaviour {

    public static Test_Projectile Create(Vector3 startPosition, Vector3 target, float speed) {

        Transform projectile = Instantiate(GameAssets.Assets.p_Projectile, startPosition, Quaternion.identity);

        Test_Projectile test_Projectile = projectile.GetComponent<Test_Projectile>();
        test_Projectile.SetUp(target, speed);
        return test_Projectile;
    }
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed, offset;


    public void SetUp(Vector3 target, float speed) {
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = speed;
        rb.velocity = target * moveSpeed;

        Vector3 direction = transform.position - target;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset; 
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, moveSpeed);
    }
}