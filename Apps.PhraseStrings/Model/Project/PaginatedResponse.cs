namespace Apps.PhraseStrings.Model.Project
{
    public class PaginatedResponse<T>
    {
        public List<T> Values { get; set; } = [];
        public bool IsLast { get; set; }
        public int StartAt { get; set; }
    }
}
