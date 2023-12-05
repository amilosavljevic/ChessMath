using System;
using System.Collections;
using System.Collections.Generic;

namespace ChessMath.Shared.Common.Localizations
{
    public interface ILanguageDictionary
    {
        string Get(string key);
    }

    public class LanguageDictionary : ILanguageDictionary, IEnumerable
    {
        private readonly Dictionary<string, string> entries;

        public LanguageDictionary()
        {
            entries = new Dictionary<string, string>();
        }

        public LanguageDictionary(Dictionary<string, string> entries)
        {
            this.entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        public void Add(string key, string value) =>
            entries.Add(key, value);

        public string Get(string key) =>
            entries.TryGetValue(key, out var value) ? value : null;

        public IEnumerator GetEnumerator() =>
            throw new System.NotImplementedException();
    }
}
