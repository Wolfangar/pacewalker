using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaminaManager : MonoBehaviour {
    public int totalHealth;
    public float decreasingSpeed;
    [HideInInspector]
    public float currentHealth;
    //GameObject GameManager;
    Animator anim;
    NavMeshAgent navMesh;
	public AudioClip deathsound;
	AudioSource src;
    [HideInInspector]
    public Counter counter;

    [HideInInspector]
    public bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        currentHealth = totalHealth;
		src = GetComponent<AudioSource>();
		//GameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        currentHealth -= decreasingSpeed * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, totalHealth);
        checkHealth();
    }

    private void checkHealth()
    {
        if ((int) currentHealth <= 0.0f)
            dead();
    }

    public void dead()
    {
        if (isDead)
            return;
		src.PlayOneShot(deathsound);
		isDead = true;
        Debug.Log(transform.name + " is dead, not a big surprise");
        anim.SetBool("dead", isDead);
        if(navMesh)
        {
            counter.killedcounter += 1;
            navMesh.isStopped = true;
            navMesh.velocity = Vector3.zero;
            navMesh.enabled = false;
        }
        

        //Destroy(gameObject);
        //Time.timeScale = 0;
    }

    public void destroyIt()
    {
        Destroy(this.gameObject);
    }

    public void loseHealth(float damage)
    {
		//src.PlayOneShot(hitsound);
        float newHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp(newHealth, -0, totalHealth);
        checkHealth();
    }

    public void getHealth(float health)
    {
        float newHealth = currentHealth + health;
        //currentHealth += newHealth > totalHealth ? totalHealth : newHealth;
        currentHealth = Mathf.Clamp(newHealth, 0.0f, totalHealth);
    }

    /*
    public int segments, bars; //number of segment and "under"-segment: bar
    public int healthPerBar;
    public float decreasingSpeed;

    private int currentSegment, currentBar;
    private float totalHealth, currentHealth;

	// Use this for initialization
	void Start () {
        totalHealth = currentHealth = segments * bars * healthPerBar;
	}
	
	// Update is called once per frame
	void Update () {
        currentHealth -= decreasingSpeed * Time.deltaTime;
        checkHealth();


        currentSegment = (int) (currentHealth / (bars * healthPerBar) - 1) + 1;
        (posX / sizeX) * sizeX


        currentBar = (int)currentHealth / (segments * bars);
    }

    void checkHealth()
    {
        if (totalHealth <= 0)
        {
            dead();
        }
    }

    void dead()
    {
        Debug.Log("dead, not a big surprise");
    }

    public void loseBar()
    {
        //currentHealth -= 
        checkHealth();
    }

    public void loseSegment()
    {
        checkHealth();
    }

    public void getHealth(float health)
    {
        totalHealth += health;
    }
    */
}
