﻿using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.PhraseStrings;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.CatAndTms];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}