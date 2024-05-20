using System.Collections;
using UnityEngine;

public class EnemyDying : MonoBehaviour {
	private SpriteRenderer renderer;

	private void Awake() {
		renderer = GetComponent<SpriteRenderer>();
	}

	public void Die() {
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut() {
		for (var ft = 1f; ft >= 0; ft -= 0.05f) {
			var c = renderer.material.color;
			c.a = ft;
			renderer.material.color = c;
			yield return new WaitForSeconds(0.05f);
		}
	}
}