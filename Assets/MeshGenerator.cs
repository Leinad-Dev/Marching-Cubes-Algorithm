using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;
    public void GenerateMesh(int[,]map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
    }

    private void OnDrawGizmos()
    {
        //Draw cube visualization for Nodes and ControlNodes
        if (squareGrid != null)
        {
            for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
            {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
                {
                    Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white; //true = black, false = white
                    Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white; //true = black, false = white
                    Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomLeft.active) ? Color.black : Color.white; //true = black, false = white
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomLeft.position, Vector3.one * .4f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomRight.active) ? Color.black : Color.white; //true = black, false = white
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomRight.position, Vector3.one * .4f);


                    Gizmos.color = Color.grey;
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerTop.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerRight.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerBottom.position, Vector3.one * .15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerLeft.position, Vector3.one * .15f);
                    
                }
            }
        }
    }

    public class SquareGrid
    {
        public Square[,] squares;
        public SquareGrid(int[,] map,float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x=0; x < nodeCountX; x++) //create a grid of control nodes
            {
                for(int y =0; y< nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);// ==1 checks for whether it's a wall
                }
            }


            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX-1; x++) //create a grid squares out of controls nodes
            {
                for (int y = 0; y < nodeCountY-1; y++)
                {
                    //x   = left
                    //x+1 = right
                    //y   = bottom
                    //y+1 = top

                    // [tL]----------[tR]
                    //   |            |
                    //   |            |
                    //   |            |
                    //   |            |
                    // [bL]----------[bR]

                    //[ControlNode] topLeft = x, y+1
                    //[ControlNode] topRight = x+1, y+1
                    //[ControlNode] bottomRight = x+1, y
                    //[ControlNode] bottomLeft = x, y

                    squares[x, y] = new Square(controlNodes[x, y+1], controlNodes[x+1, y+1], controlNodes[x+1, y], controlNodes[x, y+1]);
                        
                }
            }
        }
    }
    public class Square
    {
        public ControlNode topLeft, topRight, bottomRight, bottomLeft;
        public Node centerTop, centerRight, centerBottom, centerLeft;

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

        }
    }
    public class Node
    {
        public Vector3 position;
        public int vertexIndex = -1; //set to default -1 since we don't know what it's going to be

        public Node(Vector3 _pos) //[1] base contructor
        {
            position = _pos;
        }
    }


    public class ControlNode : Node //inherites from Node
    {
        public bool active;
        public Node above, right;
        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) //[1] _pos is set from the base construction
        {
            active = _active;
            above = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);

        }
    }
}

