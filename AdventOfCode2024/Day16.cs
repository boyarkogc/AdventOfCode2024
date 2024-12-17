using System.ComponentModel.DataAnnotations;

public class Day16 {
    List<List<char>> maze;
    public Coord start;
    public Dictionary<Coord, int> nodeCost;
    public int minCost = 999999;

    public HashSet<Coord> seats;
    public struct Coord {
        public Coord(int x, int y, char facing) {
            X = x;
            Y = y;
            Facing = facing;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return X == other.X && Y == other.Y && Facing == other.Facing;
            }
            return false;
        }

        public int X { get; }
        public int Y { get; }
        public char Facing { get; }
    }

    public Day16(string inputPath) {

        maze = new List<List<char>>();
        nodeCost = new Dictionary<Coord, int>();
        seats = new HashSet<Coord>();
        
        foreach (string s in File.ReadLines(inputPath)) {
            List<char> line = new List<char>();
            for (int i = 0; i < s.Length; i++) {
                line.Add(s[i]);
                if (s[i] == 'S') start = new Coord(maze.Count(), i, 'E');
                if (s[i] != '#') {
                    nodeCost.Add(new Coord(maze.Count(), i, 'E'), 999999);
                    nodeCost.Add(new Coord(maze.Count(), i, 'S'), 999999);
                    nodeCost.Add(new Coord(maze.Count(), i, 'W'), 999999);
                    nodeCost.Add(new Coord(maze.Count(), i, 'N'), 999999);
                }
            }
            maze.Add(line);
        }
        //Console.WriteLine();
        //printWarehouse();
    }
    public void fillMazeCost(Coord c, char facing, int cost) {
        nodeCost[c] = cost;
        if (maze[c.X][c.Y] == 'E' && cost < minCost) minCost = cost;
        if (maze[c.X + 1][c.Y] != '#' && facing != 'N') {
            int moveCost = facing == 'S' ? 1 : 1001;
            Coord target = new Coord(c.X + 1, c.Y, 'S');
            if (nodeCost[target] > cost + moveCost) {
                fillMazeCost(target, 'S', cost + moveCost);
            }
        }
        if (maze[c.X - 1][c.Y] != '#' && facing != 'S') {
            int moveCost = facing == 'N' ? 1 : 1001;
            Coord target = new Coord(c.X - 1, c.Y, 'N');
            if (nodeCost[target] > cost + moveCost) {
                fillMazeCost(target, 'N', cost + moveCost);
            }
        }
        if (maze[c.X][c.Y + 1] != '#' && facing != 'W') {
            int moveCost = facing == 'E' ? 1 : 1001;
            Coord target = new Coord(c.X, c.Y + 1, 'E');
            if (nodeCost[target] > cost + moveCost) {
                fillMazeCost(target, 'E', cost + moveCost);
            }
        }
        if (maze[c.X][c.Y - 1] != '#' && facing != 'E') {
            int moveCost = facing == 'W' ? 1 : 1001;
            Coord target = new Coord(c.X, c.Y - 1, 'W');
            if (nodeCost[target] > cost + moveCost) {
                fillMazeCost(target, 'W', cost + moveCost);
            }
        }
    }
    public void findSeats() {
        findSeats(new HashSet<Coord>(), start, start.Facing, 0);
    }

    public void findSeats(HashSet<Coord> potentialSeats, Coord c, char facing, int cost) {
        HashSet<Coord> potentialSeats2 = new HashSet<Coord>(potentialSeats);
        potentialSeats2.Add(c);
        if (maze[c.X][c.Y] == 'E' && cost == minCost) {
            foreach (Coord seat in potentialSeats2) {
                Coord east_seat = new Coord(seat.X, seat.Y, 'E');
                seats.Add(east_seat);
            }
            return;
        }
        if (maze[c.X + 1][c.Y] != '#' && facing != 'N') {
            int moveCost = facing == 'S' ? 1 : 1001;
            Coord target = new Coord(c.X + 1, c.Y, 'S');
            if (nodeCost[target] == cost + moveCost) {
                findSeats(potentialSeats2, target, 'S', cost + moveCost);
            }
        }
        if (maze[c.X - 1][c.Y] != '#' && facing != 'S') {
            int moveCost = facing == 'N' ? 1 : 1001;
            Coord target = new Coord(c.X - 1, c.Y, 'N');
            if (nodeCost[target] == cost + moveCost) {
                findSeats(potentialSeats2, target, 'N', cost + moveCost);
            }
        }
        if (maze[c.X][c.Y + 1] != '#' && facing != 'W') {
            int moveCost = facing == 'E' ? 1 : 1001;
            Coord target = new Coord(c.X, c.Y + 1, 'E');
            if (nodeCost[target] == cost + moveCost) {
                findSeats(potentialSeats2, target, 'E', cost + moveCost);
            }
        }
        if (maze[c.X][c.Y - 1] != '#' && facing != 'E') {
            int moveCost = facing == 'W' ? 1 : 1001;
            Coord target = new Coord(c.X, c.Y - 1, 'W');
            if (nodeCost[target] == cost + moveCost) {
                findSeats(potentialSeats2, target, 'W', cost + moveCost);
            }
        }
    }
    public void printSeats() {
        Console.WriteLine();
        foreach (Coord s in seats) {
            //Console.WriteLine(s.X + " " + s.Y);
            maze[s.X][s.Y] = 'O';
        }
        foreach (List<char> row in maze) {
            Console.WriteLine(new string(row.ToArray()));
        }
        Console.WriteLine();
    }
}