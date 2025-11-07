using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlePrefab;
    
    [Header("Sounds")]
    public AudioSource audioSourcePrefab;
    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        OnCollect();
        gameObject.SetActive(false);
    }
    
    protected virtual void OnCollect()
    {
        if (audioSourcePrefab != null)
        {
            AudioSource audioSource = Instantiate(audioSourcePrefab);
            audioSource.Play();
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
        if (particlePrefab != null)
        {
            ParticleSystem particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}
