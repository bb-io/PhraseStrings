using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Project;
using Tests.PhraseStrings.Base;

namespace Tests.PhraseStrings
{
    [TestClass]
    public class TranslationActionTests : TestBase
    {
        [TestMethod]
        public async Task GetLocalesForKey_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.GetTranslationForKey(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new KeyRequest { KeyId = "c7c044f03e826935d29004af975f9bd3" },
                new GetTranslationsForKeyRequest());

            Console.WriteLine($"Total: {result.Translations.Count()}");
            foreach (var item in result.Translations)
            {
                Console.WriteLine($"{item.Id}: {item.Content}");
            }
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public async Task GetLocalesForLocale_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.GetTranslationForLocale(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new LocaleRequest { LocaleId = "cde71862c52d7f9d814d116258a1c4ee" },
                new GetTranslationsForLocaleRequest());

            Console.WriteLine($"Total: {result.Translations.Count()}");
            foreach (var item in result.Translations)
            {
                Console.WriteLine($"{item.Id}: {item.Content}");
            }
            Assert.IsNotNull(result);
        }
    }
}
