using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Gun
{
    public float fireRate;

    private bool isShooting;

    override public void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(Shooting());
        }
    }

    private IEnumerator Shooting()
    {
        while(Input.GetMouseButton(0))
        {
            Fire();
            animator.SetTrigger("Recoil");
            yield return new WaitForSeconds(fireRate);
        }

        isShooting = false;
    }


    void Fire()
    {
        ammo--;

        FindObjectOfType<AudioManager>().Play("Gunshot");

        RaycastHit hit;

        float spreadX = Random.Range(-2f, 2f);
        float spreadY = Random.Range(-2f, 2f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Transform newBullet = Instantiate(bullet, muzzle.position, transform.rotation).transform;
            newBullet.LookAt(hit.point);
            newBullet.Rotate(new Vector3(spreadX, spreadY, 0));

            //Enemy enemy = hit.transform.GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //   // score += enemy.pointValue;
            //   // scoreText.text = "Points: " + score;
            //    enemy.Die();
            //}
        }
        else
        {
            Transform newBullet = Instantiate(bullet, muzzle.position, Camera.main.transform.rotation).transform;
            newBullet.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100);
            newBullet.Rotate(new Vector3(spreadX, spreadY, 0));
        }

    }

}
