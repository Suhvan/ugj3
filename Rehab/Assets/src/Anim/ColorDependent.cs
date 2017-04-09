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
			var newColor = new Color(ColorReference.Instance.RefColor.r, ColorReference.Instance.RefColor.g, ColorReference.Instance.RefColor.b, host.color.a);
			host.color = newColor;
        }
	}
}
