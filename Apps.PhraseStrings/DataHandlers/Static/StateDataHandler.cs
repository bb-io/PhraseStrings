using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.PhraseStrings.DataHandlers.Static
{
    public class StateDataHandler : IStaticDataSourceItemHandler
    {

        protected Dictionary<string, string> EnumValues => new()
        {
            {"draft", "Draft"},
            {"in_progress", "In progress"},
            {"completed","Completed" }
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
}