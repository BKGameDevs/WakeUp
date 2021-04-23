using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public GameObject PickUpPrefab;
    public Transform[] PickUpPositions;

    private List<GameObject> _PickUps;
    // Start is called before the first frame update
    void Start()
    {
        _PickUps = new List<GameObject>();

        foreach (var transform in PickUpPositions)
            transform.gameObject.SetActive(false);

        SpawnPickups();
    }

    public void SpawnPickups()
    {
        foreach (var pickUp in _PickUps)
            if (pickUp != null)
                Destroy(pickUp);

        _PickUps.Clear();

        foreach (var transform in PickUpPositions)
            _PickUps.Add(
                    Instantiate(PickUpPrefab, transform.position, Quaternion.identity, this.transform.parent)
                );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
