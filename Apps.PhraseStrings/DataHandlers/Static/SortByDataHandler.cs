using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.Handlers.Static;
public class SortByDataHandler : IStaticDataSourceItemHandler
{

    protected Dictionary<string, string> EnumValues => new()
        {
            {"name_asc", "Name ascending"},
            {"name_desc", "Name descending"},
            {"updated_at_asc","Updated oldest first" },
            {"updated_at_desc", "Updated newest first" },
            {"space_asc", "Space ascending" },
            {"space_desc", "Space descending" }
        };
    public IEnumerable<DataSourceItem> GetData()
    {
        return EnumValues.Select(item => new DataSourceItem
        {
            DisplayName = item.Value,
            Value = item.Key
        });
    }

}
