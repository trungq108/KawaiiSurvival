using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody2D rigidbody;
    private int bulletDamage;
    private Vector3 direction;
    private bool isCritical;

    public void Shoot(Vector2 direction, int bulletDamage, bool isCritical)
    {
        this.bulletDamage = bulletDamage;
        this.transform.up = direction;
        this.direction = direction;
        this.isCritical = isCritical;
        rigidbody.velocity = this.direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.layer)
        {
            case 6:   //Enemy
                collision.GetComponent<Enemy>().TakeDamage(bulletDamage, isCritical);
                LeanPool.Despawn(this);
                break;

            case 7:   //Player
                collision.GetComponent<Player>().TakeDamage(bulletDamage);
                LeanPool.Despawn(this);
                break; 

            //case 0:
            //    break;
        }

    }

    public void GetDirection(Vector2 direction, int bulletDamage)
    {
        this.direction = direction;
        this.bulletDamage = bulletDamage;
    }


}
