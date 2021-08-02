using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//REQUIRED:
// 1. GridPoint script
// 2. a grid of GridPoint
// 3. define Points.A - H before calling any of these methods

#region --- helpers ---
public class GridPoint
{
    private Vector3 _position = Vector3.zero;
    private bool _on = false;

    public Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = new Vector3(value.x, value.y, value.z);
        }
    }

    public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            _on = value;
        }
    }

    public override string ToString()  //override default tostring method
    {
        return string.Format("{0} {1}", Position, On); //replace {0} with Position, replace {1} with On;
    }
}

public static class Points
{
    /*      E ------ F
     *      |        |
     *      | A ------- B
     *      | |      |  |
     *      G | ---- H  |
     *        |         |
     *        C ------- D
    */

    // CORNERS
    public static GridPoint A = null;
    public static GridPoint B = null;
    public static GridPoint C = null;
    public static GridPoint D = null;
    public static GridPoint E = null;
    public static GridPoint F = null;
    public static GridPoint G = null;
    public static GridPoint H = null;

    //C point is always our origin/start point when marching to the next cell.
    //*that is why we return C.position + (desired point) to account for the offset.

    //example: Cube1 point E = { (0,0,0) + (1,1,1) }
    //         Cube2 moved forward on the x axis to get point E = { (1,0,0) + (1,1,1) }
    //         Cube3 etc...

    // HALF-WAY POINTS
    public static Vector3 AB
    {
        get { return C.Position + new Vector3(0.5f, 1f, 0f); }
    }
    public static Vector3 BA
    {
        get { return C.Position + new Vector3(0.5f, 1f, 0f); }
    }
    public static Vector3 BD
    {
        get { return C.Position + new Vector3(1f, 0.5f, 0f); }
    }
    public static Vector3 DB
    {
        get { return C.Position + new Vector3(1f, 0.5f, 0f); }
    }
    public static Vector3 DC
    {
        get { return C.Position + new Vector3(0.5f, 0f, 0f); }
    }
    public static Vector3 CD
    {
        get { return C.Position + new Vector3(0.5f, 0f, 0f); }
    }
    public static Vector3 CA
    {
        get { return C.Position + new Vector3(0f, 0.5f, 0f); }
    }
    public static Vector3 AC
    {
        get { return C.Position + new Vector3(0f, 0.5f, 0f); }
    }

    public static Vector3 EF
    {
        get { return C.Position + new Vector3(0.5f, 1f, 1f); }
    }
    public static Vector3 FE
    {
        get { return C.Position + new Vector3(0.5f, 1f, 1f); }
    }
    public static Vector3 FH
    {
        get { return C.Position + new Vector3(1f, 0.5f, 1f); }
    }
    public static Vector3 HF
    {
        get { return C.Position + new Vector3(1f, 0.5f, 1f); }
    }
    public static Vector3 HG
    {
        get { return C.Position + new Vector3(0.5f, 0f, 1f); }
    }
    public static Vector3 GH
    {
        get { return C.Position + new Vector3(0.5f, 0f, 1f); }
    }
    public static Vector3 GE
    {
        get { return C.Position + new Vector3(0f, 0.5f, 1f); }
    }
    public static Vector3 EG
    {
        get { return C.Position + new Vector3(0f, 0.5f, 1f); }
    }

    public static Vector3 FB
    {
        get { return C.Position + new Vector3(1f, 1f, 0.5f); }
    }
    public static Vector3 BF
    {
        get { return C.Position + new Vector3(1f, 1f, 0.5f); }
    }
    public static Vector3 AE
    {
        get { return C.Position + new Vector3(0f, 1f, 0.5f); }
    }
    public static Vector3 EA
    {
        get { return C.Position + new Vector3(0f, 1f, 0.5f); }
    }
    public static Vector3 HD
    {
        get { return C.Position + new Vector3(1f, 0f, 0.5f); }
    }
    public static Vector3 DH
    {
        get { return C.Position + new Vector3(1f, 0f, 0.5f); }
    }
    public static Vector3 CG
    {
        get { return C.Position + new Vector3(0f, 0f, 0.5f); }
    }
    public static Vector3 GC
    {
        get { return C.Position + new Vector3(0f, 0f, 0.5f); }
    }
}


public static class Bits
{
    //Here we check to see which corner is ON or OFF
    public static int A = (int)Mathf.Pow(2, 0); //1
    public static int B = (int)Mathf.Pow(2, 1); //2
    public static int C = (int)Mathf.Pow(2, 2); //4
    public static int D = (int)Mathf.Pow(2, 3); //8
    public static int E = (int)Mathf.Pow(2, 4); //16
    public static int F = (int)Mathf.Pow(2, 5); //32
    public static int G = (int)Mathf.Pow(2, 6); //64
    public static int H = (int)Mathf.Pow(2, 7); //128

 

    //this function converts our config number into a readable string format that we can output in string format.
    public static string BinaryFormat(int config)
    {
        //The << operator shifts its left-hand operand left by the number of bits defined by its right-hand operand.

        //[128],[64],[32],[16],[8],[4],[2],[1]

        //Notes: (#)^(#) can never be 0, it would be like dividing by 0. That's why we make sure != 0 in all strings

        string A = ((config & (1 << 0)) != 0) ? "A" : "-"; //is (config && [1]) != 0
        string B = ((config & (1 << 1)) != 0) ? "B" : "-"; //is (config && [2]) != 0
        string C = ((config & (1 << 2)) != 0) ? "C" : "-"; //is (config && [4]) != 0
        string D = ((config & (1 << 3)) != 0) ? "D" : "-"; //is (config && [8]) != 0
        string E = ((config & (1 << 4)) != 0) ? "E" : "-";
        string F = ((config & (1 << 5)) != 0) ? "F" : "-";
        string G = ((config & (1 << 6)) != 0) ? "G" : "-";
        string H = ((config & (1 << 7)) != 0) ? "H" : "-";

        return H + G + F + E + D + C + B + A;
    }

    public static bool isBitSet(int config, string letter) //is a specific bit (letter) is active/set in the config number.
    {
        bool ret = false;

        switch (letter)
        {
            case "A":
                ret = ((config & (1 << 0)) != 0);
                break;
            case "B":
                ret = ((config & (1 << 1)) != 0);
                break;
            case "C":
                ret = ((config & (1 << 2)) != 0);
                break;
            case "D":
                ret = ((config & (1 << 3)) != 0);
                break;
            case "E":
                ret = ((config & (1 << 4)) != 0);
                break;
            case "F":
                ret = ((config & (1 << 5)) != 0);
                break;
            case "G":
                ret = ((config & (1 << 6)) != 0);
                break;
            case "H":
                ret = ((config & (1 << 7)) != 0);
                break;
        }

        return ret;
    }
    public static class UVCoord
    {
        /*  A ------ B
            |        |
            |        |
            C ------ D  */
        public static Vector2 A = new Vector2(0, 1);
        public static Vector2 B = new Vector2(1, 1);
        public static Vector2 C = new Vector2(0, 0);
        public static Vector2 D = new Vector2(1, 0);
    }
}
#endregion
