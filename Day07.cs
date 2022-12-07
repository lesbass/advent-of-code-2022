using LanguageExt;

namespace advent_of_code_2022;

public static class Day07
{
    public const string TestFileName = "Day07_test";
    public const string ProductionFileName = "Day07";
    private const int TotalFsSize = 70_000_000;
    private const int RequiredSpace = 30_000_000;

    public static int Part1(IEnumerable<string> input)
    {
        var smallDirs = GetDirectorySizes(input)
            .Where(it => it < 100_000).ToList();

        return smallDirs.Sum(it => it);
    }

    public static int Part2(IEnumerable<string> input)
    {
        var directorySizes = GetDirectorySizes(input);
        var freeSpace = TotalFsSize - directorySizes.Max();
        var spaceToFreeUp = RequiredSpace - freeSpace;

        return directorySizes.Order()
            .First(it => it >= spaceToFreeUp);
    }

    private static List<int> GetDirectorySizes(IEnumerable<string> input)
    {
        var fs = ParseFs(input);
        return fs.GetAllDirectories().Add(fs)
            .Select(it => it.TotalSize).ToList();
    }

    private static string NormalizeCurrentDir(string s)
    {
        if (string.IsNullOrEmpty(s)) return "/";
        return s.StartsWith("//") ? s[1..] : s;
    }

    private static ElficDir ParseFs(IEnumerable<string> input)
    {
        var (_, result) = input.Aggregate((currentDir: "/", fs: new ElficDir("")), (state, row) =>
        {
            var (currentDir, fs) = state;

            if (row.StartsWith("$"))
            {
                var command = row[2..].Split(" ");
                if (command[0] != "cd") return state;
                var tempDir = NormalizeCurrentDir(command[1] switch
                {
                    "/" => command[1],
                    ".." => string.Join("/", currentDir.Split('/').SkipLast()),
                    _ => $"{currentDir}/{command[1]}"
                });

                fs.MoveToFolder(tempDir);

                return (tempDir, fs);
            }

            var rowSplit = row.Split(' ');
            if (int.TryParse(rowSplit[0], out var size)) fs.AddFile(currentDir, rowSplit[1], size);

            return state;
        });

        return result;
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