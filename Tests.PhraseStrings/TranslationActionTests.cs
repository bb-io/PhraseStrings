using Apps.PhraseStrings.Actions;
using Apps.PhraseStrings.Model;
using Apps.PhraseStrings.Model.Key;
using Apps.PhraseStrings.Model.Locale;
using Apps.PhraseStrings.Model.Project;
using Apps.PhraseStrings.Model.Translation;
using Blackbird.Applications.Sdk.Common.Files;
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

        [TestMethod]
        public async Task CreateTranslation_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.CreateTranslation(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new CreateTranslationRequest { });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateTranslation_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.UpdateTranslation(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, new UpdateTranslationRequest { },
                new TranslationRequest { TranslationId= "45838bbb73329f3ae087fdf955f1b24a" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DownloadLocale_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.DownloadLocale(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" }, new LocaleRequest { LocaleId= "cde71862c52d7f9d814d116258a1c4ee" },
                new DownloadLocaleRequest { FileFormat= "xlf" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UploadFile_IsSuccess()
        {
            var action = new TranslationAction(InvocationContext, FileManager);
            var result = await action.UploadFile(
                new ProjectRequest { ProjectId = "52ea432ad1debbf8e09cdf344998167d" },
                new UploadFileRequest { File= new FileReference {Name= "original.html" }, FileFormat= "html",
                LocaleId= "cde71862c52d7f9d814d116258a1c4ee"});

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Assert.IsNotNull(result);
        }
    }
}
