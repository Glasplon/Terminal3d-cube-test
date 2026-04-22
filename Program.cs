
namespace Terminal3Drenderer;
using System.Numerics;
class Program
{
    private static Tri[] tris;

    private static modelRenderer modelLoader;
    private static bool wireframe = false;

    static void Main(string[] args)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int width = 100;
        int height = width/2;
        char[,] grid = new char[width, height];

        if (args.Length < 1)
        {
            Console.WriteLine("no file passed");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found: " + filePath);
            return;
        }

        if(args.Length > 1)
        {
            if (args[1] == "v")
            {
                wireframe = true;
            }
        }
        int scale = 25;
        if(args.Length > 2)
        {
            if (int.TryParse(args[2], out scale))
            {
                wireframe = true;
            }
        }

        string plyContent = File.ReadAllText(filePath);

        Console.WriteLine("File loaded. Length: " + plyContent.Length);
        
        modelLoader = new modelRenderer(plyContent,scale);
        tris = new Tri[modelLoader.Triangles.Count];

        for (int i = 0; i < modelLoader.Triangles.Count; i++)
        {
            tris[i] = modelLoader.Triangles[i];
        }

        while (true)
        {
            //empty screen
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = ' ';
                }
            }
            
            //grid[playerY, playerX] = '#'; // Player
            //DrHelp.DrawLineColorArrVector2(grid, width, height, new Vector3(playerX,playerY,10), new Vector3(3,3,-10));
            
            //DrHelp.DrawTriNorm(grid, width, height, new Vector3(-25,-25,25),new Vector3(25,-25,-25),new Vector3(0,25,0));
            //DrHelp.DrawTriNorm(grid, width, height, new Vector3(-25,-25,-25),new Vector3(-25,25,25),new Vector3(25,25,0));
            for (int i = 0; i < tris.Length; i++)
            {
                //Console.WriteLine(tris[i].vert0.Z);
                if (wireframe)
                {
                    DrHelp.DrawLineColorArrVector3Norm(grid, width, height, tris[i].vert0,tris[i].vert1);
                    DrHelp.DrawLineColorArrVector3Norm(grid, width, height, tris[i].vert0,tris[i].vert2);
                    DrHelp.DrawLineColorArrVector3Norm(grid, width, height, tris[i].vert1,tris[i].vert2);
                } else
                {
                    DrHelp.DrawTriNorm(grid, width, height, tris[i].vert0,tris[i].vert1,tris[i].vert2);
                }

                tris[i].vert0 = DrHelp.rotateX(tris[i].vert0,0.1f);
                tris[i].vert1 = DrHelp.rotateX(tris[i].vert1,0.1f);
                tris[i].vert2 = DrHelp.rotateX(tris[i].vert2,0.1f);
                tris[i].vert0 = DrHelp.rotateY(tris[i].vert0,0.1f);
                tris[i].vert1 = DrHelp.rotateY(tris[i].vert1,0.1f);
                tris[i].vert2 = DrHelp.rotateY(tris[i].vert2,0.1f);
            }
    
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }


            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    case ConsoleKey.LeftArrow:
                        break;
                    case ConsoleKey.RightArrow:
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
            
            Thread.Sleep(32);
        }
    }
}

public class Tri
{
    public Vector3 vert0;
    public Vector3 vert1;
    public Vector3 vert2;
    public Tri(Vector3 firstV, Vector3 secondV, Vector3 lastV)
    {
        vert0 = firstV;
        vert1 = secondV;
        vert2 = lastV;
    }
}