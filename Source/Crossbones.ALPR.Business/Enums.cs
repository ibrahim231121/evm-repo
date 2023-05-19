namespace Corssbones.ALPR.Business.Enums
{
    public enum GetQueryFilter
    {
        Single,
        All,
        AllWithoutPaging,
        AllByUser,
        AllByUserWithOutPaging,
        Count
    }

    public enum DeleteCommandFilter
    {
        Single,
        All,
        AllOfUser
    }
}
