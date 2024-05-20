using UnityEngine;
using UnityEngine.UIElements;

public class MusicHandler : MonoBehaviour {
	private static MusicHandler instance = null;
	private AudioSource audioSource;
	public float Volume = 0.5f;

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

		GetComponent<UIDocument>().rootVisualElement.Q<Slider>().dataSource = this;
	}

	private void Start() {
		if (instance == this) {
			audioSource.Play();
		}
	}

    private void Update()
    {
		audioSource.volume = Volume;
    }
}