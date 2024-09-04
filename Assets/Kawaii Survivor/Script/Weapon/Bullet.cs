using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] float bulletSpeed;
    [SerializeField] float rotateAngular;
    private int bulletDamage;
    private Vector3 direction;
    private bool isCritical;
    private bool isPlayerBullet;

    public void Shoot(Vector2 direction, int bulletDamage, bool isCritical, bool isPlayerBullet)
    {
        this.bulletDamage = bulletDamage;
        this.transform.up = direction;
        this.direction = direction;
        this.isCritical = isCritical;s
        this.isPlayerBullet = isPlayerBullet;
        this.rigidbody.velocity = this.direction * bulletSpeed;
        this.rigidbody.angularVelocity = rotateAngular;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && isPlayerBullet)
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage, isCritical);
            LeanPool.Despawn(this.gameObject);
        }

        if (collision.gameObject.layer == 7 && !isPlayerBullet)
        {
            collision.GetComponent<Player>().TakeDamage(bulletDamage);
            LeanPool.Despawn(this.gameObject);
        }
    }

    public void GetDirection(Vector2 direction, int bulletDamage)
    {
        this.direction = direction;
        this.bulletDamage = bulletDamage;
    }

}
