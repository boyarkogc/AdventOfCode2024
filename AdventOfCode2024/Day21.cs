public class Day21 {
    List<string> codes;
    Dictionary<char, (int, int)> numpad;
    Dictionary<char, (int, int)> dirpad;
    const int dirRobotCount = 24;
    public Day21(string inputPath) {
        codes = new List<string>();

        numpad = new Dictionary<char, (int, int)>
        {
            { '7', (0, 0) },
            { '8', (0, 1) },
            { '9', (0, 2) },
            { '4', (1, 0) },
            { '5', (1, 1) },
            { '6', (1, 2) },
            { '1', (2, 0) },
            { '2', (2, 1) },
            { '3', (2, 2) },
            { 'X', (3, 0) },
            { '0', (3, 1) },
            { 'A', (3, 2) }
        };

        dirpad = new Dictionary<char, (int, int)>
        {
            { 'X', (0, 0) },
            { '^', (0, 1) },
            { 'A', (0, 2) },
            { '<', (1, 0) },
            { 'v', (1, 1) },
            { '>', (1, 2) }
        };

        foreach (string s in File.ReadLines(inputPath)) {
            codes.Add(s);
        }
    }
    public List<char> codeToNumpadSeq(string code) {
        List<char> numpadSeq = new List<char>();
        (int, int) pos = numpad['A'];
        foreach (char c in code) {
            (int, int) nextPos = numpad[c];
            int x_diff = pos.Item1 - nextPos.Item1;
            int y_diff = pos.Item2 - nextPos.Item2;

            if (pos.Item1 == 3 && nextPos.Item2 == 0) {
                for (int i = 0; i < x_diff; i++) {
                    numpadSeq.Add('^');
                }
                x_diff = 0;
            }
            if (pos.Item2 == 0 && nextPos.Item1 == 3) {
                for (int i = y_diff; i < 0; i++) {
                    numpadSeq.Add('>');
                }
                y_diff = 0;
            }

            for (int i = 0; i < y_diff; i++) {
                numpadSeq.Add('<');
            }
            for (int i = x_diff; i < 0; i++) {
                numpadSeq.Add('v');
            }
            for (int i = 0; i < x_diff; i++) {
                numpadSeq.Add('^');
            }
            for (int i = y_diff; i < 0; i++) {
                numpadSeq.Add('>');
            }
            numpadSeq.Add('A');
            pos = nextPos;
        }
        //Console.WriteLine(string.Join("",numpadSeq));
        return numpadSeq;
    }
    // public List<char> numpadSeqToDirSeq(List<char> numpadSeq) {
    //     List<char> dirpadSeq = new List<char>();
    //     (int, int) pos = dirpad['A'];

    //     foreach (char c in numpadSeq) {
    //         (int, int) nextPos = dirpad[c];
    //         int x_diff = pos.Item1 - nextPos.Item1;
    //         int y_diff = pos.Item2 - nextPos.Item2;
    //         if ((pos == dirpad['A'] || pos == dirpad['^']) && nextPos == dirpad['<']) {
    //             dirpadSeq.Add('v');
    //             x_diff = 0;
    //         }
    //         if (pos == dirpad['<'] && (nextPos == dirpad['^'] || nextPos == dirpad['A'])) {
    //             for (int i = y_diff; i < 0; i++) {
    //                 dirpadSeq.Add('>');
    //             }
    //             y_diff = 0;
    //         }
    //         for (int i = 0; i < y_diff; i++) {
    //             dirpadSeq.Add('<');
    //         }
    //         for (int i = x_diff; i < 0; i++) {
    //             dirpadSeq.Add('v');
    //         }
    //         for (int i = y_diff; i < 0; i++) {
    //             dirpadSeq.Add('>');
    //         }
    //         for (int i = 0; i < x_diff; i++) {
    //             dirpadSeq.Add('^');
    //         }

    //         dirpadSeq.Add('A');
    //         pos = nextPos;
    //     }
    //     //Console.WriteLine(string.Join("",dirpadSeq));
    //     return dirpadSeq;
    // }

    //Part 2
    //Have to memoize this, maybe split by A so that position is always the same at each split?

    public List<char>dirSeqTodirSeq(List<char> dirpadSeq) {
        List<char> newDirpadSeq = new List<char>();
        (int, int) pos = dirpad['A'];

        foreach (char c in dirpadSeq) {
            (int, int) nextPos = dirpad[c];
            int x_diff = pos.Item1 - nextPos.Item1;
            int y_diff = pos.Item2 - nextPos.Item2;
            if ((pos == dirpad['A'] || pos == dirpad['^']) && nextPos == dirpad['<']) {
                newDirpadSeq.Add('v');
                x_diff = 0;
            }
            if (pos == dirpad['<'] && (nextPos == dirpad['^'] || nextPos == dirpad['A'])) {
                for (int i = y_diff; i < 0; i++) {
                    newDirpadSeq.Add('>');
                }
                y_diff = 0;
            }
            for (int i = 0; i < y_diff; i++) {
                newDirpadSeq.Add('<');
            }
            for (int i = x_diff; i < 0; i++) {
                newDirpadSeq.Add('v');
            }
            for (int i = 0; i < x_diff; i++) {
                newDirpadSeq.Add('^');
            }
            for (int i = y_diff; i < 0; i++) {
                newDirpadSeq.Add('>');
            }

            newDirpadSeq.Add('A');
            pos = nextPos;
        }

        return newDirpadSeq;
    }

    Dictionary<string, long> dirSeqDictToDirSeqDict(Dictionary<string, long> dirSeqDict) {
        Dictionary<string, long> newDirSeqDict = new Dictionary<string, long>();
        foreach ((string item, long itemCount) in dirSeqDict) {
            string[] itemSplit = item.Split("A");
            for (int i = 0; i < itemSplit.Length; i++) {
                string s = itemSplit[i];
                List<char> input = s.ToCharArray().ToList<char>();
                if (i != itemSplit.Length - 1) {input.Add('A');}
                //if (input.Count() == 0) continue;
                List<char> dirSeq = dirSeqTodirSeq(input);
                string dirSeqString = string.Join("",dirSeq);
                if (dirSeqString.Length == 0) continue;
                if (!newDirSeqDict.ContainsKey(dirSeqString)) newDirSeqDict[dirSeqString] = 0;
                newDirSeqDict[dirSeqString] += itemCount;
            }
        }

        return newDirSeqDict;
    }

    public long totalComplexities() {
        long total = 0;
        long newTotal = 0;
        Dictionary<string, long> newDirSeqDict = new Dictionary<string, long>();
        foreach (string code in codes) {
            //Console.WriteLine(code.Substring(0,3) + " " + dirSeqTodirSeq(numpadSeqToDirSeq(codeToNumpadSeq(code))).Count());
            //Console.WriteLine(string.Join("",dirSeqTodirSeq(numpadSeqToDirSeq(codeToNumpadSeq(code)))));
            newDirSeqDict = new Dictionary<string, long>();
            //List<char> output = numpadSeqToDirSeq(codeToNumpadSeq(code)); //test
            List<char> output = dirSeqTodirSeq(codeToNumpadSeq(code));
            if (!newDirSeqDict.ContainsKey(string.Join("",output))) {
                newDirSeqDict[string.Join("",output)] = 0;
            }
            newDirSeqDict[string.Join("",output)]++;

            for (int i = 0; i < dirRobotCount; i++) {
                //output = dirSeqTodirSeq(output); //part 1
                newDirSeqDict = dirSeqDictToDirSeqDict(newDirSeqDict);
                //Console.WriteLine(i + output.Count());
            }

            total += long.Parse(code.Substring(0,3)) * output.Count();
            long tempTotal = 0;
            foreach ((string s, long l) in newDirSeqDict) {
                tempTotal += s.Length * l;
            }
            newTotal += long.Parse(code.Substring(0,3)) * tempTotal;
            Console.WriteLine(tempTotal);
        }
        //return total; //Part 1
        return newTotal;
    }
}