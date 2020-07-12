using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHandler : MonoBehaviour
{
    public NavMeshSurface meshSurface; 

    bool completed = false;//so it only bakes once

    private void LateUpdate()
    {
        if (!completed)
        {
            completed = true;
            meshSurface.BuildNavMesh();
        }
    }
}
