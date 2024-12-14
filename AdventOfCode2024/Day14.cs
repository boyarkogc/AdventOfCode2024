using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
public class Day14 {

    public int grid_length = 101;
    public int grid_height = 103;
    public List<Coord> robot_positions = new List<Coord>();
    public struct Coord {
        public Coord(long x, long y) {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public long X { get; }
        public long Y { get; }
    }

    public void loadAndMoveRobots(string inputPath, int seconds) {
        robot_positions = new List<Coord>();
        foreach (string line in File.ReadLines(inputPath)) {
            string[] split = line.Split();
            int pos_x = int.Parse(split[0].Split("=")[1].Split(",")[0]);
            int pos_y = int.Parse(split[0].Split("=")[1].Split(",")[1]);
            int vel_x = int.Parse(split[1].Split("=")[1].Split(",")[0]);
            int vel_y = int.Parse(split[1].Split("=")[1].Split(",")[1]);
            int final_x = (pos_x + vel_x * seconds) % grid_length;
            int final_y = (pos_y + vel_y * seconds) % grid_height;
            if (final_x < 0) final_x += grid_length;
            if (final_y < 0) final_y += grid_height;
            Coord final_pos = new Coord(final_x, final_y);
            robot_positions.Add(final_pos);
        }
        //Console.WriteLine(safetyFactor());
        createImage(robot_positions, "image" + seconds);
    }

    public int safetyFactor() {
        int ne_robot_count = 0;
        int se_robot_count = 0;
        int sw_robot_count = 0 ;
        int nw_robot_count = 0;
        foreach (Coord robot in robot_positions) {
            Console.WriteLine(robot.X + " " + robot.Y);
            if (robot.X < grid_length / 2) {
                if (robot.Y < grid_height / 2) {
                    nw_robot_count++;
                }
                if (robot.Y > grid_height / 2) {
                    sw_robot_count++;
                }
            }
            if (robot.X > grid_length / 2) {
                if (robot.Y < grid_height / 2) {
                    ne_robot_count++;
                }
                if (robot.Y > grid_height / 2) {
                    se_robot_count++;
                }
            }
        }
        Console.WriteLine(ne_robot_count + " " + se_robot_count + " " + sw_robot_count + " " + nw_robot_count);
        return ne_robot_count * se_robot_count * sw_robot_count * nw_robot_count;
    }

    public void createImage(List<Coord> robots, string image_name) {
        Image<Rgba32> image = new (600,600);
        image.Mutate( x=> x.Fill(Color.White));
        foreach (Coord r in robots) {
            SixLabors.ImageSharp.Drawing.RectangularPolygon p = new(new PointF((float)r.X * 5,(float)r.Y * 5),new SizeF(5.0f, 5.0f));
            image.Mutate( x=> x.Fill(Color.Red, p));
        }
        image.Save(image_name + ".jpg");
    }
}