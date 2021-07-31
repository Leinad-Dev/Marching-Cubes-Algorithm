using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public string seed;
    public bool useRandomSeed;
    [Range(0,100)]
    public int randomFillPercent;
    int[,] map;



    private void Start()
    {
        GenerateMap();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }



    private void GenerateMap()
    {
        map = new int[width,height];
        RandomFillMap();
        
        for (int i = 0; i < 5; i++)//smooth map
        {
            SmoothMap();
        }
    }

    void RandomFillMap()
    {
        if(useRandomSeed)
        {
            seed = Time.time.ToString();
        }
            

        System.Random rnd = new System.Random(seed.GetHashCode());

        for (int x=0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) //set borders to walls
                {
                    map[x, y] = 1; //this sets map[] to a wall 
                }
                else
                {
                    map[x, y] = (rnd.Next(0, 100) < randomFillPercent) ? 1 : 0; //true is 1 (wall)
                }
            }        
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroudingWallCount(x, y);

                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1; //set this tile to a wall
                }  
                else if (neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
                    
            }
    }

    int GetSurroudingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX -1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)//[1] make sure you're inside of the map
                {
                    if (neighbourX != gridX || neighbourY != gridY)//not looking at original tile passed into this function
                    {
                        wallCount += map[neighbourX, neighbourY]; //if map[] is a wall then map[x,y] == 1. Else map[x,y] == 0.
                    }
                }

                else
                {
                    wallCount++; //this will encourage the growth of walls around the edge of the map
                }
            }
        }

        return wallCount;
    }


    void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width/2 + x + .5f, 0, -height/2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }

}
