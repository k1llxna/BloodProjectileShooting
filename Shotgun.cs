using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    override public void Shoot()
    {
        ammo--;
        animator.SetTrigger("Recoil");
        FindObjectOfType<AudioManager>().Play("Gunshot");

        RaycastHit hit;

        for (int i = 0; i < 8; i++)
        {

            float spreadX = Random.Range(-5f, 5f);
            float spreadY = Random.Range(-5f, 5f);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Transform newBullet = Instantiate(bullet, muzzle.position, transform.rotation).transform;
                newBullet.LookAt(hit.point);
                newBullet.Rotate(new Vector3(spreadX, spreadY, 0));

            }
            else
            {
                Transform newBullet = Instantiate(bullet, muzzle.position, Camera.main.transform.rotation).transform;
                newBullet.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100);
                newBullet.Rotate(new Vector3(spreadX, spreadY, 0));
            }
        }
        
    }
}
