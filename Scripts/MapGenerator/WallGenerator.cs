using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GridGenerator gridGenerator;

    private int maxW;
    private int maxH;

    // Start is called before the first frame update
    void Start()
    {
        BuildWalls();
    }

    void BuildWalls()
    {
        for(int x = 0; x < maxW; x++)
        {
            for (int y = 0; y < maxH; y++)
            {
                if (x == 0 || x == maxW-1 || y == 0 || y == maxH - 1)
                {

                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {


    }
}
