using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Generator
{
    public class GenerateLevelController
    {
        private const int CountWall = 4;
        
        private Tile _tileGround;
        private Tilemap _tileMapGround;
        private int _wightMap;
        private int _heightMap;
        
        private int[,] _map;
        private int _factorSmooth;
        private int _randomFillPercent;

        public GenerateLevelController(GenerateLevelView view)
        {
            _tileMapGround = view.TileMapGround;
            _tileGround = view.TileGround;
            WightMap = view.WightMap;
            HeightMap = view.HeightMap;
            FactorSmooth = view.FactorSmooth;
            RandomFillPercent = view.RandomFillPercent;

            _map = new int[WightMap, HeightMap];
        }

        public int WightMap
        {
            get => _wightMap;
            set
            {
                _wightMap = value;
                _map = new int[WightMap, HeightMap];
            }
        }

        public int HeightMap
        {
            get => _heightMap;
            set
            {
                _heightMap = value;
                _map = new int[WightMap, HeightMap];
            }
        }

        public int FactorSmooth
        {
            get => _factorSmooth;
            set
            {
                _factorSmooth = value;
                _map = new int[WightMap, HeightMap];
            }
        }

        public int RandomFillPercent
        {
            get => _randomFillPercent;
            set
            {
                _randomFillPercent = value;
                _map = new int[WightMap, HeightMap];
            }
        }

        public void Awake()
        {
            ClearTileMap();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            RandomFillLevel();

            for (var i = 0; i < FactorSmooth; i++)
            {
                SmoothMap();
            }

            DrawTilesOnMap();
        }

        

        private void RandomFillLevel()
        {
            var seed = Time.time.ToString();
            var psevdoRandom = new System.Random(seed.GetHashCode());

            for (var x = 0; x < WightMap; x++)
            {
                for (var y = 0; y < HeightMap; y++)
                {
                    if (x == 0 || x == WightMap - 1 || y == 0 || y == HeightMap - 1)
                        _map[x, y] = 1;
                    else
                        _map[x, y] = (psevdoRandom.Next(0, 100) < RandomFillPercent) ? 1 : 0;
                }
            }
        }
        
        private void SmoothMap()
         {
             for (var x = 0; x < WightMap; x++)
             {
                 for (var y = 0; y < HeightMap; y++)
                 {
                     var neighbourWallTiles = GetNeighbourWall(x, y);

                     if (neighbourWallTiles > CountWall)
                         _map[x, y] = 1;
                     else if (neighbourWallTiles < CountWall)
                         _map[x, y] = 0;
                 }
             }
         }

        private int GetNeighbourWall(int x, int y)
        {
            var wallCount = 0;
            
            for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
            {
                for (var neighbourY = y - 1;neighbourY <= y + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < WightMap && neighbourY >= 0 && neighbourY < HeightMap)
                    {
                        if (neighbourX != x || neighbourY != y)
                        {
                            wallCount += _map[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                     
                }
            }
            return wallCount;
        }
        
        private void DrawTilesOnMap()
         {
             if(_map == null)
                 return;
             
             for (var x = 0; x < WightMap; x++)
             {
                 for (var y = 0; y < HeightMap; y++)
                 {
                     var positionTile = new Vector3Int(-WightMap / 2 + x, -HeightMap / 2 + y, 0);
                     
                     if(_map[x,y] == 1)
                         _tileMapGround.SetTile(positionTile, _tileGround);
                 }
             }
         }

        public void ClearTileMap()
        {
            if( _tileMapGround != null)
                _tileMapGround.ClearAllTiles();
        }
    }
}