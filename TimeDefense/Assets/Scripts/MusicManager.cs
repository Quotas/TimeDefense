using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip music;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(this);

		gameObject.GetComponent<AudioSource> ().PlayOneShot(music);

	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponent<AudioSource> ().isPlaying == false)
		{
			gameObject.GetComponent<AudioSource> ().PlayOneShot(music);
		}
	}
}
