public class Day4 {
    public static int xmasCount(string inputPath) {
        List<string> grid = new List<string>();
        int xmasCount = 0;
        foreach (string s in File.ReadLines(inputPath)) {
            grid.Add(s);
        }
        for (int i = 0; i < grid.Count(); i++) {
            for (int j = 0; j < grid[i].Length; j++) {
                if (grid[i][j] == 'X') {
                    //check clockwise starting with directly right
                    if (j < grid[i].Length - 3) {
                        if (grid[i].Substring(j,4) == "XMAS") xmasCount++;
                    }
                    if (j < grid[i].Length - 3 && i < grid.Count() - 3) {
                        if (grid[i+1][j+1] == 'M' && grid[i+2][j+2] == 'A' && grid[i+3][j+3] == 'S') xmasCount++;
                    }
                    if (i < grid.Count() - 3) {
                        if (grid[i+1][j] == 'M' && grid[i+2][j] == 'A' && grid[i+3][j] == 'S') xmasCount++;
                    }
                    if (j >= 3 && i < grid.Count() - 3) {
                        if (grid[i+1][j-1] == 'M' && grid[i+2][j-2] == 'A' && grid[i+3][j-3] == 'S') xmasCount++;
                    }
                    if (j >= 3) {
                        if (grid[i].Substring(j-3,4) == "SAMX") xmasCount++;
                    }
                    if (j >= 3 && i >= 3) {
                        if (grid[i-1][j-1] == 'M' && grid[i-2][j-2] == 'A' && grid[i-3][j-3] == 'S') xmasCount++;
                    }
                    if (i >= 3) {
                        if (grid[i-1][j] == 'M' && grid[i-2][j] == 'A' && grid[i-3][j] == 'S') xmasCount++;
                    }
                    if (j < grid[i].Length - 3 && i >= 3) {
                        if (grid[i-1][j+1] == 'M' && grid[i-2][j+2] == 'A' && grid[i-3][j+3] == 'S') xmasCount++;
                    }
                }
            }
        }
        return xmasCount;
    }

    public static int xmasCountPart2(string inputPath) {
        List<string> grid = new List<string>();
        int xmasCount = 0;
        foreach (string s in File.ReadLines(inputPath)) {
            grid.Add(s);
        }
        for (int i = 1; i < grid.Count() - 1; i++) {
            for (int j = 1; j < grid[i].Length - 1; j++) {
                if (grid[i][j] == 'A') {
                    //check NW-SE diag, then NE-SW diag
                    if ((grid[i-1][j-1] == 'M' && grid[i+1][j+1] == 'S') || (grid[i-1][j-1] == 'S' && grid[i+1][j+1] == 'M')) {
                        if ((grid[i-1][j+1] == 'M' && grid[i+1][j-1] == 'S') || (grid[i-1][j+1] == 'S' && grid[i+1][j-1] == 'M')) xmasCount++;
                    }
                }
                //Console.WriteLine(i + " " + j + " " + xmasCount);
            }
        }
        return xmasCount;
    }
}