using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLightController : MonoBehaviour
{

    private GameObject _PlayerLight;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerLight = GameObject.Find("Player Light");
        _PlayerLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            _PlayerLight.SetActive(true);    
            
            Destroy(gameObject);
        }
    }
}
