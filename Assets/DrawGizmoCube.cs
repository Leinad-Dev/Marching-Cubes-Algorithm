using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class DrawGizmoCube : MonoBehaviour
{


    public Vector3 cubeGridSize;

    public GameObject node_topBackLeft = null,
           node_topBackRight = null,
           node_topFronRight = null,
           node_topFrontLeft = null,
           node_botBackLeft = null,
           node_botBackRight = null,
           node_botFrontRight = null,
           node_botFrontLeft = null;

    public GameObject topBackMP = null, topRightMP = null, topFrontMP = null, topLeftMP = null, //MP = MidPoint
                   botBackMP = null, botRightMP = null, botFrontMP = null, botLeftMP = null; //MP = MidPoint

    /*
        Vector3[] CreateACube()
        {
            Vector3[] vertices =
            {
                new Vector3 (0,0,0), //v1
                new Vector3 (1,0,0), //v2
                new Vector3 (1,1,0), //v3
                new Vector3 (0,1,0), //v4
                new Vector3 (0,1,1), //v5
                new Vector3 (1,1,1), //v6
                new Vector3 (1,0,1), //v7
                new Vector3 (0,0,1), //v8
            };

            return vertices;
        }*/

    public static void AssignLabel(GameObject g)
    {
        Texture2D tex = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;
        Type editorGUIUtilityType = typeof(EditorGUIUtility);
        BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        object[] args = new object[] { g, tex };
        editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
    }

    private void OnDrawGizmos()
    {
        if (node_topBackLeft != null)
        {
            AssignLabel(node_topBackLeft);
            AssignLabel(node_topBackRight);
            AssignLabel(node_topFronRight);
            AssignLabel(node_topFrontLeft);
            AssignLabel(node_botBackLeft);
            AssignLabel(node_botBackRight);
            AssignLabel(node_botFrontRight);
            AssignLabel(node_botFrontLeft);
            AssignLabel(topBackMP);
            AssignLabel(topRightMP);
            AssignLabel(topFrontMP);
            AssignLabel(topLeftMP);
            AssignLabel(botBackMP);
            AssignLabel(botRightMP);
            AssignLabel(botFrontMP);
            AssignLabel(botLeftMP);
            AssignLabel(node_topBackLeft);
        }





        /*        if (cubeGridSize != null)
                {
                    Vector3 pos = new Vector3 (transform.position.x, transform.position.y + (cubeGridSize.y/2), transform.position.z);

                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(pos, cubeGridSize);



                    for (int x=0; x< cubeGridSize.x; x++)
                    {
                        for (int y = 0; y < cubeGridSize.y; y++)
                        {
                            for (int z = 0; z < cubeGridSize.z; z++)
                            {
                                if (x == 0)
                                {
                                    Gizmos.color = Color.red;
                                    Gizmos.DrawWireCube(new Vector3((x - (cubeGridSize.x / 2) + .5f), y+.5f, z-(cubeGridSize.z/2)+.5f), Vector3.one);
                                    Debug.Log("cube x,y,z : " + "[" + x + "]" + "[" + y + "]"+ "[" + z + "]");

                                }
                            }
                        }
                    }
                }*/
    }
    private void Start()
    {
        CubeGrid();
    }
    public void CubeGrid()
    {

        Vector3 pointSize = new Vector3(.10f, .10f, .10f);
        Vector3 centerPointSize = new Vector3(.05f, .05f, .05f);

                /*
           p5 +--------+ p6
             /|       /|
            / |      / |
        p4 +--------+p3|
           |  |     |  |
           |p8+-----|--+ p7
           | /      | /
           |/       |/
         p1+--------+ p2

        */




//create main cube points/verts
        node_topBackLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_topBackLeft.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        node_topBackLeft.transform.position = new Vector3(0, 1, 1);
        node_topBackLeft.transform.localScale = pointSize;
        node_topBackLeft.name = "node_topBackLeft";

        node_topBackRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_topBackRight.transform.position = new Vector3(1, 1, 1);
        node_topBackRight.transform.localScale = pointSize;
        node_topBackRight.name = "node_topBackRight";

        node_topFronRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_topFronRight.transform.position = new Vector3(1, 1, 0);
        node_topFronRight.transform.localScale = pointSize;
        node_topFronRight.name = "node_topFronRight";

        node_topFrontLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_topFrontLeft.transform.position = new Vector3(0, 1, 0);
        node_topFrontLeft.transform.localScale = pointSize;
        node_topFrontLeft.name = "node_topFrontLeft";

        node_botBackLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_botBackLeft.transform.position = new Vector3(0, 0, 1);
        node_botBackLeft.transform.localScale = pointSize;
        node_botBackLeft.name = "node_botBackLeft";

        node_botBackRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_botBackRight.transform.position = new Vector3(1, 0, 1);
        node_botBackRight.transform.localScale = pointSize;
        node_botBackRight.name = "node_botBackRight";

        node_botFrontRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_botFrontRight.transform.position = new Vector3(1, 0, 0);
        node_botFrontRight.transform.localScale = pointSize;
        node_botFrontRight.name = "node_botFrontRight";

        node_botFrontLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        node_botFrontLeft.transform.position = new Vector3(0, 0, 0);
        node_botFrontLeft.transform.localScale = pointSize;
        node_botFrontLeft.name = "node_botFrontLeft";


        //create center points
        topBackMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topBackMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        topBackMP.transform.position = new Vector3(.5f, 1, 1);
        topBackMP.transform.localScale = centerPointSize;
        topBackMP.name = "topBackMP";

        topRightMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topRightMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        topRightMP.transform.position = new Vector3(1, 1, .5f);
        topRightMP.transform.localScale = centerPointSize;
        topRightMP.name = "topRightMP";

        topFrontMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topFrontMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        topFrontMP.transform.position = new Vector3(.5f, 1, 0);
        topFrontMP.transform.localScale = centerPointSize;
        topFrontMP.name = "topFrontMP";

        topLeftMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topLeftMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        topLeftMP.transform.position = new Vector3(0, 1, .5f);
        topLeftMP.transform.localScale = centerPointSize;
        topLeftMP.name = "topLeftMP";


        botBackMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        botBackMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        botBackMP.transform.position = new Vector3(.5f, 0, 1);
        botBackMP.transform.localScale = centerPointSize;
        botBackMP.name = "botBackMP";

        botRightMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        botRightMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        botRightMP.transform.position = new Vector3(1, 0, .5f);
        botRightMP.transform.localScale = centerPointSize;
        botRightMP.name = "botRightMP";

        botFrontMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        botFrontMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        botFrontMP.transform.position = new Vector3(.5f, 0, 0);
        botFrontMP.transform.localScale = centerPointSize;
        botFrontMP.name = "botFrontMP";

        botLeftMP = GameObject.CreatePrimitive(PrimitiveType.Cube);
        botLeftMP.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        botLeftMP.transform.position = new Vector3(0, 0, .5f);
        botLeftMP.transform.localScale = centerPointSize;
        botLeftMP.name = "botLeftMP";

    }

    public class Node
    {
        public Vector3 position;
        public int vertexIndex;

        public Node(Vector3 _pos) //[1] base/parent class/contructor
        {
            position = _pos;
        }
    }

    public class ControlNode : Node //inherites from Node
    {
        public bool active;
        public Node above, right;
        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) //base(_pos) specifies to use the function in our parent/base that requires the parameter (_pos).
        {
            active = _active;
            above = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);

        }
    }


    
    private void CreateCube()
    {
    /*
      v5 +--------+ v6
        /|       /|
       / |      / |
   v4 +--------+v3|
      |  |     |  |
      |v8+-----|--+ v7
      | /      | /
      |/       |/
    v1+--------+ v2
   */



/*    Vector3[] vertices =
        {
            new Vector3 (0,0,0), //v1
            new Vector3 (1,0,0), //v2
            new Vector3 (1,1,0), //v3
            new Vector3 (0,1,0), //v4
            new Vector3 (0,1,1), //v5
            new Vector3 (1,1,1), //v6
            new Vector3 (1,0,1), //v7
            new Vector3 (0,0,1), //v8
        };

        v1 = vertices[0];
        v2 = vertices[1];
        v3 = vertices[2];
        v4 = vertices[3];
        v5 = vertices[4];
        v6 = vertices[5];
        v7 = vertices[6];
        v8 = vertices[7];*/





        /*        public ControlNode topBackLeft, topBackRight, topFrontRight, topFrontLeft; //A,B,C,D
                public ControlNode botBackLeft, botBackRight, botFrontRight, botFrontLeft; //E,F,G,H
                public Node topCenterBack, topCenterRight, topCenterFront, topCenterLeft;
                public Node botCenterBack, botCenterRight, botCenterFront, botCenterLeft;*/
        /*
             
         A +--------+ B
          /|       /|
         / |      / |
      D +--------+ C|
        |  |     |  |
        |E +-----|--+ F
        | /      | /
        |/       |/
      H +--------+ G

        */

        /*
                public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft)
                {

                    //       (cT)
                    // [tL]----*----[tR]
                    //  -            -  
                    //  -            -  
                    //  *(cL)        *(cR)  
                    //  -            -  
                    //  -            -  
                    // [bL]----*----[bR]
                    //       (cB)

                    topLeft = _topLeft;
                    topRight = _topRight;
                    bottomRight = _bottomRight;
                    bottomLeft = _bottomLeft;

                    centerTop = topLeft.right;
                    centerRight = bottomRight.above;
                    centerBottom = bottomLeft.right;
                    centerLeft = bottomLeft.above;

                }*/
    }
}
