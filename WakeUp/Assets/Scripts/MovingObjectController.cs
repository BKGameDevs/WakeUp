using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Allows an object to move back and forth between 2 specified points
 * 
*/
public class MovingObjectController : MonoBehaviour
{

    public GameObject objectToMove; // object to be moved

    //points that object will move between back and forth
    public Transform startPoint; // starting point
    public Transform endPoint; // ending point

    public float moveSpeed; // for fast the object moves

    private Vector3 currentTarget; // the current point it's going to, start or end?
    private bool _IsActive;

    // Use this for initialization
    void Start()
    {
        currentTarget = endPoint.position; // start currTarget as endpoint
        objectToMove.name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToMove != null && _IsActive)
        { //while there is something to move

            //move towards endpoint
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

            //switch the current target
            if (objectToMove.transform.position == endPoint.position)
            {
                currentTarget = startPoint.position;
            }

            if (objectToMove.transform.position == startPoint.position)
            {
                currentTarget = endPoint.position;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //set the flag
            other.gameObject.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.parent = null;
        }
    }

    public void SetIsActive(bool isActive)
    {
        _IsActive = isActive;
    }
}