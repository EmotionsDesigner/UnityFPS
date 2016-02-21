using UnityEngine;
using System.Collections;

public class BodyDamage : MonoBehaviour {
    public ZombieAI Zombie;
    public float damage;
     void OnCollisionEnter(Collision col)
    {

        if (col.transform.tag == "Bullet" && Zombie.Health>0)
        {
              Zombie.Health -= damage ;
              AudioSource a = GameObject.Find("Hit").GetComponent<AudioSource>();
              a.Play();

        }
          
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (Zombie.Health <= 0)
            //Destroy(gameObject);
	}
}
