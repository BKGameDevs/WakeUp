using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _DoorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _DoorAnimator = GetComponent<Animator>();
    }

    public void InvokeOpenDoor(object value)
    {
        var isOpen = (bool)value;

        if (!isOpen && GameManager.Instance.KeyObtained)
            StartCoroutine(OpenDoor());
    }

    public IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.25f);

        _DoorAnimator.SetTrigger("Open");
    }
}
