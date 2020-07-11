using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;//the tilemap to be edited

    public TileBase groundTile;
    public TileBase wallTile;
    public TileBase topTile;
    public Transform playerTransform;
    public int size;//the size of the map (its a square)
    public int deadEnds = 5;//how many dead ends to generate
    public int width = 2;

    //important locations
    Vector2Int startPoint;
    Vector2Int endPoint;
    List<Vector2Int> filledPoints = new List<Vector2Int>();
    // Start is called before the first frame update
    void Start()
    {
        tilemap.BoxFill(new Vector3Int(0, 0, 0), topTile, 0, 0, size, size);
          //finding start and end points
        float dist = 0;//distance between start and end point
        while (dist < size / 1.5f) {//find endpoints that are kinda far apart
            startPoint = new Vector2Int(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
            endPoint = new Vector2Int(UnityEngine.Random.Range(0, size - 1), UnityEngine.Random.Range(0, size - 1));
            dist = Vector2Int.Distance(startPoint, endPoint);
        }
        generatePath(startPoint, endPoint);//generate the path from the begining to end
        playerTransform.position = tilemap.CellToWorld(new Vector3Int(startPoint.x, startPoint.y, 0));
        
        //placing side paths with dead ends
        for(int i = 0; i < deadEnds; i++)
        {
            generatePath(filledPoints[UnityEngine.Random.Range(0,filledPoints.Count-1)], new Vector2Int(UnityEngine.Random.Range(0, size - 1), UnityEngine.Random.Range(0, size - 1)));
        }

        //add edges
        foreach(Vector2Int point in filledPoints){//for every point
            if(! filledPoints.Contains(point + new Vector2Int(0, 1)))//if there is not a filled point directly above it
            {
                tilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), wallTile);
            }
        }
         
    }

    void generatePath(Vector2Int start, Vector2Int end)
    {
        float dist = Vector2Int.Distance(start, end);
        float initDistance = dist;//set the initial distance, so the path wont ever go longer than that
        Vector2Int currPoint = start;//the current point in the path
        bool complete = false;
        int z = 0;//to hold the iteration number, so the max distance can decrease gradually
        while (!complete && z < 200)//while you havent gotten to the end point
        {
            Vector2Int tryPoint = genPoint(currPoint);
            dist = Vector2Int.Distance(tryPoint, end);
            if (tryPoint.Equals(end)) { complete = true; }
            if (tryPoint.x >= 0 && tryPoint.x <= size - 1 && tryPoint.y > 0 && tryPoint.y <= size - 1)
            {
                if (dist < initDistance)//if you decreased distance
                {
                    initDistance = dist;//the initial distance is now tht current
                    for (int i = 0; i < width; i++) { 
                    tilemap.SetTile(new Vector3Int(tryPoint.x, tryPoint.y, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y+i, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y-i, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x , tryPoint.y+i, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x , tryPoint.y-i, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y-i, 0), groundTile);//fill the grid
                    tilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y+i, 0), groundTile);//fill the grid
                    
                    currPoint = tryPoint;
                    //add it to the list of filled points                       
                    filledPoints.Add(currPoint + new Vector2Int(0,0));
                    filledPoints.Add(currPoint + new Vector2Int(1,0));
                    filledPoints.Add(currPoint + new Vector2Int(1,1));
                    filledPoints.Add(currPoint + new Vector2Int(1,-1));
                    filledPoints.Add(currPoint + new Vector2Int(0,1));
                    filledPoints.Add(currPoint + new Vector2Int(0,-1));
                    filledPoints.Add(currPoint + new Vector2Int(-1,0));
                    filledPoints.Add(currPoint + new Vector2Int(-1,-1));
                    filledPoints.Add(currPoint + new Vector2Int(-1,1));
                }
                }
            }
            z++;
        }
    }//end generate path

    private Vector2Int genPoint(Vector2Int currPoint)//takes a point and
    {
        if (UnityEngine.Random.Range(0f, 2f) > 1f)
        {
            return currPoint + new Vector2Int(UnityEngine.Random.Range(-1, 2), 0);
        }
        else
        {
            return currPoint + new Vector2Int(0, UnityEngine.Random.Range(-1, 2));
        }
    }
}
