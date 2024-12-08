public class Day8 {
    public static int uniqueLocations(string inputPath) {
        Dictionary<char,List<(int,int)>> frequencyLocations = new Dictionary<char, List<(int, int)>>();

        int lineNum = -1;
        int lineLength = 0;
        foreach (string line in File.ReadLines(inputPath)) {
            lineNum++;
            lineLength = line.Length;
            for (int i = 0; i < line.Length; i++) {
                if (line[i] != '.') {
                    if (!frequencyLocations.ContainsKey(line[i])) frequencyLocations[line[i]] = new List<(int, int)>();
                    frequencyLocations[line[i]].Add((lineNum, i));
                }
            }
        }
        

        HashSet<(int,int)> uniqueAntinodeLocations = new HashSet<(int, int)>();
        foreach (List<(int,int)> frequencyLocationList in frequencyLocations.Values) {
            if (frequencyLocationList.Count > 0) {
                for (int i = 0; i < frequencyLocationList.Count - 1; i++) {
                    for (int j = i + 1; j < frequencyLocationList.Count; j++) {
                        uniqueAntinodeLocations.Add(frequencyLocationList[i]);
                        uniqueAntinodeLocations.Add(frequencyLocationList[j]);
                        bool antiNodeInBounds = true;
                        int antiNodeMultiplier = 1;
                        while (antiNodeInBounds) {
                            antiNodeInBounds = false;
                            (int, int) antiNode1 = (frequencyLocationList[i].Item1 + (frequencyLocationList[i].Item1 - frequencyLocationList[j].Item1) * antiNodeMultiplier, frequencyLocationList[i].Item2 + (frequencyLocationList[i].Item2 - frequencyLocationList[j].Item2) * antiNodeMultiplier);
                            (int, int) antiNode2 = (frequencyLocationList[j].Item1 - (frequencyLocationList[i].Item1 - frequencyLocationList[j].Item1) * antiNodeMultiplier, frequencyLocationList[j].Item2 - (frequencyLocationList[i].Item2 - frequencyLocationList[j].Item2) * antiNodeMultiplier);
                            if (antiNode1.Item1 >= 0 && antiNode1.Item1 <= lineNum && antiNode1.Item2 >= 0 && antiNode1.Item2 < lineLength) {
                                uniqueAntinodeLocations.Add(antiNode1);
                                antiNodeInBounds = true;
                            }
                            if (antiNode2.Item1 >= 0 && antiNode2.Item1 <= lineNum && antiNode2.Item2 >= 0 && antiNode2.Item2 < lineLength) {
                                uniqueAntinodeLocations.Add(antiNode2);
                                antiNodeInBounds = true;
                            }
                            antiNodeMultiplier++;
                        }
                    }
                }
            }
        }

        return uniqueAntinodeLocations.Count();
    }
}