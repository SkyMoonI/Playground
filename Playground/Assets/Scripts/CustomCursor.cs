using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
	RectTransform rectTransform;
	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 mousePosition = Input.mousePosition;

		rectTransform.position = mousePosition;
	}
}
