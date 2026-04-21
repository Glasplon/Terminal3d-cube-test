using System.Numerics;
using Microsoft.VisualBasic;

namespace Terminal3Drenderer;
public static class DrHelp
{
    //static string Grad = @"@@%%#&*+=-::..";
    static string Grad = @"@%#&*+=-;:";
    //static string Grad = @"█▓▒░";
    public static void DrawLineColorArrVector2(char[,] canvas, int canvasWidth, int canvasHeight, Vector3 Pos1, Vector3 Pos2)
    {
        DrawLineColorArr(canvas, canvasWidth, canvasHeight, (int)((Pos1.X+10)*2f),(int)Pos1.Y+10, (int)Pos1.Z, (int)((Pos2.X+10)*2f),(int)Pos2.Y+10, (int)Pos2.Z);
    }

    public static void DrawLineColorArr(char[,] canvas, int textureWidth, int textureHeight, int x0, int y0, int z0, int x1, int y1, int z1)
    {
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
                int newPixTMP = (int)(((Math.Min(Math.Max(z,-10),10)+10)/20f)*(Grad.Length-1));
                if (pixels[py,px] == ' ' || Grad.IndexOf(pixels[py,px]) > newPixTMP)
                {
                    pixels[py,px] = Grad[newPixTMP];
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