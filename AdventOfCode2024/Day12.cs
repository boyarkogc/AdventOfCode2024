public class Day12 {
    public List<List<char>> grid;
    public HashSet<Coord> visitedCoords;

    public struct Coord {
        public Coord(int row, int col) {
            Row = row;
            Col = col;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return Row == other.Row && Col == other.Col;
            }
            return false;
        }

        public int Row { get; }
        public int Col { get; }
    }

    public Day12(string inputPath) {
        grid = new List<List<char>>();
        visitedCoords = new HashSet<Coord>();

        foreach (string s in File.ReadLines(inputPath)) {
            List<char> line = new List<char>();
            for (int i = 0; i < s.Length; i++) {
                line.Add(s[i]);
            }
            grid.Add(line);
        }
    }

    public int totalFenceCost() {
        int cost = 0;
        for (int row = 0; row < grid.Count(); row++) {
            for (int col = 0; col < grid[row].Count(); col++) {
                Coord c = new Coord(row, col);
                if (visitedCoords.Contains(c)) continue;
                HashSet<Coord> newRegion = new HashSet<Coord>();
                fillRegion(c, ref newRegion);
                int area = newRegion.Count();
                //int perimeter = calcPerimeter(newRegion);
                int perimeter = calcPerimeter2(newRegion);
                cost += area * perimeter;
                Console.WriteLine(grid[row][col] + " " + area * perimeter);
            }
        }

        return cost;
    }
    //takes starting coord c and empty set region to fill in
    public void fillRegion(Coord c, ref HashSet<Coord> region) {
        char plant = grid[c.Row][c.Col];
        region.Add(c);
        visitedCoords.Add(c);
        if (c.Row - 1 >= 0 && grid[c.Row - 1][c.Col] == plant && !region.Contains(new Coord(c.Row - 1, c.Col))) fillRegion(new Coord(c.Row - 1, c.Col), ref region);
        if (c.Col - 1 >= 0 && grid[c.Row][c.Col - 1] == plant && !region.Contains(new Coord(c.Row, c.Col - 1))) fillRegion(new Coord(c.Row, c.Col - 1), ref region);
        if (c.Row + 1 < grid.Count() && grid[c.Row + 1][c.Col] == plant && !region.Contains(new Coord(c.Row + 1, c.Col))) fillRegion(new Coord(c.Row + 1, c.Col), ref region);
        if (c.Col + 1 < grid[c.Row].Count() && grid[c.Row][c.Col + 1] == plant && !region.Contains(new Coord(c.Row, c.Col + 1))) fillRegion(new Coord(c.Row, c.Col + 1), ref region);

        return;
    }

    public int calcPerimeter(HashSet<Coord> region) {
        int perimeter = 0;
        foreach (Coord c in region) {
            if (!region.Contains(new Coord(c.Row - 1, c.Col))) perimeter++;
            if (!region.Contains(new Coord(c.Row + 1, c.Col))) perimeter++;
            if (!region.Contains(new Coord(c.Row, c.Col - 1))) perimeter++;
            if (!region.Contains(new Coord(c.Row, c.Col + 1))) perimeter++;
        }
        return perimeter;
    }

    public int calcPerimeter2(HashSet<Coord> region) {
        int perimeter = 0;
        foreach (Coord c in region) {
            if (!region.Contains(new Coord(c.Row - 1, c.Col)) && (!region.Contains(new Coord(c.Row, c.Col - 1)) || region.Contains(new Coord(c.Row - 1, c.Col - 1)))) perimeter++;
            if (!region.Contains(new Coord(c.Row + 1, c.Col)) && (!region.Contains(new Coord(c.Row, c.Col + 1)) || region.Contains(new Coord(c.Row + 1, c.Col + 1)))) perimeter++;
            if (!region.Contains(new Coord(c.Row, c.Col - 1)) && (!region.Contains(new Coord(c.Row + 1, c.Col)) || region.Contains(new Coord(c.Row + 1, c.Col - 1)))) perimeter++;
            if (!region.Contains(new Coord(c.Row, c.Col + 1)) && (!region.Contains(new Coord(c.Row - 1, c.Col)) || region.Contains(new Coord(c.Row - 1, c.Col + 1)))) perimeter++;
        }        
        return perimeter;
    }
}