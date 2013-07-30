namespace SortingNetwork.SorterPoolOpsSpec
{
    public interface ISorterPoolOp
    {
        SorterPoolOpType SorterPoolOpType { get;  }
        string Comment { get; }
    }
}