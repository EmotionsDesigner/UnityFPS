using UnityEngine;
using System.Collections;

public class BulletCleaner : MonoBehaviour {
    public GameObject ShootMark;

    void OnCollisionEnter(Collision col)
    {
     
       
        if (name != "BulletBase")
        {
           
            
            

            //jeżeli kula trafi w martwego już wroga(leżącego na ziemi, to nie zostaje zniszczona)
            Debug.Log(col.transform.tag);
            if (col.transform.tag == "Enemy")
            {
                  ZombieAI Zombie = col.gameObject.GetComponentInParent<ZombieAI>();
                  if (Zombie.Health <= 0)
                      return;
            }
            else
            {
                 Debug.Log(col.transform.name);
            }
              
            //ślad po strzale
            Quaternion rotation = new Quaternion(0,0,0,0);
            Instantiate(ShootMark, transform.position,rotation);
            //usunięcie pocisku
          Destroy(gameObject);
        }
         

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
