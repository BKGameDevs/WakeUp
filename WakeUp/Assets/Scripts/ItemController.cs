using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float ItemValue;
    // Start is called before the first frame update

    public GameEvent ItemGameEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if(ItemGameEvent != null){
                ItemGameEvent.Raise(ItemValue);
            }

            Destroy(gameObject);
        }
    }
}
