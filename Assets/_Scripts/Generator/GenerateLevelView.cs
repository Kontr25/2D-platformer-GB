using _Scripts.Generator;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevelView : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMapGround;
    [SerializeField] private Tile _tileGround;
    private int _wightMap;
    private int _heightMap;
    private int _factorSmooth;
    private GenerateLevelController _generateLevelController;
    [SerializeField, Range(0,100)] private int _randomFillPercent;

    public Tilemap TileMapGround => _tileMapGround;
    public int WightMap
    {
        get => _wightMap;
        set => _wightMap = value;
    }

    public Tile TileGround => _tileGround;
    public int HeightMap
    {
        get => _heightMap;
        set => _heightMap = value;
    }

    public int FactorSmooth
    {
        get => _factorSmooth;
        set => _factorSmooth = value;
    }

    public int RandomFillPercent => _randomFillPercent;

    public GenerateLevelController LevelController
    {
        get => _generateLevelController;
        set => _generateLevelController = value;
    }
}
