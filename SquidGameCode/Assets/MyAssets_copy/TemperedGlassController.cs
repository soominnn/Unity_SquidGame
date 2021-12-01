using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TemperedGlassController : MonoBehaviour
{ 

    [Space]
    public AudioClip tempered_sound; //sound source: 직접녹음

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision collision)
    {
        //구슬과 충돌할 경우에만 사운드 재생
        if (collision.collider.CompareTag("stone"))
        {
            GetComponent<AudioSource>().clip = tempered_sound;
            GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
    }
}
