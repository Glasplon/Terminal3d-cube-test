
/*namespace Terminal3Drenderer;
using System.Numerics;
class Program
{

    public static Vector3 LUB = new Vector3(-5, -5, -5); // Left  Upper Back
    public static Vector3 RUB = new Vector3( 5, -5, -5); // Right Upper Back
    public static Vector3 LLB = new Vector3(-5,  5, -5); // Left  Down  Back
    public static Vector3 RLB = new Vector3( 5,  5, -5); // Right Down  Back
    public static Vector3 LUF = new Vector3(-5, -5,  5); // Left  Upper Front
    public static Vector3 RUF = new Vector3( 5, -5,  5); // Right Upper Front
    public static Vector3 LLF = new Vector3(-5,  5,  5); // Left  Down  Front
    public static Vector3 RLF = new Vector3( 5,  5,  5); // Right Down  Front

    static void Main(string[] args)
    {
        Console.Clear();
        Console.CursorVisible = false;

        int width = 50;
        int height = width/2;
        char[,] grid = new char[height, width];

        int playerX = 10;
        int playerY = 10;
        while (true)
        {
            //empty screen
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[y, x] = ' ';
                }
            }
            
            //grid[playerY, playerX] = '#'; // Player
            //DrHelp.DrawLineColorArrVector2(grid, width, height, new Vector3(playerX,playerY,10), new Vector3(3,3,-10));
            
            DrHelp.DrawLineColorArrVector2(grid, width, height, LUB, RUB);
            DrHelp.DrawLineColorArrVector2(grid, width, height, LLB, RLB);
            DrHelp.DrawLineColorArrVector2(grid, width, height, LUF, RUF);
            DrHelp.DrawLineColorArrVector2(grid, width, height, LLF, RLF);

            DrHelp.DrawLineColorArrVector2(grid, width, height, LUB, LLB);
            DrHelp.DrawLineColorArrVector2(grid, width, height, RUB, RLB);
            DrHelp.DrawLineColorArrVector2(grid, width, height, LUF, LLF);
            DrHelp.DrawLineColorArrVector2(grid, width, height, RUF, RLF);
            
            DrHelp.DrawLineColorArrVector2(grid, width, height, LUB, LUF);
            DrHelp.DrawLineColorArrVector2(grid, width, height, LLB, LLF);
            DrHelp.DrawLineColorArrVector2(grid, width, height, RUB, RUF);
            DrHelp.DrawLineColorArrVector2(grid, width, height, RLB, RLF);

            LUB = DrHelp.rotateX(LUB,0.05f);
            LLB = DrHelp.rotateX(LLB,0.05f);
            LUF = DrHelp.rotateX(LUF,0.05f);
            LLF = DrHelp.rotateX(LLF,0.05f);
            RUB = DrHelp.rotateX(RUB,0.05f);
            RLB = DrHelp.rotateX(RLB,0.05f);
            RUF = DrHelp.rotateX(RUF,0.05f);
            RLF = DrHelp.rotateX(RLF,0.05f);

            LUB = DrHelp.rotateY(LUB,0.1f);
            LLB = DrHelp.rotateY(LLB,0.1f);
            LUF = DrHelp.rotateY(LUF,0.1f);
            LLF = DrHelp.rotateY(LLF,0.1f);
            RUB = DrHelp.rotateY(RUB,0.1f);
            RLB = DrHelp.rotateY(RLB,0.1f);
            RUF = DrHelp.rotateY(RUF,0.1f);
            RLF = DrHelp.rotateY(RLF,0.1f);
    
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[y, x]);
                }
                Console.WriteLine();
            }


            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                            LUB = DrHelp.rotateX(LUB,0.1f);
                            LLB = DrHelp.rotateX(LLB,0.1f);
                            LUF = DrHelp.rotateX(LUF,0.1f);
                            LLF = DrHelp.rotateX(LLF,0.1f);
                            RUB = DrHelp.rotateX(RUB,0.1f);
                            RLB = DrHelp.rotateX(RLB,0.1f);
                            RUF = DrHelp.rotateX(RUF,0.1f);
                            RLF = DrHelp.rotateX(RLF,0.1f);
                        break;
                    case ConsoleKey.DownArrow:
                            LUB = DrHelp.rotateX(LUB,-0.1f);
                            LLB = DrHelp.rotateX(LLB,-0.1f);
                            LUF = DrHelp.rotateX(LUF,-0.1f);
                            LLF = DrHelp.rotateX(LLF,-0.1f);
                            RUB = DrHelp.rotateX(RUB,-0.1f);
                            RLB = DrHelp.rotateX(RLB,-0.1f);
                            RUF = DrHelp.rotateX(RUF,-0.1f);
                            RLF = DrHelp.rotateX(RLF,-0.1f);
                        break;
                    case ConsoleKey.LeftArrow:
                            LUB = DrHelp.rotateY(LUB,0.1f);
                            LLB = DrHelp.rotateY(LLB,0.1f);
                            LUF = DrHelp.rotateY(LUF,0.1f);
                            LLF = DrHelp.rotateY(LLF,0.1f);
                            RUB = DrHelp.rotateY(RUB,0.1f);
                            RLB = DrHelp.rotateY(RLB,0.1f);
                            RUF = DrHelp.rotateY(RUF,0.1f);
                            RLF = DrHelp.rotateY(RLF,0.1f);
                        break;
                    case ConsoleKey.RightArrow:
                            LUB = DrHelp.rotateY(LUB,-0.1f);
                            LLB = DrHelp.rotateY(LLB,-0.1f);
                            LUF = DrHelp.rotateY(LUF,-0.1f);
                            LLF = DrHelp.rotateY(LLF,-0.1f);
                            RUB = DrHelp.rotateY(RUB,-0.1f);
                            RLB = DrHelp.rotateY(RLB,-0.1f);
                            RUF = DrHelp.rotateY(RUF,-0.1f);
                            RLF = DrHelp.rotateY(RLF,-0.1f);
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
            
            Thread.Sleep(32);
        }
    }
}*/