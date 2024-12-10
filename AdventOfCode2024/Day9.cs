public class Day9 {
    public List<FileBlock> diskMap = new List<FileBlock>();
    public struct FileBlock {
        public FileBlock(long x, long y)
        {
            X = x;
            Y = y;
        }

        public long X { get; }
        public long Y { get; }

    }

    public Day9(string inputPath) {
        string initialMap = File.ReadAllText(inputPath);
        for (int i = 0; i < initialMap.Length; i++) {
            if (i % 2 == 0) {
                diskMap.Add(new FileBlock(i / 2, long.Parse(initialMap.Substring(i,1))));
                //Console.WriteLine(i / 2 + " " + long.Parse(initialMap.Substring(i,1)));
            }else {
                diskMap.Add(new FileBlock(-1, long.Parse(initialMap.Substring(i,1))));
                //Console.WriteLine(-1 + " " + long.Parse(initialMap.Substring(i,1)));
            }
        }
    }

    public void rearrange() {
        List<FileBlock> newDiskMap = new List<FileBlock>();
        int reverseIndex = diskMap.Count() - 1;
        int index = 0;
        while (index != reverseIndex) {
            //Console.WriteLine(index +" " + reverseIndex);
            if (diskMap[index].X != -1 || diskMap[index].Y == 0) {
                newDiskMap.Add(diskMap[index]);
                index++;
                continue;
            }
            if (diskMap[reverseIndex].X == -1 || diskMap[reverseIndex].Y == 0) {
                reverseIndex--;
                continue;
            }
            long count = Math.Min(diskMap[index].Y, diskMap[reverseIndex].Y);
            diskMap[index] = new FileBlock(diskMap[index].X, diskMap[index].Y - count);
            diskMap[reverseIndex] = new FileBlock(diskMap[reverseIndex].X, diskMap[reverseIndex].Y - count);

            newDiskMap.Add(new FileBlock(diskMap[reverseIndex].X, count));
        }

        for (int i = reverseIndex; i < diskMap.Count(); i++) {
            if (diskMap[i].X == -1) diskMap[i] = new FileBlock(diskMap[i].X, 0);
            newDiskMap.Add(diskMap[i]);
        }

        diskMap = newDiskMap;
    }

    public void rearrange2() {
        int reverseIndex = diskMap.Count() - 1;
        int index = 0;
        while (reverseIndex != 0) {
            if (index == reverseIndex) {
                index = 0;
                reverseIndex--;
                continue;
            }
            if (diskMap[reverseIndex].X == -1) {
                reverseIndex--;
                continue;
            }
            if (diskMap[index].X != -1) {
                index++;
                continue;
            }
            if (diskMap[index].Y < diskMap[reverseIndex].Y) {
                index++;
                continue;
            }

            long count = Math.Min(diskMap[index].Y, diskMap[reverseIndex].Y);
            diskMap[index] = new FileBlock(diskMap[index].X, diskMap[index].Y - count);
            diskMap[reverseIndex] = new FileBlock(diskMap[reverseIndex].X, diskMap[reverseIndex].Y - count);
            diskMap.Insert(index, new FileBlock(diskMap[reverseIndex].X, count));
            diskMap.Insert(reverseIndex, new FileBlock(-1, count));
            //reverseIndex++;
            index = 0;
        }

        for (int i = 0; i < diskMap.Count(); i++) {
            //if (diskMap[i].X == -1) diskMap[i] = new FileBlock(diskMap[i].X, 0);
        }
    }

    public long checksum() {
        long checksum = 0;
        long index = 0;
        foreach (FileBlock block in diskMap) {
            Console.WriteLine(block.X + " " + block.Y);
            for (long i = 0; i < block.Y; i++) {
                if (block.X != -1) checksum += index * block.X;
                //Console.WriteLine(index + " * " + block.X);
                index++;
            }
        }

        return checksum;
    }

}