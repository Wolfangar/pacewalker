using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject spawnBad;
    public GameObject spawnGood;

    public GameObject hero;

    public GameObject templateBad;
    public GameObject templateGood;

    private List<GameObject> listSpawnBad = new List<GameObject>();
    private List<GameObject> listSpawnGood = new List<GameObject>();
    private float currentCountBad;

    public float startCountBad = 5.0f;
    public float startCountBadSpeed = 0.2f;
    public float countBadMin = 2.0f;

    public float spawnBadDistance = 10;

    private List<bool> availablePileSpawns = new List<bool>();

    // Use this for initialization
    void Start () {
        foreach (Transform child in spawnBad.transform)
            listSpawnBad.Add(child.gameObject);

        foreach (Transform child in spawnGood.transform)
        {
            listSpawnGood.Add(child.gameObject);
            availablePileSpawns.Add(true);
        }
            
        currentCountBad = startCountBad;

        StartCoroutine(spawnBadThings());
        StartCoroutine(spawnGoodThings());
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pilePicked(int index)
    {
        availablePileSpawns[index] = true;
    }

    IEnumerator spawnGoodThings()
    {
        yield return new WaitForSeconds(3.0f);

        while (true)
        {
            List<GameObject> tempList = new List<GameObject>();
            int i = 0;
            foreach (GameObject spawn in listSpawnGood)
            {
                if (availablePileSpawns[i])
                {
                    tempList.Add(spawn);
                    Debug.Log("pile; " + spawn.transform.name);
                }
                i++;
            }

            if (tempList.Count == 0)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            int numberGood = tempList.Count;
            int indexSelected = Random.Range(0, numberGood);
            
            availablePileSpawns[listSpawnGood.IndexOf(tempList[indexSelected])] = false;

            int type = Random.Range(0, 2);

            //Init + instanciate
            GameObject pile = Instantiate(templateGood);
            pile.tag = type == 0 ? "petite_pile" : "grosse_pile";

            pile.transform.position = tempList[indexSelected].transform.position;
            Pile pileScript = pile.GetComponent<Pile>();
            pileScript.indexSpawn = listSpawnGood.IndexOf(tempList[indexSelected]);
            pileScript.spawnManager = this;
            

            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator spawnBadThings()
    {
        yield return new WaitForSeconds(currentCountBad);

        while (true)
        {
            List<GameObject> tempList = new List<GameObject>();
            foreach(GameObject spawn in listSpawnBad)
            {
                if (Vector3.SqrMagnitude(spawn.transform.position - hero.transform.position) > spawnBadDistance * spawnBadDistance)
                {
                    tempList.Add(spawn);
                    Debug.Log("spawn; " + spawn.transform.name);
                }
            }

            if(tempList.Count == 0)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            int numberBad = tempList.Count;
            int indexSelected = Random.Range(0, numberBad);

            GameObject spawned = Instantiate(templateBad);
            spawned.transform.position = tempList[indexSelected].transform.position;
            spawned.GetComponent<Enemy>().hero = hero;

            currentCountBad -= startCountBadSpeed;

            currentCountBad = Mathf.Clamp(startCountBad, countBadMin, startCountBad);

            yield return new WaitForSeconds(currentCountBad);
        }
    }
}
