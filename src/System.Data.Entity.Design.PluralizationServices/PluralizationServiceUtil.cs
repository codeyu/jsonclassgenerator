using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Design.PluralizationServices
{
	internal static class PluralizationServiceUtil
	{
		internal static bool DoesWordContainSuffix(string word, IEnumerable<string> suffixes, CultureInfo culture)
		{
			return suffixes.Any<string>((string x) => word.EndsWith(x));
		}

		internal static bool TryGetMatchedSuffixForWord(string word, IEnumerable<string> suffixes, CultureInfo culture, out string matchedSuffix)
		{
			Func<string, bool> func = null;
			matchedSuffix = null;
			if (!PluralizationServiceUtil.DoesWordContainSuffix(word, suffixes, culture))
			{
				return false;
			}
			if (func == null)
			{
				func = (string x) => word.EndsWith(x);
			}
			matchedSuffix = suffixes.First<string>(func);
			return true;
		}

		internal static bool TryInflectOnSuffixInWord(string word, IEnumerable<string> suffixes, Func<string, string> operationOnWord, CultureInfo culture, out string newWord)
		{
			string str;
			newWord = null;
			if (!PluralizationServiceUtil.TryGetMatchedSuffixForWord(word, suffixes, culture, out str))
			{
				return false;
			}
			newWord = operationOnWord(word);
			return true;
		}

		
	}
}