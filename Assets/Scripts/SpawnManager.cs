using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject spawnBad;
    public GameObject spawnGood;

    public GameObject hero;

    public GameObject templateBad;

    private List<GameObject> listSpawnBad = new List<GameObject>();
    private List<GameObject> listSpawnGood = new List<GameObject>();
    private float currentCountBad;

    public float startCountBad = 5.0f;
    public float startCountBadSpeed = 0.2f;
    public float countBadMin = 2.0f;

    // Use this for initialization
    void Start () {
        foreach (Transform child in spawnBad.transform)
            listSpawnBad.Add(child.gameObject);

        foreach (Transform child in spawnGood.transform)
            listSpawnGood.Add(child.gameObject);

        currentCountBad = startCountBad;

        StartCoroutine(spawnBadThings());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator spawnBadThings()
    {
        yield return new WaitForSeconds(currentCountBad);

        while (true)
        {
            List<GameObject> tempList = new List<GameObject>();
            foreach(GameObject spawn in listSpawnBad)
            {
                if (Vector3.SqrMagnitude(spawn.transform.position - hero.transform.position) > 15*15)
                {
                    tempList.Add(spawn);
                }
            }

            if(tempList.Count == 0)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            int numberBad = tempList.Count;
            int indexSelected = Random.Range(0, numberBad);

            Instantiate(templateBad).transform.position = tempList[indexSelected].transform.position;

            currentCountBad -= startCountBadSpeed;

            currentCountBad = Mathf.Clamp(startCountBad, countBadMin, startCountBad);

            yield return new WaitForSeconds(currentCountBad);
        }
    }
}
