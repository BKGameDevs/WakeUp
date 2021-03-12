using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseWallController : GameEventListener
{
    // Start is called before the first frame update

    private Tilemap FalseWall;
    private AudioSource SoundEffect;
    public int rowCount = 5;

    void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
        FalseWall = GetComponent<Tilemap>();
        FalseWall.CompressBounds();
        Response.AddListener(DisableWall);
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
        var count = 0;
        var cellBounds = FalseWall.cellBounds.allPositionsWithin;
        foreach (var pos in cellBounds){
            FalseWall.SetTile(new Vector3Int(pos.x, pos.y, pos.z), null);
            if (count % rowCount == 0) {
                SoundEffect.Play();
            }
            count++;
            yield return new WaitForSeconds(.125f);
        } 

    }
}
