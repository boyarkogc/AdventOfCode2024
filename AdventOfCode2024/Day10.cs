public class Day10 {
    List<List<int>> grid = new List<List<int>>();
    HashSet<string> visitedSummits = new HashSet<string>();
    public Day10 (string inputPath) {
        foreach (string s in File.ReadLines(inputPath)) {
            List<int> line = new List<int>();
            for (int i = 0; i < s.Length; i++) {
                line.Add(int.Parse(s.Substring(i,1)));
            }
            grid.Add(line);
        }
    }
    public int pathCount(int x, int y) {
        int currentValue = grid[x][y];
        //part 1
        // if (currentValue == 9) {
        //     string coord = x.ToString() + " " + y.ToString();
        //     if (!visitedSummits.Contains(coord)) {
        //         visitedSummits.Add(coord);
        //         return 1;
        //     }else {
        //         return 0;
        //     }
        // }
        //part 2
        if (currentValue == 9) return 1;

        int validCount = 0;
        if (x - 1 >= 0 && grid[x-1][y] == currentValue + 1) validCount += pathCount(x - 1, y);
        if (y - 1 >= 0 && grid[x][y-1] == currentValue + 1) validCount += pathCount(x, y - 1);
        if (x + 1 < grid.Count() && grid[x+1][y] == currentValue + 1) validCount += pathCount(x + 1, y);
        if (y + 1 < grid[x].Count() && grid[x][y+1] == currentValue + 1) validCount += pathCount(x, y + 1);
        return validCount;
    }

    public int totalPathCount() {
        int count = 0;
        for (int i = 0; i < grid.Count(); i++) {
            for (int j = 0; j < grid[i].Count(); j++) {
                if (grid[i][j] == 0) {
                    visitedSummits = new HashSet<string>();
                    count += pathCount(i, j);
                    Console.WriteLine(count);
                }
            }
        }
        return count;
    }
}