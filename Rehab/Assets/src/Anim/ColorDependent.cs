using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDependent : MonoBehaviour {

	private SpriteRenderer host;

	// Use this for initialization
	void Start () {
		host = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (host != null && ColorReference.Instance != null)
		{
			host.color = ColorReference.Instance.RefColor;
        }
	}
}
