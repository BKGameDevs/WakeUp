using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseWallController : GameEventListener
{
    // Start is called before the first frame update

    private Tilemap FalseWall;

    void Start()
    {
        FalseWall = GetComponent<Tilemap>();
        FalseWall.CompressBounds();
        Response.AddListener(DisableWall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableWall(){
        // gameObject.SetActive(false);
        var bounds = FalseWall.cellBounds;
        TileBase[] tileArray = FalseWall.GetTilesBlock(FalseWall.cellBounds);

        foreach (var pos in FalseWall.cellBounds.allPositionsWithin){
            StartCoroutine(WaitToRemoveTile());
            FalseWall.SetTile(new Vector3Int(pos.x, pos.y, pos.z), null);
        }
    }

    private IEnumerator WaitToRemoveTile(){
        yield return new WaitForSeconds(1f);
    }
}
