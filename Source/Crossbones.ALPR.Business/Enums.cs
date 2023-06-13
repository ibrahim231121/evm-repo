namespace Corssbones.ALPR.Business.Enums
{
    public enum GetQueryFilter
    {
        Single,
        All,
        AllWithoutPaging,
        AllByUser,
        AllByUserWithOutPaging,
        Count,
        FilterByHostList
    }

    public enum DeleteCommandFilter
    {
        Single,
        All,
        AllOfUser
    }
}
