using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour {
    [HideInInspector]
    public int indexSpawn;
    [HideInInspector]
    public SpawnManager spawnManager;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        //collision.gameobject.getComponent<StaminaManager>().getHealth(stamina);

        Debug.Log("pile picked");
        spawnManager.pilePicked(indexSpawn);
        //Destroy(this.gameObject);
    }
}
