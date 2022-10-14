namespace Inbox.AutoMapper
{
    public class MapAttribute : Attribute
    {
        public Type[] Target { get; set; }

        public virtual MapFlag Direction => MapFlag.MapTo | MapFlag.MapFrom;

        public MapAttribute(params Type[] target)
        {
            Target = target;
        }
    }

    [Flags]
    public enum MapFlag
    {
        MapTo = 1,
        MapFrom = 2
    }
}