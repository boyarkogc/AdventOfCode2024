using System.Numerics;

public class Day23 {
    public List<(string, string)> links;
    public Dictionary<string, int> compCount;
    public Day23(string inputPath) {
        links = new List<(string, string)>();
        compCount = new Dictionary<string, int>();
        foreach (string s in File.ReadLines(inputPath)) {
            string[] split = s.Split("-");
            links.Add((split[0], split[1]));
            links.Add((split[1], split[0]));
            
            if (compCount.ContainsKey(split[0])) {
                compCount[split[0]]++;
            }else {
                compCount[split[0]] = 1;
            }
            if (compCount.ContainsKey(split[1])) {
                compCount[split[1]]++;
            }else {
                compCount[split[1]] = 1;
            }
        }
    }
    public int groupsOfThreeStartsWith(string s) {
        int total = 0;
        HashSet<string> visited = new HashSet<string>();
        foreach ((string compA, string compB) in links) {
            if (!compA.StartsWith(s)) continue;
            List<string> compCList = links
                    .Where(link => link.Item1 == compA && link.Item2 != compB)
                    .Select(link => link.Item2)
                    .ToList();
            foreach (string compC in compCList) {
                if (links.Contains((compB, compC))) {
                    List<string> trioList = new List<string> {compA, compB, compC};
                    trioList.Sort();
                    string trio = string.Join("-", trioList);
                    if (!visited.Contains(trio)) {
                        visited.Add(trio);
                        total++;
                        Console.WriteLine(trio);
                    }
                }
            }
        }

        return total;
    }
    //set.intersect?
    public string maxSet() {
        HashSet<string> maxSet = new HashSet<string>();
        foreach ((string compA, string compB) in links) {
            HashSet<string> compList = links
                .Where(link => link.Item1 == compA)
                .Select(link => link.Item2)
                .ToHashSet();
            compList.Add(compA);
            foreach (string comp in compList) {
                HashSet<string> compList2 = links
                    .Where(link => link.Item1 == comp)
                    .Select(link => link.Item2)
                    .ToHashSet();
                compList2.Add(comp);
                compList.IntersectWith(compList2);
            }
            if (maxSet.Count < compList.Count()) maxSet = compList;
        }
        List<string> list = maxSet.ToList();
        list.Sort();
        return string.Join(",",list);
    }
}