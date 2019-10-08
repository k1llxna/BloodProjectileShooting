using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    public GameObject bloodSplatter;

    private void Start()
    {
        //Add force to start the movement
        rb = GetComponent<Rigidbody>();

        rb.AddRelativeForce(new Vector3(0, 0, 2000));
        
        //Lifetime
        StartCoroutine(DeathTime());

        //Ignore collisions between bullets
        Physics.IgnoreLayerCollision(11, 11);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            
            var contact = collision.contacts[0];
            var rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Instantiate(bloodSplatter, contact.point, rot);
            
            enemy.Die();
        }

        Destroy(gameObject);
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
