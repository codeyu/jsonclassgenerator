using System;
using System.Collections.Generic;

namespace System.Data.Entity.Design.PluralizationServices
{
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		
		internal Dictionary<TFirst, TSecond> FirstToSecondDictionary
		{
			get;
			
			set;
			
		}

		internal Dictionary<TSecond, TFirst> SecondToFirstDictionary
		{
			get;
			
			set;
			
		}

		internal BidirectionalDictionary()
		{
			this.FirstToSecondDictionary = new Dictionary<TFirst, TSecond>();
			this.SecondToFirstDictionary = new Dictionary<TSecond, TFirst>();
		}

		internal BidirectionalDictionary(Dictionary<TFirst, TSecond> firstToSecondDictionary) : this()
		{
			foreach (TFirst key in firstToSecondDictionary.Keys)
			{
				this.AddValue(key, firstToSecondDictionary[key]);
			}
		}

		internal void AddValue(TFirst firstValue, TSecond secondValue)
		{
			this.FirstToSecondDictionary.Add(firstValue, secondValue);
			if (!this.SecondToFirstDictionary.ContainsKey(secondValue))
			{
				this.SecondToFirstDictionary.Add(secondValue, firstValue);
			}
		}

		internal virtual bool ExistsInFirst(TFirst value)
		{
			return this.FirstToSecondDictionary.ContainsKey(value);
		}

		internal virtual bool ExistsInSecond(TSecond value)
		{
			return this.SecondToFirstDictionary.ContainsKey(value);
		}

		internal virtual TFirst GetFirstValue(TSecond value)
		{
			if (!this.ExistsInSecond(value))
			{
				return default(TFirst);
			}
			return this.SecondToFirstDictionary[value];
		}

		internal virtual TSecond GetSecondValue(TFirst value)
		{
			if (!this.ExistsInFirst(value))
			{
				return default(TSecond);
			}
			return this.FirstToSecondDictionary[value];
		}
	}
}