namespace SaveSystem
{
    public interface ISaveLoader
    {
        public void Save<T>(T data);
        public void Load();
    }
}