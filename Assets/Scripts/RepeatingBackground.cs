using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private float colliderLength;

	// Use this for initialization
	void Start ()
    {
        colliderLength = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x <= -colliderLength)
            RepositionBackground();
	}

    private void RepositionBackground()
    {
        transform.position = new Vector3(gameObject.transform.position.x + 2 * colliderLength, transform.position.y, 0f);
    }
}
