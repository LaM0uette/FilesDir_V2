namespace FilesDir.Core;

public static class Structs
{
    public struct SFlags
    {
        public SEnum.SearchMode SearchMode;
        public string[] Words;
        public string[] Extensions;
        public int PoolSize;
        public bool Casse;
        public bool Utf;
    }
}