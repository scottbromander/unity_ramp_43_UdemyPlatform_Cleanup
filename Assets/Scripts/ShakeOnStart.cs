using UnityEngine;
using System.Collections;

public class ShakeOnStart : MonoBehaviour {

	public float screenShakeAmount;
	private CameraController cameraController;

	// Use this for initialization
	void Start () {
		cameraController = FindObjectOfType<CameraController> ();
		cameraController.ScreenShake (screenShakeAmount);
	}
}
