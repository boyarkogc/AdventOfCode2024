public class Day5 {
    public static int Part1(string inputPath) {
        int middle_total = 0;
        Dictionary<int,List<int>> order_rules = new Dictionary<int, List<int>>();
        bool first_section = true;
        foreach (string s in File.ReadLines(inputPath)) {
            if (s == "") {
                first_section = false;
                continue; 
            }
            if (first_section) {
                string[] split = s.Split("|");
                int a = Int32.Parse(split[0]);
                int b = Int32.Parse(split[1]);
                if (!order_rules.ContainsKey(b)) order_rules[b] = new List<int>();
                order_rules[b].Add(a);
                //Console.WriteLine(b + " " + string.Join(" ", order_rules[b]));
            }else {
                int[] update_ints = Array.ConvertAll(s.Split(","), s => int.Parse(s));
                int middle = update_ints[update_ints.Length / 2];
                bool follows_rules = true;
                for (int i = 0; i < update_ints.Length - 1; i++) {
                    for (int j = i + 1; j < update_ints.Length; j++) {
                        if (order_rules.ContainsKey(update_ints[i]) && order_rules[update_ints[i]].Contains(update_ints[j])) follows_rules = false;
                    }
                }
                if (follows_rules) middle_total += middle;
            }
        }

        return middle_total;
    }
    public static int Part2(string inputPath) {
        int middle_total = 0;
        Dictionary<int,List<int>> order_rules = new Dictionary<int, List<int>>();
        bool first_section = true;
        foreach (string s in File.ReadLines(inputPath)) {
            if (s == "") {
                first_section = false;
                continue; 
            }
            if (first_section) {
                string[] split = s.Split("|");
                int a = Int32.Parse(split[0]);
                int b = Int32.Parse(split[1]);
                if (!order_rules.ContainsKey(b)) order_rules[b] = new List<int>();
                order_rules[b].Add(a);
                //Console.WriteLine(b + " " + string.Join(" ", order_rules[b]));
            }else {
                int[] update_ints = Array.ConvertAll(s.Split(","), s => int.Parse(s));
                bool follows_rules = false;
                bool modified = false;
                //if 2 numbers don't follow a rule, swap them and run through everything again until it all checks out
                while (!follows_rules) {
                    follows_rules = true;
                    for (int i = 0; i < update_ints.Length - 1; i++) {
                        for (int j = i + 1; j < update_ints.Length; j++) {
                            if (order_rules.ContainsKey(update_ints[i]) && order_rules[update_ints[i]].Contains(update_ints[j])) {
                                follows_rules = false;
                                int temp = update_ints[i];
                                update_ints[i] = update_ints[j];
                                update_ints[j] = temp;
                                modified = true;
                            }
                        }
                    }
                }
                if (follows_rules && modified) middle_total += update_ints[update_ints.Length / 2];
            }
        }

        return middle_total;
    }
}