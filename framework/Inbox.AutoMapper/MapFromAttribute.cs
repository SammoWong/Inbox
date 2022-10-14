namespace Inbox.AutoMapper
{
    public class MapFromAttribute : MapAttribute
    {
        public override MapFlag Direction => MapFlag.MapFrom;
        public MapFromAttribute(params Type[] types) : base(types)
        {
        }
    }
}
