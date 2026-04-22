using System.Numerics;
using Microsoft.VisualBasic;

namespace Terminal3Drenderer;
public static class DrHelp
{
    //static string Grad = @"@@%%#&*+=-::..";
    //static string Grad = @"@B$%8&WM#Z0QOLCJUYX*ozcvunxrjft/\|()1{}[]?-_+~<>i!lI;:,""^`',. ";
    static string Grad = @"@%#&*+=;:-";
    //static string Grad = @"█▓▒░";
    public static void DrawLineColorArrVector3(char[,] canvas, int canvasWidth, int canvasHeight, Vector3 Pos1, Vector3 Pos2)
    {
        /*Console.WriteLine("start");
        Console.WriteLine(Pos1.X);
        Console.WriteLine(Pos1.Y);
        Console.WriteLine(Pos2.X);
        Console.WriteLine(Pos2.Y);*/
        DrawLineColorArr(canvas, canvasWidth, canvasHeight, (int)Pos1.X,(int)Pos1.Y, (int)Pos1.Z, (int)Pos2.X,(int)Pos2.Y, (int)Pos2.Z);
        //DrawLineColorArr(canvas, canvasWidth, canvasHeight, (int)((Pos1.X+10)*2f),(int)Pos1.Y+10, (int)Pos1.Z, (int)((Pos2.X+10)*2f),(int)Pos2.Y+10, (int)Pos2.Z);
    }
    public static void DrawLineColorArrVector3Norm(char[,] canvas, int canvasWidth, int canvasHeight, Vector3 Pos1, Vector3 Pos2)
    {
        DrawLineColorArrVector3(canvas,canvasWidth,canvasHeight,new Vector3(Pos1.X+(canvasWidth/2),(Pos1.Y+(canvasWidth/2))/2,Pos1.Z+(canvasWidth/2)),new Vector3(Pos2.X+(canvasWidth/2),(Pos2.Y+(canvasWidth/2))/2,Pos2.Z+(canvasWidth/2)));
    }

    public static void DrawLineColorArr(char[,] canvas, int textureWidth, int textureHeight, int x0, int y0, int z0, int x1, int y1, int z1)
    {
        /*Console.WriteLine("start2");
        Console.WriteLine(x0);
        Console.WriteLine(y0);
        Console.WriteLine(z0);
        Console.WriteLine(x1);
        Console.WriteLine(y1);
        Console.WriteLine(z1);*/

        int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;
        int steps = Math.Max(dx, dy);
        int currentStep = 0;

        while (true)
        {
            int z = (steps == 0) ? z0 : z0 + (z1 - z0) * currentStep / steps;

            DrawPixelSquare(canvas, textureWidth, textureHeight, x0, y0, z);

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
            currentStep++;
        }
    }

    private static void DrawPixelSquare(char[,] pixels, int texWidth, int texHeight,int x, int y, float z)
    {
        int half = 1/2;
        for (int iy = -half; iy <= half; iy++)
        {
            int py = y + iy;
            if (py < 0 || py >= texHeight) continue;

            for (int ix = -half; ix <= half; ix++)
            {
                int px = x + ix;
                if (px < 0 || px >= texWidth) continue;
                //Console.WriteLine((int)(((Math.Min(Math.Max(z,-10),10)+10)/20f)*(Grad.Length-1)));
                //int newPixTMP = (int)(((Math.Min(Math.Max(z,-10),10)+10)/20f)*(Grad.Length-1));
                int newPixTMP = (int)Math.Min((Math.Max(((z/(float)texWidth)*(Grad.Length-1)),0)),Grad.Length-1); //index of the possible new pixel
                if (pixels[px,py] == ' ' || Grad.IndexOf(pixels[px,py]) > newPixTMP)
                {
                    //Console.WriteLine(newPixTMP);
                    pixels[px,py] = Grad[newPixTMP];
                }
            }
        }
    }

    public static void DrawTriNorm(char[,] canvas, int canvasWidth, int canvasHeight, Vector3 Pos1, Vector3 Pos2, Vector3 Pos3)
    {
        DrawTri(canvas,canvasWidth,canvasHeight,new Vector3(Pos1.X+(canvasWidth/2),(Pos1.Y+(canvasWidth/2))/2,Pos1.Z+(canvasWidth/2)),new Vector3(Pos2.X+(canvasWidth/2),(Pos2.Y+(canvasWidth/2))/2,Pos2.Z+(canvasWidth/2)),new Vector3(Pos3.X+(canvasWidth/2),(Pos3.Y+(canvasWidth/2))/2,Pos3.Z+(canvasWidth/2)));
    }
    public static void DrawTri(char[,] canvas, int canvasWidth, int canvasHeight, Vector3 Pos1, Vector3 Pos2, Vector3 Pos3)
    {
        // Get the bounding box of the triangle, clamped to canvas bounds
        int minX = Math.Max(0, (int)Math.Floor(Math.Min(Pos1.X, Math.Min(Pos2.X, Pos3.X))));
        int maxX = Math.Min(canvasWidth - 1, (int)Math.Ceiling(Math.Max(Pos1.X, Math.Max(Pos2.X, Pos3.X))));
        int minY = Math.Max(0, (int)Math.Floor(Math.Min(Pos1.Y, Math.Min(Pos2.Y, Pos3.Y))));
        int maxY = Math.Min(canvasHeight - 1, (int)Math.Ceiling(Math.Max(Pos1.Y, Math.Max(Pos2.Y, Pos3.Y))));

        // Precompute the denominator for barycentric coordinates
        float denom = (Pos2.Y - Pos3.Y) * (Pos1.X - Pos3.X) + (Pos3.X - Pos2.X) * (Pos1.Y - Pos3.Y);

        // Degenerate triangle (zero area), nothing to draw
        if (Math.Abs(denom) < float.Epsilon) return;

        float invDenom = 1.0f / denom;

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                // Compute barycentric coordinates
                float w1 = ((Pos2.Y - Pos3.Y) * (x - Pos3.X) + (Pos3.X - Pos2.X) * (y - Pos3.Y)) * invDenom;
                float w2 = ((Pos3.Y - Pos1.Y) * (x - Pos3.X) + (Pos1.X - Pos3.X) * (y - Pos3.Y)) * invDenom;
                float w3 = 1.0f - w1 - w2;

                // Check if inside the triangle (with small epsilon for edge pixels)
                if (w1 >= -float.Epsilon && w2 >= -float.Epsilon && w3 >= -float.Epsilon)
                {
                    // Interpolate Z depth from the three vertices
                    float interpolatedZ = w1 * Pos1.Z + w2 * Pos2.Z + w3 * Pos3.Z;
                    int depthValue = (int)interpolatedZ;

                    // Only write if new depth is greater (closer to camera / on top)
                    int newPixTMP = (int)((depthValue/(float)canvasWidth)*(Grad.Length-1)); //index of the possible new pixel
                    if (canvas[x,y] == ' ' || Grad.IndexOf(canvas[x, y]) > newPixTMP)
                    {
                        canvas[x,y] = Grad[newPixTMP];
                    }
                }
            }
        }
    }

    public static Vector3 rotateX(Vector3 v, float Radangle) {
        float c = MathF.Cos(Radangle);
        float s = MathF.Sin(Radangle);

        return new Vector3(
            v.X,
            v.Y * c - v.Z * s,
            v.Y * s + v.Z * c
        );
    }

    public static Vector3 rotateY(Vector3 v, float Radangle) {
        float c = MathF.Cos(Radangle);
        float s = MathF.Sin(Radangle);

        return new Vector3(
            v.X * c + v.Z * s,
            v.Y,
            -v.X * s + v.Z * c
        );
    }

    public static Vector3 rotateZ(Vector3 v, float Radangle) {
        float c = MathF.Cos(Radangle);
        float s = MathF.Sin(Radangle);

        return new Vector3(
            v.X * c - v.Y * s,
            v.X * s + v.Y * c,
            v.Z
        );
    }
}