namespace backend.DTO;
public class RepositoryResponse<T> {
    public IEnumerable<T> Items { get; set; }
    public long TotalItems { get; set; }
}