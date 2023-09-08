using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTarget : OnDestroyFunction
{
    [SerializeField] private AudioClip fallHitSound;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void OnCollisionEnter(Collision other)
    {
        bool _other = other.collider.CompareTag("Bullet");
        if (!_other)
        {
            audioSource.PlayOneShot(fallHitSound);
            Destroy(this.gameObject, 1);
        }
    }

    public override void Destroy()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
