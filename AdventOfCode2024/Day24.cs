public class Day24 {
    SortedDictionary<string, int> wireValues;
    List<Gate> gates;

    //worst code of the event tbh. part 2 was largely done manually by looking for connections 
    //that did not follow the rules of a ripple carry adder
    //https://www.reddit.com/r/adventofcode/comments/1hl698z/comment/m3k68gd/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
    //I don't understand logic gates and circuits well at all, hopefully next year I will grasp it better.

    public Day24(string inputPath) {
        wireValues = new SortedDictionary<string, int>();
        gates = new List<Gate>();

        bool valueMode = true;
        foreach (string s in File.ReadLines(inputPath)) {
            if (s.Length == 0) {
                valueMode = false;
                continue;
            }
            if (valueMode) {
                string[] split = s.Split(": ");
                wireValues[split[0]] = int.Parse(split[1]);
            }else {
                string[] split = s.Split(" ");
                Gate g = new Gate(split[0], split[2], split[1], split[4]);
                gates.Add(g);
                if (!wireValues.ContainsKey(split[0])) wireValues[split[0]] = -1;
                if (!wireValues.ContainsKey(split[2])) wireValues[split[2]] = -1;
                if (!wireValues.ContainsKey(split[4])) wireValues[split[4]] = -1;

                //Part 2
                if ((g.Input1.StartsWith("x") && g.Input2.StartsWith("y")) ||
                (g.Input1.StartsWith("y") && g.Input2.StartsWith("x"))) {
                    if (g.Op == "XOR" && g.Output.StartsWith("z")) {
                        Console.WriteLine(g.Output + " 1");
                    }
                }else if (g.Op == "XOR" && !g.Output.StartsWith("z")) {
                    Console.WriteLine(g.Output + " 2");
                }else if (g.Output.StartsWith("z") && g.Op != "XOR") {
                    Console.WriteLine(g.Output + " 3");
                }
            }
        }
    }

    public struct Gate {
        public Gate(string input1, string input2, string op, string output) {
            Input1 = input1;
            Input2 = input2;
            Op = op;
            Output = output;
        }
        public string Input1 {get; set;}
        public string Input2 {get; set;}
        public string Op {get; set;}
        public string Output {get; set;}
    }

    public void fillGates() {
        bool done = false;
        while (!done) {
            done = true;
            for (int i = 0; i < gates.Count(); i++) {
                Gate g = gates[i];
                if (wireValues[g.Output] == -1 && wireValues[g.Input1] != -1 && wireValues[g.Input2] != -1) {
                    if (g.Op == "AND") {
                        wireValues[g.Output] = wireValues[g.Input1] & wireValues[g.Input2];
                    }else if (g.Op == "OR") {
                        wireValues[g.Output] = wireValues[g.Input1] | wireValues[g.Input2];
                    }else if (g.Op == "XOR") {
                        wireValues[g.Output] = wireValues[g.Input1] ^ wireValues[g.Input2];
                    }
                    done = false;
                }
            }
        }
        int test = 0;

        List<int> zValues = new List<int>();
        foreach ((string wire, int value) in wireValues) {
            if (wire.StartsWith("z")) {
                zValues.Add(value);
                //Console.WriteLine(wire + " " + value);
            }
        }
        zValues.Reverse();
        Console.WriteLine(string.Join("",zValues));
    }

}