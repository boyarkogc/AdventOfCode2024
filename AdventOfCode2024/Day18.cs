public class Day18 {
    const int width = 71 + 2;//padding
    const int height = 71 + 2;//padding
    //part 2 doesn't require any code changes.
    //just change maxBytesFallen until you find max number that is less than mincost (which means a path has been found)
    //if mincost is unchanged that means there is no path
    //then look up the coordinate at this line number in input
    int maxBytesFallen = 2951;

    List<List<char>> grid;
    public int minCost = 999999;
    public Dictionary<Coord, int> nodeCost;


    public struct Coord {
        public Coord(int x, int y) {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public int X { get; }
        public int Y { get; }
    }

    public Day18(string inputPath) {
        grid = new List<List<char>>();
        nodeCost = new Dictionary<Coord, int>();
        for (int i = 0; i < width; i++) {
            List<char> line = new List<char>();
            for (int j = 0; j < height; j++) {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1) {
                    line.Add('#');
                }else {
                    line.Add('.');
                }
            }
            grid.Add(line);
        }
        int bytesFallen = 0;
        foreach (string s in File.ReadLines(inputPath)) {
            string[] split = s.Split(",");
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);
            grid[x + 1][y + 1] = '#';
            bytesFallen++;
            if (bytesFallen == maxBytesFallen) break;
        }
        for (int i = 0; i < grid.Count(); i++) {
            for (int j = 0; j < grid[i].Count(); j++) {
                if (grid[i][j] != '#') {
                    nodeCost.Add(new Coord(i, j), 999999);
                }
            }
        }
    }
    public void fillgridCost() {
        fillgridCost(new Coord(1, 1), 0);
    }
    public void fillgridCost(Coord c, int cost) {
        nodeCost[c] = cost;
        if (cost < minCost && c.X == width - 2 && c.Y == height - 2) minCost = cost;
        
        int moveCost = 1;
        if (grid[c.X + 1][c.Y] != '#') {
            Coord target = new Coord(c.X + 1, c.Y);
            if (nodeCost[target] > cost + moveCost) {
                fillgridCost(target, cost + moveCost);
            }
        }
        if (grid[c.X - 1][c.Y] != '#') {
            Coord target = new Coord(c.X - 1, c.Y);
            if (nodeCost[target] > cost + moveCost) {
                fillgridCost(target, cost + moveCost);
            }
        }
        if (grid[c.X][c.Y + 1] != '#') {
            Coord target = new Coord(c.X, c.Y + 1);
            if (nodeCost[target] > cost + moveCost) {
                fillgridCost(target, cost + moveCost);
            }
        }
        if (grid[c.X][c.Y - 1] != '#') {
            Coord target = new Coord(c.X, c.Y - 1);
            if (nodeCost[target] > cost + moveCost) {
                fillgridCost(target, cost + moveCost);
            }
        }
    }
}