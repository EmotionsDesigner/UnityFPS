using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    public PlayerController Player;
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //animacja strzelania
	if (Player.alreadyShooting)
        animator.SetBool("Shoot", true);
    else
        animator.SetBool("Shoot", false);
	}
}
