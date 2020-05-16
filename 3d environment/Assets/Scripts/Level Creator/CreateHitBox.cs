using UnityEngine;

/// <summary>
/// contains only the calculate mesh function, when invoked it will given with a start and end point spit out a usable mesh object (note the mesh is not world coordinates)
/// </summary>
public class CreateHitBox 
{
    /// <summary>
    /// give two Vector3 positions and a mesh and it will generate all the details needed for the mesh, returning it back
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="hitBoxMesh"></param>
    /// <returns></returns>
    public static Mesh calculateMesh(Vector3 startPos, Vector3 endPos, Mesh hitBoxMesh)
    {
        // the height and width is set to whatever the last position is
        float height = endPos.y;
        float width = endPos.x;
        float endZ = endPos.z;

        float startX = startPos.x;
        float startY = startPos.y;
        float startZ = startPos.z;
        //  in order to draw a plane we need 4 vertices. with 4 verticies we can make two right handed triangles and it is with those two right handed triangles we can draw a plane
        int[] triangles;
        Vector3[] vertices;
        // uvs are coordinates that allow the renderer to apply textures to an object. Since UVs follow the same logic of backwards culling as triangles we have to manually calculate the uvs depending on the criteria of plane being drawn
        Vector2[] uv;



        // Because of a process called backwards culling we need to make sure we feed in the vertices in a clockwise motion. So I get the velocity and height of the hitbox being drawn 
        // and determine which way the numbers should be feed in 

        // image four points each representing the positional data that act as the four points of a rectangle, we need to test in what direction on the x and y axis the user is drawing on 
        // and where on the z axis the object is heading towards. Based on the direction we need to manipulate the direction that we draw our triangles in order for it be draw in on the map in the right
        // order

        if (endPos.z > 0 && startPos.z > 0) // left 
            if (startX < width)
            {
                if (height > startY)
                {
                    triangles = new int[] { 0, 3, 2, 0, 2, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1 1 = 1,0, 2 = 1,1
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1),
                        new Vector2(1,0)
                    };
                }
                else
                {
                    triangles = new int[] { 3, 0, 1, 3, 1, 2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1 1 = 1,0, 2 = 1,1
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0),
                        new Vector2(1,1)
                    };
                }
            }
            else
            {
                if (height > startY)
                {
                    triangles = new int[] { 1, 2, 3, 1, 3, 0 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };
                }
                else
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }

            }
        else if (endZ > 0) // right hand side, backwards culling
        {
            if (startX < 0) // determines which direction the user is drawning, right to left or left to right. Depending on which we feed different triangle data, Less than zero is left hand side
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 3, 0, 2, 0, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }
                else
                {
                    triangles = new int[] { 0, 3, 2, 0, 2, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1),
                        new Vector2(1,0)
                    };
                }
            }
            else
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };

                }
                else
                {
                    triangles = new int[] { 1, 0, 3, 1, 3, 2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1)
                    };
                }
            }
        }
        else if (startZ > 0) // left hand side clockwise culling
        {
            if (width < 0)
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }
                else
                {
                    triangles = new int[] { 1, 2, 3, 1, 3, 0 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };

                }
            }
            else
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 3, 0, 2, 0, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }
                else
                {
                    triangles = new int[] { 1, 0, 3, 1, 3, 2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1)
                    };

                }
            }
        }
        else // backwards 
        {
            if (startX > width) // 
            {

                if (startY < height)
                {
                    triangles = new int[] { 0, 3, 2, 0, 2, 1 };
                    uv = new Vector2[] {
                            // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                            new Vector2(0,0),
                            new Vector2(0,1),
                            new Vector2(1,1),
                            new Vector2(1,0)
                        };
                }
                else
                {
                    triangles = new int[] { 2, 3, 0, 2, 0, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }

            }
            else
            {

                if (startY < height)
                {
                    triangles = new int[] { 1, 2, 3, 1, 3, 0 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };
                }
                else
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }
            }
        }

        vertices = new Vector3[]
       {
                // starting position of the vertices in world space
                new Vector3(startX,startY, startZ),
                new Vector3(width,startY, endZ),
                new Vector3(width,height,endZ),
                new Vector3(startX,height,startZ)
       };
        // calculate the uvs at some point





        // allocate the corresponding data to the different aspects of the mesh object
        hitBoxMesh.vertices = vertices;
        hitBoxMesh.triangles = triangles;
        hitBoxMesh.uv = uv;
        hitBoxMesh.RecalculateNormals();
        hitBoxMesh.RecalculateBounds();








        return hitBoxMesh;

    }


}
