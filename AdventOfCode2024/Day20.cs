using System.ComponentModel;

public class Day20 {
    List<List<char>> grid;
    List<Coord> path;
    List<(Coord, Coord)> cheats;

    static int minSaved = 100;
    static int maxDistance = 20;

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
    public Day20(string inputPath) {
        grid = new List<List<char>>();
        path = new List<Coord>();
        cheats = new List<(Coord, Coord)>();
        Coord start = new Coord();
        foreach (string line in File.ReadLines(inputPath)) {
            List<char> row = new List<char>();
            foreach (char c in line) {
                if (c == 'S') {
                    start = new Coord(grid.Count(), row.Count());
                    path.Add(start);
                }
                row.Add(c);
            }
            grid.Add(row);
        }
        fillPath(start);
        int test = 0;
    }

    public void fillPath(Coord c) {
        bool pathsAvailable = true;
        while (pathsAvailable) {
            Coord target = new Coord();
            pathsAvailable = false;
            if (grid[c.X + 1][c.Y] != '#') {
                target = new Coord(c.X + 1, c.Y);
                if (!path.Contains(target)) {
                    path.Add(target);
                    c = target;
                    pathsAvailable = true;
                }
            }
            if (grid[c.X - 1][c.Y] != '#') {
                target = new Coord(c.X - 1, c.Y);
                if (!path.Contains(target)) {
                    path.Add(target);
                    c = target;
                    pathsAvailable = true;
                }
            }
            if (grid[c.X][c.Y + 1] != '#') {
                target = new Coord(c.X, c.Y + 1);
                if (!path.Contains(target)) {
                    path.Add(target);
                    c = target;
                    pathsAvailable = true;
                }
            }
            if (grid[c.X][c.Y - 1] != '#') {
                target = new Coord(c.X, c.Y - 1);
                if (!path.Contains(target)) {
                    path.Add(target);
                    c = target;
                    pathsAvailable = true;
                }
            }
        }
    }
    public int numCheatsPart1() {
        int cheatCount = 0;
        foreach (Coord c in path) {
            if (c.X - 2 > 0) {
                if (grid[c.X - 1][c.Y] == '#' && grid[c.X - 2][c.Y] != '#') {
                    //have to sub 2 to accounts for time it takes to do the cheat
                    if (path.IndexOf(c) - path.IndexOf(new Coord(c.X -2, c.Y)) - 2 >= minSaved) {
                        int test = path.IndexOf(c) - path.IndexOf(new Coord(c.X - 2, c.Y)) - 2;
                        cheatCount++;
                    }
                }
            }
            if (c.Y - 2 > 0) {
                if (grid[c.X][c.Y - 1] == '#' && grid[c.X][c.Y - 2] != '#') {
                    if (path.IndexOf(c) - path.IndexOf(new Coord(c.X, c.Y - 2)) - 2 >= minSaved) {
                        int test = path.IndexOf(c) - path.IndexOf(new Coord(c.X, c.Y - 2)) - 2;
                        cheatCount++;
                    }
                }
            }
            if (c.X + 2 < grid.Count()) {
                if (grid[c.X + 1][c.Y] == '#' && grid[c.X + 2][c.Y] != '#') {
                    if (path.IndexOf(c) - path.IndexOf(new Coord(c.X + 2, c.Y)) - 2 >= minSaved) {
                        int test = path.IndexOf(c) - path.IndexOf(new Coord(c.X + 2, c.Y)) - 2;
                        cheatCount++;
                    }
                }
            }
            if (c.Y + 2 < grid[0].Count()) {
                if (grid[c.X][c.Y + 1] == '#' && grid[c.X][c.Y + 2] != '#') {
                    if (path.IndexOf(c) - path.IndexOf(new Coord(c.X, c.Y + 2)) - 2 >= minSaved) {
                        int test = path.IndexOf(c) - path.IndexOf(new Coord(c.X, c.Y + 2)) - 2;
                        cheatCount++;
                    }
                }
            }
        }
        return cheatCount;
    }

    public int numCheatsPart2() {
        int cheatCount = 0;
        for (int i = 0; i < path.Count() - minSaved; i++) {
            Coord ci = path[i];
            for (int j = i + minSaved; j < path.Count(); j++) {
                Coord cj = path[j];
                int distance = Math.Abs(ci.X - cj.X) + Math.Abs(ci.Y - cj.Y);
                if (distance <= maxDistance && j - i - distance >= minSaved) {
                    cheatCount++;
                }
            }
        }
        return cheatCount;
    }
}