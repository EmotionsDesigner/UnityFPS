using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {
    public float AttackRange;
    public float Health;
    public GameObject target;
    public float TargetDistance;
    public float speed;
    float approximation=1f;
    Vector3 TargetPosition;
    Animator animator;
    bool attackPlaying = false;
    //w momencie śmierci przyjmuje wartość true
    bool deathMoment=false;
    bool attack = false;




    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.transform.name);
    }
    struct direction
    {
        public bool x;
        public bool y;
        public bool z;
      public void setX(){
          x=true;
          y=false;
          z=false;
      }
        public void setY(){
          x=false;
          y=true;
          z=false;
      }
        public void setZ(){
          x=false;
          y=false;
          z=true;
      }
    };

    //zwraca wartość, do którego dana współrzędna powinna dążyć
    float ChooseDirection(direction dir)
    {
        if (dir.x)
        {
            if (transform.position.x+approximation < TargetPosition.x)
                return speed;
            else if (transform.position.x-approximation > TargetPosition.x)
                return -speed;
            else
                return 0;
        }
       if (dir.y)
        {
            if (transform.position.y+approximation < TargetPosition.y)
                return speed;
            else if (transform.position.y-approximation > TargetPosition.y)
                return -speed;
            else return 0;
        }
        if (dir.z)
        {
            if (transform.position.z +approximation< TargetPosition.z)
                return speed;
            else if (transform.position.z-approximation > TargetPosition.z)
                return -speed;
            else return 0;
        }
       return 0f;//zła wartość (tylko x,y)
    }

    IEnumerator AudioPlay(string name, float delay)
    {

        attackPlaying = true;
        AudioSource audio = GameObject.Find(name).GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(delay);
        attackPlaying = false;
    }
    IEnumerator HuntTarget()
    {
         //odwrócenie w strone gracza
        transform.LookAt(target.transform);
        yield return new WaitForSeconds(0.1f);
        direction dir = new direction();
        //ustalenie, kierunku poruszania się
         float dirX,dirY,dirZ;
         dir.setX();
         dirX = ChooseDirection(dir);
         dir.setY();
         dirY = ChooseDirection(dir);
         dir.setZ();
         dirZ = ChooseDirection(dir);
         float distanceX= Mathf.Abs(transform.position.x-target.transform.position.x);
         float distanceZ = Mathf.Abs(transform.position.z-target.transform.position.z);
          if (distanceX>distanceZ)
              dirZ/=(distanceX/distanceZ);
          else
              dirX/=(distanceZ/distanceX);
        //poruszanie 
         transform.Translate(dirX, 0, dirZ, Space.World);
          }
    

	// Use this for initialization
	void Start () {
        animator = GetComponent < Animator>();
	}
	
	// Update is called once per frame
	void Update () {
       
        //uaktualnienie punktów w życia w kontrolerze animacji
        animator.SetFloat("Health", Health);
         animator.SetBool("Attack", attack);
        //uaktualnienie odległości od gracza
        TargetPosition = target.transform.position;
        TargetDistance = Vector3.Distance(transform.position, target.transform.position);

        //moment śmierci
        if (Health <= 0 && deathMoment==false)
        {
            deathMoment = true;
            //zwłoki trzeba przesunąć trochę nad poziom podłoża, żeby było je widać (?)
            transform.Translate(0, 0.3f, 0, Space.World);
            AudioPlay("ZombieDead", 0);
        }
        //pościg/atak gracza
        if (TargetDistance < AttackRange && Health > 0)
        {
            //atak
            if (TargetDistance <= 2){
                //odwrócenie w strone gracza
                  transform.LookAt(target.transform);
                  attack = true;
                if (attackPlaying==false)
                  StartCoroutine(AudioPlay("ZombieAttack", 2f));
            }         
            //pościg
            else
            {
                attack = false;
                StartCoroutine(HuntTarget());
            }
    
        }
           
	}
}
