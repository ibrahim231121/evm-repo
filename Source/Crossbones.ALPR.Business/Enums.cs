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
        FilterByHostList,
        SearchByNumberPlate
    }

    public enum DeleteCommandFilter
    {
        Single,
        All,
        AllOfUser
    }
}
