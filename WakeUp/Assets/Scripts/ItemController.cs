using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float ItemValue;
    public FloatVariable ItemPickUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if (ItemPickUp != null)
                ItemPickUp.RuntimeValue = ItemValue;
            
            Destroy(gameObject);
        }
    }
}
