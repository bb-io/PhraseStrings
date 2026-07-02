using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Filters.Enums;

namespace Apps.PhraseStrings.DataHandlers.Static;

public class XliffStateDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData() =>
    [
        new(SegmentStateHelper.Serialize(SegmentState.Initial), "Initial or empty"),
        new(SegmentStateHelper.Serialize(SegmentState.Translated), "Translated"),
        new(SegmentStateHelper.Serialize(SegmentState.Reviewed), "Reviewed (verify target translation)"),
        new(SegmentStateHelper.Serialize(SegmentState.Final), "Final (review target translation)"),
    ];
}
