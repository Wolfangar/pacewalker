using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxBlood : MonoBehaviour {
    SpriteRenderer sprite;
    public float fadeSpeed = 2.0f;
    public float wait = 2.0f;
    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(over());
	}

    IEnumerator over()
    {
        yield return new WaitForSeconds(wait);

        for(float alpha = 1.0f; alpha > 0f; alpha -= Time.deltaTime * fadeSpeed)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
