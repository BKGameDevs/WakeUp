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

    public bool GoToOriginalWhenInactive;
    public bool ActiveOnStart;
    public bool InactiveOnEnd;
    public bool ParentPlayer = true;

    public float moveSpeed; // for fast the object moves

    private Vector3 currentTarget; // the current point it's going to, start or end?
    private bool _IsActive;

    // Use this for initialization
    void Start()
    {
        currentTarget = endPoint.position; // start currTarget as endpoint
        //objectToMove.name = gameObject.name;
        SetIsActive(ActiveOnStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToMove == null)
            return;

        var objectPosition = objectToMove.transform.position;
        var move = _IsActive || (!_IsActive && GoToOriginalWhenInactive && objectPosition != startPoint.position);

        if (move)
        { //while there is something to move

            //move towards endpoint
            objectPosition = 
                objectToMove.transform.position = Vector3.MoveTowards(objectPosition, currentTarget, moveSpeed * Time.deltaTime);

            //switch the current target
            if (objectPosition == endPoint.position)
            {
                if (InactiveOnEnd)
                {
                    SetIsActive(false);
                    return;
                }

                currentTarget = startPoint.position;
            }

            if (objectPosition == startPoint.position)
            {
                currentTarget = endPoint.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ParentPlayer)
            return;

        var gameObject = collision.gameObject;
        if (gameObject.CompareTag("Player"))
        {
            var playerController = gameObject.GetComponent<PlayerController>();
            playerController?.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.CompareTag("Player"))
        {
            var playerController = gameObject.GetComponent<PlayerController>();
            playerController?.SetParent(null);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //}

    public void SetIsActive(bool isActive)
    {
        _IsActive = isActive;
    }

    public void ResetPosition(float delay)
    {
        this.StartTimedAction(null,
            () =>
            {
                SetIsActive(false);
                objectToMove.transform.position = startPoint.position;
            },
            delay);

    }
}