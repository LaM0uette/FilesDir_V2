using FilesDir.Utils;

namespace FilesDir;

public static class Api
{
    public static async Task DirSearchAsync(string sDir)
    {
        // For each folders
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await DirSearchAsync(dir);
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
            catch (Exception)
            {
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
        });
        
        // For each files
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    var fi = new FileInfo(file);

                    if (!fi.Name.ToLower().Contains("cem") || file.Contains('~')) return;

                    Interlocked.Add(ref Var.Results.NbFiles, 1);
                    
                    Var.Log.Ok(fi.Name);
                    Var.Dump.String($"{Var.Results.NbFiles};{fi.Name};{fi.CreationTime};{fi.LastWriteTime};{fi.FullName};{fi.Directory}");
                }, _);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
}