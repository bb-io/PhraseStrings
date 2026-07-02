using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.DataHandlers.Static;

public class JobStatusChangeEventDataHandler : IStaticDataSourceItemHandler
{
    private static readonly Dictionary<string, string> Events = new()
    {
        ["jobs:create"] = "Created",
        ["jobs:start"] = "Activated",
        ["jobs:complete"] = "Completed"
    };

    public IEnumerable<DataSourceItem> GetData()
        => Events.Select(item => new DataSourceItem
        {
            Value = item.Key,
            DisplayName = item.Value
        });
}
