using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject spawnBad;
    public GameObject spawnGood;

    public GameObject hero;

    //Same order (please)
    public GameObject[] templateBad;
    public float[] templateBadProba;
    public GameObject[] templateGood;
    public float[] templateGoodProba;

    private List<GameObject> listSpawnBad = new List<GameObject>();
    private List<GameObject> listSpawnGood = new List<GameObject>();
    private float currentCountBad;

    public float startCountBad = 5.0f;//time between spawn
    public float startCountBadSpeed = 0.2f;//decreasing speed
    public float countBadMin = 2.0f;//min time between spawn

    public float spawnBadDistance = 12.0f;//spawn distance to instanciate enemy

    //
    public int enemyPerWave = 3;
    public int enemyPerWaveIncreasing = 1;

    public float timeBetweenWave = 5.0f;
    public float timeBetweenWaveDecreasing = 0.3f;
    public float timeBetweenWaveMin = 2.0f;
    //

    public Counter counter;

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
        currentTimeBetweenWave = timeBetweenWave;

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
            //Spawn
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

            //Proba good
            int indexGood = Choose(templateGoodProba);//0 et 1 possible en result
            
            //Init + instanciate
            GameObject pile = Instantiate(templateGood[indexGood]);
            //pile.tag = indexGood == 0 ? "petite_pile" : "grosse_pile";

            pile.transform.position = tempList[indexSelected].transform.position;
            Pile pileScript = pile.GetComponent<Pile>();
            pileScript.indexSpawn = listSpawnGood.IndexOf(tempList[indexSelected]);
            pileScript.spawnManager = this;
            

            yield return new WaitForSeconds(3.0f);
        }
    }

    private int currentEnemySpawned = 0;
    private float currentTimeBetweenWave;

    [HideInInspector]
    public int numberWave = 0;

    IEnumerator spawnBadThings()
    {
        yield return new WaitForSeconds(currentTimeBetweenWave);
        Debug.Log("wave " + numberWave + " starto");

        while (true)
        {
            //Spawn
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

            //Index of the enemy
            int indexEnemy = Choose(templateBadProba);

            GameObject spawned = Instantiate(templateBad[indexEnemy]);
            spawned.transform.position = tempList[indexSelected].transform.position;
            spawned.GetComponent<Enemy>().hero = hero;
            spawned.GetComponent<StaminaManager>().counter = counter;

            currentCountBad -= startCountBadSpeed;

            currentCountBad = Mathf.Clamp(startCountBad, countBadMin, startCountBad);

            currentEnemySpawned++;
            if(currentEnemySpawned >= enemyPerWave)//Wave finished
            {
                Debug.Log("wave "+ numberWave + " finished");
                numberWave++;

                enemyPerWave += enemyPerWaveIncreasing;
                Debug.Log("enemyPerWave " + enemyPerWave);
                currentEnemySpawned = 0;

                currentTimeBetweenWave -= timeBetweenWaveDecreasing;
                currentTimeBetweenWave = Mathf.Clamp(currentTimeBetweenWave, timeBetweenWaveMin, timeBetweenWave);
                Debug.Log("currentTimeBetweenWave " + currentTimeBetweenWave);

                yield return new WaitForSeconds(currentTimeBetweenWave);
            }
            else
            {
                yield return new WaitForSeconds(currentCountBad);
            }
        }
    }

    //Choosing Items with Different Probabilities, return index chosen
    private int Choose(float[] probs)
    {
        float total = 0;
        foreach (float elem in probs)
        {
            total += elem;
        }
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
