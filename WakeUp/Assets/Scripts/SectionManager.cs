using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerController PlayerController;
    public Transform PlayerSpawn;

    [Header("Pick Up")]
    public GameObject PickUpPrefab;
    public Transform[] PickUpPositions;

    public int Order;

    private static int _Current;
    private static List<SectionManager> _Managers;

    public static event EventHandler PlayerSpawned;

    static SectionManager()
    {
        _Current = -1;
        _Managers = new List<SectionManager>();
    }

    public static void Next()
    {
        _Current++;
        var manager = _Managers.ElementAtOrDefault(_Current);
        if (manager != null)
            manager.SpawnPlayer();
    }
    
    //private PlayerController _PlayerController;
    private List<GameObject> _PickUps;

    private void Awake()
    {
        _Managers.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //_PlayerController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

        _PickUps = new List<GameObject>();

        foreach (var transform in PickUpPositions)
            transform.gameObject.SetActive(false);

        SpawnPickups();

        if (_Current < 0)
        {
            _Managers = _Managers.OrderBy(x => x.Order).ToList();
            Next();
        }
    }

    public void SpawnPlayer()
    {
        if (PlayerController == null)
            return;
        if (_Current > -1)
        {
            if (_Current == 0)
                SetupPlayer();
            else
            {
                CrossFadeController.Instance.RunCrossFade(() =>
                {
                    SetupPlayer();
                    DisablePreviousSection();
                });
            }
        }
    }

    private void DisablePreviousSection()
    {
        var manager = _Managers.ElementAtOrDefault(_Current - 1);
        manager?.gameObject.SetActive(false);
    }

    private void SetupPlayer()
    {
        PlayerController.ResetSanity();
        PlayerController.ResetPlayer(PlayerSpawn.position, false);
        PlayerSpawned?.Invoke(this, EventArgs.Empty);
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
