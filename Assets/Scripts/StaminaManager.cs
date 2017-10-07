using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour {
    public int totalHealth, hitDamage, dashStamina;
    public float decreasingSpeed, currentHealth;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    private void Update()
    {
        currentHealth -= decreasingSpeed * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, totalHealth);
        checkHealth();
    }

    private void checkHealth()
    {
        if (currentHealth <= 0.0f)
            dead();
    }

    public void dead()
    {
        Debug.Log(transform.name + " is dead, not a big surprise");
        Destroy(this);
        //Time.timeScale = 0;
    }

    public void loseHealth(float damage)
    {
        float newHealth = currentHealth - damage;
        currentHealth = Mathf.Clamp(newHealth, 0.0f, totalHealth);
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
