using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMeshes : MonoBehaviour
{
    public static CornerMeshes instance;
    public GameObject mesh;

    private Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();


    private void Awake()
    {
        if (instance == null) //no instance of level generator in game.
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject); //already exists in scene, destroy this copy.
        }

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        foreach(Transform c in mesh.transform)
        {
            meshes.Add(c.name, c.GetComponent<MeshFilter>().sharedMesh);
        }
    }

    public Mesh GetCornerMesh(int bitmask, int floorLevel)
    {
        Mesh meshToReturn;

        if(floorLevel > 1)
        {
            if(meshes.TryGetValue("_" + bitmask.ToString(), out meshToReturn))
            {
                return meshToReturn;
            }
        }
        else if (floorLevel == 0)
        {
            if (meshes.TryGetValue("_" + 0 + "_" + bitmask.ToString(), out meshToReturn))
            {
                return meshToReturn;
            }
        }
        else if (floorLevel == 1)
        {
            if (meshes.TryGetValue("_" + 1 + "_" + bitmask.ToString(), out meshToReturn))
            {
                return meshToReturn;
            }
        }

        return null; //if we get here, it means we failed to return in any of the above statements/conditionals
    }


}

