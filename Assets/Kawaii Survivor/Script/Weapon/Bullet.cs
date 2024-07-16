using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private int bulletDamage;
    private Vector3 direction;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(bulletDamage);
        }
    }


    public void GetDirection(Vector2 direction, int bulletDamage)
    {
        this.direction = direction;
        this.bulletDamage = bulletDamage;
    }
}
