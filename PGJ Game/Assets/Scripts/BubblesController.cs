using UnityEngine;

public class BubblesController : MonoBehaviour
{
    public ParticleSystem chargingBubbles;
    public ParticleSystem boostBubbles;
    public Rigidbody2D rb;

    void Start()
    {
        var emission = chargingBubbles.emission;
        emission.enabled = false;
        emission = boostBubbles.emission;
        emission.enabled = false;
    }

    void Update()
    {
        var emission = chargingBubbles.emission;

        if (Input.GetMouseButtonDown(0))
        {
            emission.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            emission.enabled = false;
        }

        emission = boostBubbles.emission;

        if (rb.velocity.magnitude > 1)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }
}
