namespace Inbox.AutoMapper
{
    public class MapToAttribute : MapAttribute
    {
        public override MapFlag Direction => MapFlag.MapTo;

        public MapToAttribute(params Type[] types) : base(types)
        {
        }
    }
}
