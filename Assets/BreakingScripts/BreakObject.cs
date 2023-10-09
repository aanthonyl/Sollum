using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script offers a public method as well as trigger functionality for breaking an object.
// When broken, the object will be destroyed, play optional SFX, and emit some particles.
// Attach this script to the object that can be broken. It must have a SpriteRenderer.

public class BreakObject : MonoBehaviour
{
    [Tooltip("SFX to play on break. Can be left null.")]
    public AudioSource breakAudio;

    [Header("Particle settings")]
    public int numOfParticles = 5;
    public float startLifetime = 0.8f;
    public float minStartSize = 0.4f;
    public float maxStartSize = 0.8f;
    public float minRotation = -45;
    public float maxRotation = 45;

    [Header("Trigger settings")]
    [Tooltip("By default, breaking can only be triggered by calling the public method Break(). Check to enable break on trigger.")]
    public bool breakOnTrigger = false;
    [Tooltip("When checked, OnTriggerEnter will check if the tag matches any of the tags below before breaking the object. Break On Trigger is required for this to work.")]
    public bool requireTriggerTag = false;
    public string[] tags;

    void OnTriggerEnter(Collider other)
    {
        if (breakOnTrigger)
        {
            if (requireTriggerTag) // Check if trigger matches specified tags before breaking
            {
                foreach (string tag in tags)
                {
                    if (other.tag == tag)
                    {
                        Break();
                        break;
                    }
                }
            }
            else // Break object without checking tags
            {
                Break();
            }
        }
    }

    public void Break()
    {
        // Play break SFX if present
        if (breakAudio != null)
        {
            breakAudio.Play();
        }

        // Get sprite before destroying so it can be attached to particle system
        Texture2D texture = GetComponent<SpriteRenderer>().sprite.texture;

        // Destroy all components except the transform, any audio source, and this monobehaviour
        foreach (Component component in GetComponents(typeof(Component)))
        {
            if (component != this && component != transform && component != breakAudio)
            {
                Destroy(component);
            }
        }

        for (int i = 0; i < numOfParticles; i++)
        {
            // Create material for particles based on random portion of sprite
            int particleWidth = texture.width / 4;
            int particleHeight = texture.height / 4;

            int randomX = texture.width - Random.Range(1, 4) * particleWidth;
            int randomY = texture.height - Random.Range(1, 4) * particleHeight;

            Color[] randomPixels = texture.GetPixels(randomX, randomY, particleWidth, particleHeight);
            Texture2D randomTexture = new Texture2D(particleWidth, particleHeight);

            randomTexture.SetPixels(randomPixels);
            randomTexture.Apply();

            Material particleMat = new Material(Shader.Find("Particles/Standard Unlit"));
            particleMat.mainTexture = randomTexture;

            // Start particles
            GameObject child = new GameObject("Break particles", typeof(ParticleSystem));
            child.transform.parent = transform;
            child.transform.Rotate(new Vector3(0, 0, 45));

            ParticleSystem partSys = child.GetComponent<ParticleSystem>();
            ParticleSystemRenderer partRenderer = child.GetComponent<ParticleSystemRenderer>();

            var main = partSys.main;
            var shape = partSys.shape;
            var rotationOverLifetime = partSys.rotationOverLifetime;

            main.startLifetime = startLifetime;
            main.startSize = new ParticleSystem.MinMaxCurve(minStartSize, maxStartSize);
            main.startRotation = new ParticleSystem.MinMaxCurve(-45, 45);
            main.gravityModifier = 2;

            shape.shapeType = ParticleSystemShapeType.Circle;
            shape.arc = 90;

            rotationOverLifetime.enabled = true;
            rotationOverLifetime.z = new ParticleSystem.MinMaxCurve(minRotation, maxRotation);

            partRenderer.material = particleMat;
            partSys.Stop();
            partSys.Emit(1);
        }

        // Destroy object
        Destroy(gameObject, 1);
    }
}
