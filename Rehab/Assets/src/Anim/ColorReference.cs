using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ColorReference : MonoBehaviour
{	
	SpriteRenderer refSprite;

	public static ColorReference Instance { get; private set; }

	public Color RefColor {
		get
		{
			if(refSprite!=null)
				return refSprite.color;
			return Color.white;
		}
	}

	void Start()
	{
		refSprite = gameObject.GetComponent<SpriteRenderer>();
		Instance = this;
	}

}

