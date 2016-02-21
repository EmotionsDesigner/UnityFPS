using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool alreadyShooting = false;
    public float shootingDelay=0.3f;
    public GameObject ammo;
    public GameObject weapon;
    IEnumerator Shoot()
        {
               
                alreadyShooting = true;
                AudioSource Shoot = GameObject.Find("GunShot").GetComponent<AudioSource>();
                Shoot.Play();          
                //utworzenie pocisku
                GameObject Bullet = (GameObject)Instantiate(ammo, ammo.transform.position, ammo.transform.rotation);
                //wystrzał
                //odmrożenie pozycji pocisku
                Bullet.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.None;
                Bullet.GetComponent<Rigidbody>().AddForce(ammo.transform.up*4000);

                yield return new WaitForSeconds(shootingDelay);
                alreadyShooting = false;
        }

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void FixedUpdate () {
        Cursor.visible = false;  
        //strzał
        if (Input.GetButton("Fire1") && alreadyShooting == false)
            StartCoroutine(Shoot());
     
	
	}
}
