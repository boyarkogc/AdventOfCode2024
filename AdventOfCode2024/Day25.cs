public class Day25 {
    public List<List<int>> keys;
    public List<List<int>> locks;
    public Day25(string inputPath) {
        keys = new List<List<int>>();
        locks = new List<List<int>>();

        bool loadingKey = false;
        int line_count = 0;
        List<int> key_lock = new List<int>() {0,0,0,0,0};
        foreach (string line in File.ReadLines(inputPath)) {
            if (line.Length == 0) {
                line_count = 0;
                if (loadingKey) {
                    keys.Add(key_lock);
                }else {
                    locks.Add(key_lock);
                }
                key_lock = new List<int>() {0,0,0,0,0};
                continue;
            }else if (line_count == 0) {
                loadingKey = line == "....." ? true : false;
            }else if (line_count >= 1 && line_count <= 5) {
                for (int i = 0; i < line.Length; i++) {
                    if (line[i] == '#') {
                        key_lock[i]++;
                    }
                }
            }
            line_count++;

        }
        int test = 0;
    }

    public int totalKeyLockPairs() {
        int count = 0;
        foreach (List<int> key in keys) {
            foreach (List<int> lockk in locks) {
                bool validPair = true;
                for (int i = 0; i < key.Count(); i++) {
                    if (key[i] + lockk[i] >= 6) {
                        validPair = false;
                        break;
                    }
                }
                if (validPair) {
                    count++;
                }
            }
        }
        return count;
    }
}