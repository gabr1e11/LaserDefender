using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/* Adjust orthographic size of the camera so each pixel on the background sprite
		 * is rendered to an integer number of pixels at the current screen resolution,
		 * thus maintaining the pixel perfect feeling */
		float backgroundHeight = 720.0f * 2.0f;

		float ratio = Mathf.Floor (Screen.height / backgroundHeight);
		if (ratio > -float.Epsilon && ratio < float.Epsilon)
			ratio = 1.0f;

        Camera.main.orthographicSize = Screen.height / ratio / 2.0f;
    }
}
