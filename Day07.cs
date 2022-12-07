using LanguageExt;

namespace advent_of_code_2022;

public static class Day07
{
    public const string TestFileName = "Day07_test";
    public const string ProductionFileName = "Day07";

    public static int Part1(IEnumerable<string> input)
    {
        var fs = ParseFs(input);

        var directories = fs.GetAllDirectories();
        var smallDirs = directories.Add(fs).Where(it => it.TotalSize < 100000).ToList();

        return smallDirs.Sum(it => it.TotalSize);
    }

    public static int Part2(IEnumerable<string> input)
    {
        var totalFsSize = 70000000;
        var requiredSpace = 30000000;

        var fs = ParseFs(input);

        var directories = fs.GetAllDirectories();
        var freeSpace = totalFsSize - fs.TotalSize;
        var spaceToFreeUp = requiredSpace - freeSpace;

        var possibleDirectories = directories.Where(it => it.TotalSize >= spaceToFreeUp).OrderBy(it => it.TotalSize);

        return possibleDirectories.First().TotalSize;
    }

    private static string NormalizeCurrentDir(string s)
    {
        if (string.IsNullOrEmpty(s)) return "/";
        return s.StartsWith("//") ? s[1..] : s;
    }

    private static ElficDir ParseFs(IEnumerable<string> input)
    {
        var fs = new ElficDir("");
        var currentDir = "/";
        foreach (var row in input)
            if (row.StartsWith("$"))
            {
                var command = row[2..].Split(" ");
                if (command[0] != "cd") continue;
                currentDir = command[1] switch
                {
                    "/" => command[1],
                    ".." => string.Join("/", currentDir.Split('/').SkipLast()),
                    _ => $"{currentDir}/{command[1]}"
                };

                currentDir = NormalizeCurrentDir(currentDir);
                fs.MoveToFolder(currentDir);
            }
            else
            {
                var rowSplit = row.Split(' ');
                if (int.TryParse(rowSplit[0], out var size)) fs.AddFile(currentDir, rowSplit[1], size);
            }

        return fs;
    }

    public record ElficDir(string Name)
    {
        public int TotalSize => Files.Sum(it => it.Size) + Directories.Sum(it => it.TotalSize);

        private Lst<ElficDir> Directories { get; set; }
        private Lst<ElficFile> Files { get; set; }

        public Lst<ElficDir> GetAllDirectories()
        {
            return Lst<ElficDir>.Empty
                .AddRange(Directories)
                .AddRange(Directories.SelectMany(it => it.GetAllDirectories()));
        }

        public void AddFile(string dirPath, string name, int size)
        {
            var dir = MoveToFolder(dirPath);
            dir.Files = dir.Files.Add(new ElficFile(size));
        }

        public ElficDir MoveToFolder(string dirPath)
        {
            if (dirPath == "/") return this;

            var dirName = dirPath.Split('/')[1];

            var dir = Directories.FirstOrDefault(d => d.Name == dirName);
            if (dir is null)
            {
                dir = new ElficDir(dirName);
                Directories = Directories.Add(dir);
            }

            return dir.MoveToFolder("/" + string.Join("/", dirPath.Split('/').Skip(2)));
        }

        private record ElficFile(int Size);
    }
}