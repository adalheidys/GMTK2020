using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;//the tilemap to be edited
    public Tilemap wallTilemap;
    public Tilemap objectsTilemap;

    public TileBase groundTile;
    public TileBase wallTile;
    public TileBase topTile;
    public TileBase randomWallTile;
    public TileBase torchTile;
    public GameObject lever;
    public GameObject[] hazards;
    public GameObject[] distractions;
    public GameObject[] locks;

    public Transform playerTransform;
    public int size;//the size of the map (its a square)
    public int deadEnds = 5;//how many dead ends to generate
    public int width = 2;
    public int wallFrequency;//how often to place a random wall thing
    public int numHazards;
    public float objectDistance;//the min distance of how far apart the objects are
    public String[] temptationTags = { "RogueTempt", "BardTempt", "WarriorTempt" };
    public String[] lockTags = { "RogueLock", "BardLock", "WarriorLock" };
    //important locations
    Vector2Int startPoint;
    Vector2Int endPoint;
    List<Vector2Int> filledPoints = new List<Vector2Int>();
    List<Vector2Int> objectPositions = new List<Vector2Int>();//where objects have been placed
    List<Vector2Int> deadEndList = new List<Vector2Int>();//where deadEnds have been placed

    // Start is called before the first frame update
    void Start()
    {       
        wallTilemap.BoxFill(new Vector3Int(0, 0, 0), topTile, 0, 0, size, size);//fill the world
        
        //finding start and end points
        float dist = 0;//distance between start and end point
        while (dist < size / 1.5f) {//find endpoints that are kinda far apart
            startPoint = new Vector2Int(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
            endPoint = new Vector2Int(UnityEngine.Random.Range(0, size - 1), UnityEngine.Random.Range(0, size - 1));
            dist = Vector2Int.Distance(startPoint, endPoint);
            objectPositions.Add(startPoint);
            objectPositions.Add(endPoint);
        }
        generatePath(startPoint, endPoint);//generate the path from the begining to end
        playerTransform.position = tilemap.CellToWorld(new Vector3Int(startPoint.x, startPoint.y, 0));
        playerTransform.position = new Vector3(playerTransform.position.x, 1, playerTransform.position.z);
        //Debug.Log(tilemap.CellToWorld(new Vector3Int(startPoint.x, startPoint.y, 0)));
       
        //placing side paths with dead ends
        for(int i = 0; i < deadEnds; i++)
        {
            Vector2Int end = new Vector2Int(UnityEngine.Random.Range(0, size - 1), UnityEngine.Random.Range(0, size - 1));
            deadEndList.Add(end);
            //Debug.Log(end);
            generatePath(filledPoints[UnityEngine.Random.Range(0,filledPoints.Count-1)], end);
        }

        //add edges
        int j = 0;
        foreach(Vector2Int point in filledPoints){//for every point
            if(! filledPoints.Contains(point + new Vector2Int(0, 1)))//if there is not a filled point directly above it
            {
                j++;
                if(j == wallFrequency)//if it hit the iteration, set it to a either a random tile or a torch
                {
                    j = 0;
                    TileBase tile = (UnityEngine.Random.Range(0, 2) >= 1) ? randomWallTile : torchTile;
                    tilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), wallTile);
                    objectsTilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), tile);                    
                    wallTilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), null);
                }
                else
                    tilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), wallTile);
                    wallTilemap.SetTile(new Vector3Int(point.x, point.y + 1, 0), null);
            }
        }

        //add switches/locks
        for (int z = 0; z < 3; z++)
        {
            bool placed = false;//while it isn't placed
            while (!placed)
            {
                Vector2Int tryPosition = deadEndList[z];//pick a random tile
                objectPositions.Add(tryPosition);
                Vector3 worldPos = tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y, 0));
                //Debug.Log(tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y, 0)));
                //place the lever
                GameObject lev = Instantiate(lever, worldPos, Quaternion.identity);
                lev.tag = lockTags[z];
                //get the hazard
                GameObject barrier = locks[z];
                if (filledPoints.Contains(tryPosition + new Vector2Int(0, 1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y + 1, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(1, 1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x + 1, tryPosition.y + 1, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(1, 0)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x + 1, tryPosition.y, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(1, -1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x + 1, tryPosition.y - 1, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(0, -1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y - 1, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(-1, -1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x - 1, tryPosition.y - 1, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(-1, 0)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x - 1, tryPosition.y, 0)), Quaternion.identity, lev.transform);
                if (filledPoints.Contains(tryPosition + new Vector2Int(-1, 1)))
                    Instantiate(barrier, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x - 1, tryPosition.y + 1, 0)), Quaternion.identity, lev.transform);
                placed = true;
                //}
            }
        }

        //add hazards/distractions
        for (int s = 0; s < numHazards;s++)
        {
            bool placed = false;//while it isn't placed
            while (!placed)
            {
                Vector2Int tryPosition = filledPoints[UnityEngine.Random.Range(0, filledPoints.Count - 1)];//pick a random tile
                bool isValid = true;
                foreach(Vector2Int pos in objectPositions)
                {
                    if(Vector2.Distance(pos,tryPosition) < objectDistance)
                    {
                        isValid = false;
                    }
                }
                if (isValid && !isAccessable(tryPosition))//if it's accessable
                {
                    Vector3 worldPos = tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y, 0));
                    objectPositions.Add(tryPosition);
                    //place the distraciton
                    GameObject distraction = distractions[UnityEngine.Random.Range(0, distractions.Length - 1)];
                    Instantiate(distraction, worldPos, Quaternion.identity);
                    
                    //get the hazard
                    GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length - 1)];
                    if (filledPoints.Contains(tryPosition + new Vector2Int(0, 1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y+1, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(1, 1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x+1, tryPosition.y + 1, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(1, 0)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x+1, tryPosition.y, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(1, -1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x+1, tryPosition.y - 1, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(0, -1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x, tryPosition.y -1, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(-1, -1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x-1, tryPosition.y - 1, 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(-1, 0)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x-1, tryPosition.y , 0)), Quaternion.identity, distraction.transform);
                    if (filledPoints.Contains(tryPosition + new Vector2Int(-1, 1)))
                        Instantiate(hazard, tilemap.GetCellCenterWorld(new Vector3Int(tryPosition.x-1, tryPosition.y + 1, 0)), Quaternion.identity, distraction.transform);
                    placed = true;
                }
            }
        }
        


        //add exit door
    }

    private bool isAccessable(Vector2Int tryPosition)
    {
        if(tilemap.GetTile(new Vector3Int(tryPosition.x+1, tryPosition.y, 0)) != null && tilemap.GetTile(new Vector3Int(tryPosition.x-1, tryPosition.y, 0)) != null)
        {
            if (tilemap.GetTile(new Vector3Int(tryPosition.x , tryPosition.y+1, 0)) != null && tilemap.GetTile(new Vector3Int(tryPosition.x, tryPosition.y-1, 0)) != null)
            {
                return true;
            }
        }
        return false;
    }

    void generatePath(Vector2Int start, Vector2Int end)
    {
        float dist = Vector2Int.Distance(start, end);
        float initDistance = dist;//set the initial distance, so the path wont ever go longer than that
        Vector2Int currPoint = start;//the current point in the path
        bool complete = false;
        int z = 0;//to hold the iteration number, so the max distance can decrease gradually
        while (!complete && z < 900)//while you havent gotten to the end point
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

                    wallTilemap.SetTile(new Vector3Int(tryPoint.x, tryPoint.y, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y + i, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x + i, tryPoint.y - i, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x, tryPoint.y + i, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x, tryPoint.y - i, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y - i, 0), null);//fill the grid
                    wallTilemap.SetTile(new Vector3Int(tryPoint.x - i, tryPoint.y + i, 0), null);//fill the grid

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
