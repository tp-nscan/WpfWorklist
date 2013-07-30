namespace DynamicModel.Common
{
    public interface IIndexProvider
    {
        int MakeIndex();
    }

    public static class IndexProvider
    {
        public static IIndexProvider MakeTest(int index)
        {
            return new IndexProviderTestImpl(index);
        }
    }

    class IndexProviderTestImpl : IIndexProvider
    {
         public IndexProviderTestImpl(int index)
         {
             _index = index;
         }
         readonly int _index;
        public int MakeIndex()
        {
            return _index;
        }
    }
}
