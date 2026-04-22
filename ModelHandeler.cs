namespace Terminal3Drenderer;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

#nullable disable

public class modelRenderer
{
    private Vector3[] verts;
    public List<Tri> Triangles = new();

    public modelRenderer(string asset, float scale)
    {
        //using var sr = new StringReader(Assets.PLY_monkey1);
        using var sr = new StringReader(asset);
        string line;
        int lineNumber = 1;
        int vertexAmount = 0;
        int FaceAmounts;
        int vertexCountIndexer = 0;
        bool HeaderDone = false;

        while ((line = sr.ReadLine()) != null)
        {
            if (line.StartsWith("comment")) {
                lineNumber++;
                continue; // move to next line
            } else
            if (line.StartsWith("element vertex")) {
                vertexAmount = int.Parse(line.Substring("element vertex".Length));
                Console.WriteLine(vertexAmount);
                verts = new Vector3[vertexAmount];
            } else
            if (line.StartsWith("element face")) {
                FaceAmounts = int.Parse(line.Substring("element face".Length));
                Console.WriteLine(FaceAmounts);
            } else
            if (line.StartsWith("end_header")){
                HeaderDone = true;
            } else
            if (HeaderDone)
            {
                if (vertexCountIndexer < vertexAmount)
                {
                    //Console.WriteLine(line);

                    string[] parts = line.Split(
                        new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                    if (parts.Length == 3 &&
                        float.TryParse(parts[0], out float x) &&
                        float.TryParse(parts[1], out float y) &&
                        float.TryParse(parts[2], out float z))
                    {
                        verts[vertexCountIndexer] = new Vector3(x*scale,y*scale,z*scale);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format.");
                    }
                    vertexCountIndexer++;
                } else {
                    string[] parts = line.Split(' ');
                    int i, a, b, c, r, g, bl;

                    if (parts.Length == 7 &&
                        int.TryParse(parts[0], out i) &&
                        int.TryParse(parts[1], out a) &&
                        int.TryParse(parts[2], out b) &&
                        int.TryParse(parts[3], out c) &&
                        int.TryParse(parts[4], out r) &&
                        int.TryParse(parts[5], out g) &&
                        int.TryParse(parts[6], out bl) && i == 3)
                    {
                        //Color color = new Color(r / 255f, g / 255f, bl / 255f);
                        Triangles.Add(new Tri(
                            new Vector3(verts[a].X,verts[a].Y,verts[a].Z),
                            new Vector3(verts[b].X,verts[b].Y,verts[b].Z),
                            new Vector3(verts[c].X,verts[c].Y,verts[c].Z)
                        ));
                    } else if (parts.Length == 4 &&
                        int.TryParse(parts[0], out i) &&
                        int.TryParse(parts[1], out a) &&
                        int.TryParse(parts[2], out b) &&
                        int.TryParse(parts[3], out c) && i == 3)
                    {
                        //Color color = new Color(255,255,255); //white default
                        Triangles.Add(new Tri(
                            new Vector3(verts[a].X,verts[a].Y,verts[a].Z),
                            new Vector3(verts[b].X,verts[b].Y,verts[b].Z),
                            new Vector3(verts[c].X,verts[c].Y,verts[c].Z)
                        ));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. at triangle creation for model load, probably a quad inmodel data, all models have to be only triangles.");
                        Console.Write("input line that messed up: ");
                        Console.WriteLine(line);
                        Console.Write("total vertex amount: ");
                        Console.WriteLine(vertexAmount);
                    }
                }
            }

            lineNumber++;
        }
    }
}