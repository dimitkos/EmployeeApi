namespace Shared
{
    public class Paging
    {
        public int Skip { get; }
        public int Take { get; }

        public Paging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
