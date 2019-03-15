using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConrtoller : MonoBehaviour
{
    public int health = 1;
	
	void Update ()
    {
        if(health <= 0 )
        {
            Destroy(gameObject);
        }		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            minusHealth();
        }
    }
    void minusHealth()
    {
        health--;
    }
}
