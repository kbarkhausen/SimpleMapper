namespace SimpleMapper
{
    public interface IMapping
    {
        IMapper Mapper { get; set; }
        void Load();
    }
}
