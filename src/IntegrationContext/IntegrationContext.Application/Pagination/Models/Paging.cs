namespace IntegrationContext.Application.Pagination.Models;

public class Paging
{
    public int ItemPerPage { get; init; }
    public int Page { get; init; }

    public Paging() : this(10, 1) { }

    public Paging(int? itemPerPage, int? page)
    {
        if (itemPerPage is null || itemPerPage <= 0)
            itemPerPage = 10;

        if (page is null || page <= 0)
            page = 1;

        ItemPerPage = (int)itemPerPage;
        Page = (int)page;
    }

    public Paging(int page) : this(10, page) { }
}
