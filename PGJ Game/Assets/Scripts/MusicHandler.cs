using UnityEngine;

public class MusicHandler : MonoBehaviour {
	private static MusicHandler instance = null;
	private AudioSource audioSource;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
	}

	private void Start() {
		if (instance == this) {
			audioSource.Play();
		}
	}
}