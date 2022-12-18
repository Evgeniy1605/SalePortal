namespace SalePortal.Domain;

public class PagingClass<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalPage { get; set; }
    public PagingClass(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPage = (int) Math.Ceiling(count/(double)pageSize);
        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPage;

    public static PagingClass<T> Create(List<T> sourse, int pageIndex, int pageSize)
    {
        var count = sourse.Count;
        var items = sourse.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PagingClass<T>(items, count, pageIndex, pageSize);
    }
}
