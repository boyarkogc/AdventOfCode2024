public class Day19 {
    public List<string> towels;
    public List<string> designs;
    public HashSet<string> validDesigns;
    public Dictionary<string, long> validDesignCount;
    public HashSet<string> invalidDesigns;

    public Day19(string inputPath) {
        towels = new List<string>();
        designs = new List<string>();
        validDesigns = new HashSet<string>();
        invalidDesigns = new HashSet<string>();
        validDesignCount = new Dictionary<string, long>();
        bool start = true;
        foreach (string s in File.ReadLines(inputPath)) {
            if (start) {
                towels = s.Split(", ").ToList<string>();
                start = false;
            }else if (s.Length > 0) {
                designs.Add(s);
            }
        }
        towels.Sort();
    }
    public bool isDesignPossible(string design) {
        if (design.Length == 0) return true;
        if (validDesigns.Contains(design)) return true;
        if (invalidDesigns.Contains(design)) return false;

        foreach (string towel in towels) {
            if (design.Length >= towel.Length && design.Substring(0, towel.Length) == towel) {
                if (isDesignPossible(design.Substring(towel.Length, design.Length - towel.Length))) {
                    validDesigns.Add(design);
                    return true;
                }else {
                    invalidDesigns.Add(design);
                }
            }
        }
        return false;
    }

    public long designPossibleWaysCount(string design) {
        if (design == "gbbr") {
            int test = 0;
        }
        if (design.Length == 0) return 1;
        if (validDesigns.Contains(design)) return validDesignCount[design];
        if (invalidDesigns.Contains(design)) return 0;
        long count = 0;

        foreach (string towel in towels) {
            if (design.Length >= towel.Length && design.Substring(0, towel.Length) == towel) {
                long ways = designPossibleWaysCount(design.Substring(towel.Length, design.Length - towel.Length));
                if (ways != 0) {
                    validDesigns.Add(design);
                    long c = 0;
                    validDesignCount.TryGetValue(design, out c);
                    validDesignCount[design] = ways + c;
                    count += ways;
                }else {
                    invalidDesigns.Add(design);
                }
            }
        }
        return count;
    }

    public int numDesignsPossible() {
        int count = 0;
        foreach (string design in designs) {
            Console.WriteLine(design);
            if (isDesignPossible(design)) count++;
        }
        return count;
    }

    public long totalDesignsPossibleWays() {
        long count = 0;
        foreach (string design in designs) {
            count += designPossibleWaysCount(design);
            Console.WriteLine(design + " " + count);
        }
        return count;
    }
}