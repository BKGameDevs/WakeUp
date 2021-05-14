using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseWallController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool PlaySoundOnColumn;

    private Tilemap FalseWall;
    private AudioSource SoundEffect;

    void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
        FalseWall = GetComponent<Tilemap>();
        FalseWall.CompressBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWall(object value)
    {
        var bounds = FalseWall.cellBounds;
        TileBase[] tileArray = FalseWall.GetTilesBlock(FalseWall.cellBounds);

        StartCoroutine(WaitToRemoveTile());
    }

    private IEnumerator WaitToRemoveTile()
    {
        //var count = 0;
        int? last = null;
        var cellBounds = FalseWall.cellBounds.allPositionsWithin;
        foreach (var pos in cellBounds){
            var current = PlaySoundOnColumn ? pos.x : pos.y;
            if (last != current)
            {
                last = current;
                SoundEffect.Play();
            }

            FalseWall.SetTile(new Vector3Int(pos.x, pos.y, pos.z), null);
            //count++;
            yield return new WaitForSeconds(.125f);
        } 

    }
}
