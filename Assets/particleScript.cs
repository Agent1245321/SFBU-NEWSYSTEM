using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class particleScript : MonoBehaviour
{
    private GameObject particles;
    private ParticleSystem particleSystem;

    private void Start()
    {
        particles = this.transform.root.Find("ParticleHit1").gameObject;
        particleSystem = particles.GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        particles.transform.position = collision.GetContact(0).point;
        particleSystem.Play();
    }
}
