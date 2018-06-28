using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class WallTile : Tile {

    [SerializeField] private Sprite m_BaseSprite;
    [SerializeField] private Sprite m_Preview;
    [SerializeField] private LayerMask m_LayerMask;

#if UNITY_EDITOR

    [MenuItem("Assets/Create/Tiles/WallTile")]
    public static void CreateWallTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save WallTile", "New WallTile", "asset", "Save WallTile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
    }

#endif

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

}
