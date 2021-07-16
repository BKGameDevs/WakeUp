using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseWallController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool DisableOnStart = false;
    public bool PlaySoundOnColumn;
    public float DestroyTileDelay = 0.125f;
    public bool DestroyLine;

    private Tilemap FalseWall;
    private AudioSource SoundEffect;

    private BoundsInt.PositionEnumerator _CellBounds;
    private TileBase _BaseTile;

    void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
        FalseWall = GetComponent<Tilemap>();
        FalseWall.CompressBounds();

        _CellBounds = FalseWall.cellBounds.allPositionsWithin;
        if (DisableOnStart)
        {
            foreach (var pos in _CellBounds)
            {
                if (_BaseTile == null)
                    _BaseTile = FalseWall.GetTile(pos);

                FalseWall.SetTile(pos, null);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWall(object value)
    {
        //var bounds = FalseWall.cellBounds;
        //TileBase[] tileArray = FalseWall.GetTilesBlock(FalseWall.cellBounds);

        StartCoroutine(WaitToRemoveTile());
    }

    private IEnumerator WaitToRemoveTile()
    {
        //var count = 0;
        int? last = null;

        foreach (var pos in _CellBounds)
        {
            var current = PlaySoundOnColumn ? pos.x : pos.y;
            if (last != current)
            {
                last = current;
                SoundEffect.Play();
            }

            FalseWall.SetTile(pos, null);
            //count++;
            yield return new WaitForSeconds(DestroyTileDelay);
        }

    }
    public void EnableWall(object value)
    {
        //var bounds = FalseWall.cellBounds;
        //TileBase[] tileArray = FalseWall.GetTilesBlock(FalseWall.cellBounds);

        StartCoroutine(WaitToEnableTile());
    }

    private IEnumerator WaitToEnableTile()
    {
        //var count = 0;
        int? last = null;

        foreach (var pos in _CellBounds)
        {
            var current = PlaySoundOnColumn ? pos.x : pos.y;
            if (last != current)
            {
                last = current;
                SoundEffect.Play();
            }

            FalseWall.SetTile(pos, _BaseTile);
            //count++;
            yield return new WaitForSeconds(DestroyTileDelay);
        }

    }
}
