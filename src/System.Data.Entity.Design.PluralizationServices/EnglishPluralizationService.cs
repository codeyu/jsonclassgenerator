using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Design.PluralizationServices
{
	internal class EnglishPluralizationService : PluralizationService, ICustomPluralizationMapping
	{
		private Dictionary<string, string> _assimilatedClassicalInflectionList;

		private StringBidirectionalDictionary _assimilatedClassicalInflectionPluralizationService;

		private Dictionary<string, string> _classicalInflectionList;

		private StringBidirectionalDictionary _classicalInflectionPluralizationService;

		private Dictionary<string, string> _irregularPluralsList;

		private StringBidirectionalDictionary _irregularPluralsPluralizationService;

		private Dictionary<string, string> _irregularVerbList;

		private StringBidirectionalDictionary _irregularVerbPluralizationService;

		private List<string> _knownConflictingPluralList;

		private List<string> _knownPluralWords;

		private List<string> _knownSingluarWords;

		private Dictionary<string, string> _oSuffixList;

		private StringBidirectionalDictionary _oSuffixPluralizationService;

		private List<string> _pronounList;

		private string[] _uninflectiveSuffixes = new string[] { "fish", "ois", "sheep", "deer", "pos", "itis", "ism" };

		private string[] _uninflectiveWords = new string[] { "bison", "flounder", "pliers", "bream", "gallows", "proceedings", "breeches", "graffiti", "rabies", "britches", "headquarters", "salmon", "carp", "herpes", "scissors", "chassis", "high-jinks", "sea-bass", "clippers", "homework", "series", "cod", "innings", "shears", "contretemps", "jackanapes", "species", "corps", "mackerel", "swine", "debris", "measles", "trout", "diabetes", "mews", "tuna", "djinn", "mumps", "whiting", "eland", "news", "wildebeest", "elk", "pincers", "police", "hair", "ice", "chaos", "milk", "cotton", "corn", "millet", "hay", "pneumonoultramicroscopicsilicovolcanoconiosis", "information", "rice", "tobacco", "aircraft", "rabies", "scabies", "diabetes", "traffic", "cotton", "corn", "millet", "rice", "hay", "hemp", "tobacco", "cabbage", "okra", "broccoli", "asparagus", "lettuce", "beef", "pork", "venison", "bison", "mutton", "cattle", "offspring", "molasses", "shambles", "shingles" };

		private BidirectionalDictionary<string, string> _userDictionary;

		private Dictionary<string, string> _wordsEndingWithSeList;

		private StringBidirectionalDictionary _wordsEndingWithSePluralizationService;

		private Dictionary<string, string> _wordsEndingWithSisList;

		private StringBidirectionalDictionary _wordsEndingWithSisPluralizationService;

		internal EnglishPluralizationService()
		{
			Dictionary<string, string> strs = new Dictionary<string, string>()
			{
				{ "am", "are" },
				{ "are", "are" },
				{ "is", "are" },
				{ "was", "were" },
				{ "were", "were" },
				{ "has", "have" },
				{ "have", "have" }
			};
			this._irregularVerbList = strs;
			List<string> strs1 = new List<string>()
			{
				"I",
				"we",
				"you",
				"he",
				"she",
				"they",
				"it",
				"me",
				"us",
				"him",
				"her",
				"them",
				"myself",
				"ourselves",
				"yourself",
				"himself",
				"herself",
				"itself",
				"oneself",
				"oneselves",
				"my",
				"our",
				"your",
				"his",
				"their",
				"its",
				"mine",
				"yours",
				"hers",
				"theirs",
				"this",
				"that",
				"these",
				"those",
				"all",
				"another",
				"any",
				"anybody",
				"anyone",
				"anything",
				"both",
				"each",
				"other",
				"either",
				"everyone",
				"everybody",
				"everything",
				"most",
				"much",
				"nothing",
				"nobody",
				"none",
				"one",
				"others",
				"some",
				"somebody",
				"someone",
				"something",
				"what",
				"whatever",
				"which",
				"whichever",
				"who",
				"whoever",
				"whom",
				"whomever",
				"whose"
			};
			this._pronounList = strs1;
			Dictionary<string, string> strs2 = new Dictionary<string, string>()
			{
				{ "brother", "brothers" },
				{ "child", "children" },
				{ "cow", "cows" },
				{ "ephemeris", "ephemerides" },
				{ "genie", "genies" },
				{ "money", "moneys" },
				{ "mongoose", "mongooses" },
				{ "mythos", "mythoi" },
				{ "octopus", "octopuses" },
				{ "ox", "oxen" },
				{ "soliloquy", "soliloquies" },
				{ "trilby", "trilbys" },
				{ "crisis", "crises" },
				{ "synopsis", "synopses" },
				{ "rose", "roses" },
				{ "gas", "gases" },
				{ "bus", "buses" },
				{ "axis", "axes" },
				{ "memo", "memos" },
				{ "casino", "casinos" },
				{ "silo", "silos" },
				{ "stereo", "stereos" },
				{ "studio", "studios" },
				{ "lens", "lenses" },
				{ "alias", "aliases" },
				{ "pie", "pies" },
				{ "corpus", "corpora" },
				{ "viscus", "viscera" },
				{ "hippopotamus", "hippopotami" },
				{ "trace", "traces" },
				{ "person", "people" },
				{ "chilli", "chillies" },
				{ "analysis", "analyses" },
				{ "basis", "bases" },
				{ "neurosis", "neuroses" },
				{ "oasis", "oases" },
				{ "synthesis", "syntheses" },
				{ "thesis", "theses" },
				{ "pneumonoultramicroscopicsilicovolcanoconiosis", "pneumonoultramicroscopicsilicovolcanoconioses" },
				{ "status", "statuses" },
				{ "prospectus", "prospectuses" },
				{ "change", "changes" },
				{ "lie", "lies" },
				{ "calorie", "calories" },
				{ "freebie", "freebies" },
				{ "case", "cases" },
				{ "house", "houses" },
				{ "valve", "valves" },
				{ "cloth", "clothes" }
			};
			this._irregularPluralsList = strs2;
			Dictionary<string, string> strs3 = new Dictionary<string, string>()
			{
				{ "alumna", "alumnae" },
				{ "alga", "algae" },
				{ "vertebra", "vertebrae" },
				{ "codex", "codices" },
				{ "murex", "murices" },
				{ "silex", "silices" },
				{ "aphelion", "aphelia" },
				{ "hyperbaton", "hyperbata" },
				{ "perihelion", "perihelia" },
				{ "asyndeton", "asyndeta" },
				{ "noumenon", "noumena" },
				{ "phenomenon", "phenomena" },
				{ "criterion", "criteria" },
				{ "organon", "organa" },
				{ "prolegomenon", "prolegomena" },
				{ "agendum", "agenda" },
				{ "datum", "data" },
				{ "extremum", "extrema" },
				{ "bacterium", "bacteria" },
				{ "desideratum", "desiderata" },
				{ "stratum", "strata" },
				{ "candelabrum", "candelabra" },
				{ "erratum", "errata" },
				{ "ovum", "ova" },
				{ "forum", "fora" },
				{ "addendum", "addenda" },
				{ "stadium", "stadia" },
				{ "automaton", "automata" },
				{ "polyhedron", "polyhedra" }
			};
			this._assimilatedClassicalInflectionList = strs3;
			Dictionary<string, string> strs4 = new Dictionary<string, string>()
			{
				{ "albino", "albinos" },
				{ "generalissimo", "generalissimos" },
				{ "manifesto", "manifestos" },
				{ "archipelago", "archipelagos" },
				{ "ghetto", "ghettos" },
				{ "medico", "medicos" },
				{ "armadillo", "armadillos" },
				{ "guano", "guanos" },
				{ "octavo", "octavos" },
				{ "commando", "commandos" },
				{ "inferno", "infernos" },
				{ "photo", "photos" },
				{ "ditto", "dittos" },
				{ "jumbo", "jumbos" },
				{ "pro", "pros" },
				{ "dynamo", "dynamos" },
				{ "lingo", "lingos" },
				{ "quarto", "quartos" },
				{ "embryo", "embryos" },
				{ "lumbago", "lumbagos" },
				{ "rhino", "rhinos" },
				{ "fiasco", "fiascos" },
				{ "magneto", "magnetos" },
				{ "stylo", "stylos" }
			};
			this._oSuffixList = strs4;
			Dictionary<string, string> strs5 = new Dictionary<string, string>()
			{
				{ "stamen", "stamina" },
				{ "foramen", "foramina" },
				{ "lumen", "lumina" },
				{ "anathema", "anathemata" },
				{ "enema", "enemata" },
				{ "oedema", "oedemata" },
				{ "bema", "bemata" },
				{ "enigma", "enigmata" },
				{ "sarcoma", "sarcomata" },
				{ "carcinoma", "carcinomata" },
				{ "gumma", "gummata" },
				{ "schema", "schemata" },
				{ "charisma", "charismata" },
				{ "lemma", "lemmata" },
				{ "soma", "somata" },
				{ "diploma", "diplomata" },
				{ "lymphoma", "lymphomata" },
				{ "stigma", "stigmata" },
				{ "dogma", "dogmata" },
				{ "magma", "magmata" },
				{ "stoma", "stomata" },
				{ "drama", "dramata" },
				{ "melisma", "melismata" },
				{ "trauma", "traumata" },
				{ "edema", "edemata" },
				{ "miasma", "miasmata" },
				{ "abscissa", "abscissae" },
				{ "formula", "formulae" },
				{ "medusa", "medusae" },
				{ "amoeba", "amoebae" },
				{ "hydra", "hydrae" },
				{ "nebula", "nebulae" },
				{ "antenna", "antennae" },
				{ "hyperbola", "hyperbolae" },
				{ "nova", "novae" },
				{ "aurora", "aurorae" },
				{ "lacuna", "lacunae" },
				{ "parabola", "parabolae" },
				{ "apex", "apices" },
				{ "latex", "latices" },
				{ "vertex", "vertices" },
				{ "cortex", "cortices" },
				{ "pontifex", "pontifices" },
				{ "vortex", "vortices" },
				{ "index", "indices" },
				{ "simplex", "simplices" },
				{ "iris", "irides" },
				{ "clitoris", "clitorides" },
				{ "alto", "alti" },
				{ "contralto", "contralti" },
				{ "soprano", "soprani" },
				{ "basso", "bassi" },
				{ "crescendo", "crescendi" },
				{ "tempo", "tempi" },
				{ "canto", "canti" },
				{ "solo", "soli" },
				{ "aquarium", "aquaria" },
				{ "interregnum", "interregna" },
				{ "quantum", "quanta" },
				{ "compendium", "compendia" },
				{ "lustrum", "lustra" },
				{ "rostrum", "rostra" },
				{ "consortium", "consortia" },
				{ "maximum", "maxima" },
				{ "spectrum", "spectra" },
				{ "cranium", "crania" },
				{ "medium", "media" },
				{ "speculum", "specula" },
				{ "curriculum", "curricula" },
				{ "memorandum", "memoranda" },
				{ "stadium", "stadia" },
				{ "dictum", "dicta" },
				{ "millenium", "millenia" },
				{ "trapezium", "trapezia" },
				{ "emporium", "emporia" },
				{ "minimum", "minima" },
				{ "ultimatum", "ultimata" },
				{ "enconium", "enconia" },
				{ "momentum", "momenta" },
				{ "vacuum", "vacua" },
				{ "gymnasium", "gymnasia" },
				{ "optimum", "optima" },
				{ "velum", "vela" },
				{ "honorarium", "honoraria" },
				{ "phylum", "phyla" },
				{ "focus", "foci" },
				{ "nimbus", "nimbi" },
				{ "succubus", "succubi" },
				{ "fungus", "fungi" },
				{ "nucleolus", "nucleoli" },
				{ "torus", "tori" },
				{ "genius", "genii" },
				{ "radius", "radii" },
				{ "umbilicus", "umbilici" },
				{ "incubus", "incubi" },
				{ "stylus", "styli" },
				{ "uterus", "uteri" },
				{ "stimulus", "stimuli" },
				{ "apparatus", "apparatus" },
				{ "impetus", "impetus" },
				{ "prospectus", "prospectus" },
				{ "cantus", "cantus" },
				{ "nexus", "nexus" },
				{ "sinus", "sinus" },
				{ "coitus", "coitus" },
				{ "plexus", "plexus" },
				{ "status", "status" },
				{ "hiatus", "hiatus" },
				{ "afreet", "afreeti" },
				{ "afrit", "afriti" },
				{ "efreet", "efreeti" },
				{ "cherub", "cherubim" },
				{ "goy", "goyim" },
				{ "seraph", "seraphim" },
				{ "alumnus", "alumni" }
			};
			this._classicalInflectionList = strs5;
			List<string> strs6 = new List<string>()
			{
				"they",
				"them",
				"their",
				"have",
				"were",
				"yourself",
				"are"
			};
			this._knownConflictingPluralList = strs6;
			Dictionary<string, string> strs7 = new Dictionary<string, string>()
			{
				{ "house", "houses" },
				{ "case", "cases" },
				{ "enterprise", "enterprises" },
				{ "purchase", "purchases" },
				{ "surprise", "surprises" },
				{ "release", "releases" },
				{ "disease", "diseases" },
				{ "promise", "promises" },
				{ "refuse", "refuses" },
				{ "whose", "whoses" },
				{ "phase", "phases" },
				{ "noise", "noises" },
				{ "nurse", "nurses" },
				{ "rose", "roses" },
				{ "franchise", "franchises" },
				{ "supervise", "supervises" },
				{ "farmhouse", "farmhouses" },
				{ "suitcase", "suitcases" },
				{ "recourse", "recourses" },
				{ "impulse", "impulses" },
				{ "license", "licenses" },
				{ "diocese", "dioceses" },
				{ "excise", "excises" },
				{ "demise", "demises" },
				{ "blouse", "blouses" },
				{ "bruise", "bruises" },
				{ "misuse", "misuses" },
				{ "curse", "curses" },
				{ "prose", "proses" },
				{ "purse", "purses" },
				{ "goose", "gooses" },
				{ "tease", "teases" },
				{ "poise", "poises" },
				{ "vase", "vases" },
				{ "fuse", "fuses" },
				{ "muse", "muses" },
				{ "slaughterhouse", "slaughterhouses" },
				{ "clearinghouse", "clearinghouses" },
				{ "endonuclease", "endonucleases" },
				{ "steeplechase", "steeplechases" },
				{ "metamorphose", "metamorphoses" },
				{ "intercourse", "intercourses" },
				{ "commonsense", "commonsenses" },
				{ "intersperse", "intersperses" },
				{ "merchandise", "merchandises" },
				{ "phosphatase", "phosphatases" },
				{ "summerhouse", "summerhouses" },
				{ "watercourse", "watercourses" },
				{ "catchphrase", "catchphrases" },
				{ "compromise", "compromises" },
				{ "greenhouse", "greenhouses" },
				{ "lighthouse", "lighthouses" },
				{ "paraphrase", "paraphrases" },
				{ "mayonnaise", "mayonnaises" },
				{ "racecourse", "racecourses" },
				{ "apocalypse", "apocalypses" },
				{ "courthouse", "courthouses" },
				{ "powerhouse", "powerhouses" },
				{ "storehouse", "storehouses" },
				{ "glasshouse", "glasshouses" },
				{ "hypotenuse", "hypotenuses" },
				{ "peroxidase", "peroxidases" },
				{ "pillowcase", "pillowcases" },
				{ "roundhouse", "roundhouses" },
				{ "streetwise", "streetwises" },
				{ "expertise", "expertises" },
				{ "discourse", "discourses" },
				{ "warehouse", "warehouses" },
				{ "staircase", "staircases" },
				{ "workhouse", "workhouses" },
				{ "briefcase", "briefcases" },
				{ "clubhouse", "clubhouses" },
				{ "clockwise", "clockwises" },
				{ "concourse", "concourses" },
				{ "playhouse", "playhouses" },
				{ "turquoise", "turquoises" },
				{ "boathouse", "boathouses" },
				{ "cellulose", "celluloses" },
				{ "epitomise", "epitomises" },
				{ "gatehouse", "gatehouses" },
				{ "grandiose", "grandioses" },
				{ "menopause", "menopauses" },
				{ "penthouse", "penthouses" },
				{ "racehorse", "racehorses" },
				{ "transpose", "transposes" },
				{ "almshouse", "almshouses" },
				{ "customise", "customises" },
				{ "footloose", "footlooses" },
				{ "galvanise", "galvanises" },
				{ "princesse", "princesses" },
				{ "universe", "universes" },
				{ "workhorse", "workhorses" }
			};
			this._wordsEndingWithSeList = strs7;
			Dictionary<string, string> strs8 = new Dictionary<string, string>()
			{
				{ "analysis", "analyses" },
				{ "crisis", "crises" },
				{ "basis", "bases" },
				{ "atherosclerosis", "atheroscleroses" },
				{ "electrophoresis", "electrophoreses" },
				{ "psychoanalysis", "psychoanalyses" },
				{ "photosynthesis", "photosyntheses" },
				{ "amniocentesis", "amniocenteses" },
				{ "metamorphosis", "metamorphoses" },
				{ "toxoplasmosis", "toxoplasmoses" },
				{ "endometriosis", "endometrioses" },
				{ "tuberculosis", "tuberculoses" },
				{ "pathogenesis", "pathogeneses" },
				{ "osteoporosis", "osteoporoses" },
				{ "parenthesis", "parentheses" },
				{ "anastomosis", "anastomoses" },
				{ "peristalsis", "peristalses" },
				{ "hypothesis", "hypotheses" },
				{ "antithesis", "antitheses" },
				{ "apotheosis", "apotheoses" },
				{ "thrombosis", "thromboses" },
				{ "diagnosis", "diagnoses" },
				{ "synthesis", "syntheses" },
				{ "paralysis", "paralyses" },
				{ "prognosis", "prognoses" },
				{ "cirrhosis", "cirrhoses" },
				{ "sclerosis", "scleroses" },
				{ "psychosis", "psychoses" },
				{ "apoptosis", "apoptoses" },
				{ "symbiosis", "symbioses" }
			};
			this._wordsEndingWithSisList = strs8;
			base.Culture = new CultureInfo("en");
			this._userDictionary = new BidirectionalDictionary<string, string>();
			this._irregularPluralsPluralizationService = new StringBidirectionalDictionary(this._irregularPluralsList);
			this._assimilatedClassicalInflectionPluralizationService = new StringBidirectionalDictionary(this._assimilatedClassicalInflectionList);
			this._oSuffixPluralizationService = new StringBidirectionalDictionary(this._oSuffixList);
			this._classicalInflectionPluralizationService = new StringBidirectionalDictionary(this._classicalInflectionList);
			this._wordsEndingWithSePluralizationService = new StringBidirectionalDictionary(this._wordsEndingWithSeList);
			this._wordsEndingWithSisPluralizationService = new StringBidirectionalDictionary(this._wordsEndingWithSisList);
			this._irregularVerbPluralizationService = new StringBidirectionalDictionary(this._irregularVerbList);
			this._knownSingluarWords = new List<string>(this._irregularPluralsList.Keys.Concat<string>(this._assimilatedClassicalInflectionList.Keys).Concat<string>(this._oSuffixList.Keys).Concat<string>(this._classicalInflectionList.Keys).Concat<string>(this._irregularVerbList.Keys).Concat<string>(this._uninflectiveWords).Except<string>(this._knownConflictingPluralList));
			this._knownPluralWords = new List<string>(this._irregularPluralsList.Values.Concat<string>(this._assimilatedClassicalInflectionList.Values).Concat<string>(this._oSuffixList.Values).Concat<string>(this._classicalInflectionList.Values).Concat<string>(this._irregularVerbList.Values).Concat<string>(this._uninflectiveWords));
		}

		

		public void AddWord(string singular, string plural)
		{
			this._userDictionary.AddValue(singular, plural);
		}

		private string Capitalize(string word, Func<string, string> action)
		{
			string str = action(word);
			if (!this.IsCapitalized(word))
			{
				return str;
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder(str.Length);
			stringBuilder.Append(char.ToUpperInvariant(str[0]));
			stringBuilder.Append(str.Substring(1));
			return stringBuilder.ToString();
		}

		private string GetSuffixWord(string word, out string prefixWord)
		{
			int num = word.LastIndexOf(' ');
			prefixWord = word.Substring(0, num + 1);
			return word.Substring(num + 1);
		}

		private string InternalPluralize(string word)
		{
			string str;
			string str1;
			if (this._userDictionary.ExistsInFirst(word))
			{
				return this._userDictionary.GetSecondValue(word);
			}
			if (this.IsNoOpWord(word))
			{
				return word;
			}
			string suffixWord = this.GetSuffixWord(word, out str);
			if (this.IsNoOpWord(suffixWord))
			{
				return string.Concat(str, suffixWord);
			}
			if (this.IsUninflective(suffixWord))
			{
				return string.Concat(str, suffixWord);
			}
			if (this._knownPluralWords.Contains(suffixWord.ToLowerInvariant()) || this.IsPlural(suffixWord))
			{
				return string.Concat(str, suffixWord);
			}
			if (this._irregularPluralsPluralizationService.ExistsInFirst(suffixWord))
			{
				return string.Concat(str, this._irregularPluralsPluralizationService.GetSecondValue(suffixWord));
			}
			List<string> strs = new List<string>()
			{
				"man"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs, (string s)=>string.Concat(s.Remove(s.Length - 2, 2), "en"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs1 = new List<string>()
			{
				"louse",
				"mouse"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs1, (string s) => string.Concat(s.Remove(s.Length - 4, 4), "ice"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs2 = new List<string>()
			{
				"tooth"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs2, (string s) => string.Concat(s.Remove(s.Length - 4, 4), "eeth"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs3 = new List<string>()
			{
				"goose"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs3, (string s) => string.Concat(s.Remove(s.Length - 4, 4), "eese"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs4 = new List<string>()
			{
				"foot"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs4, (string s) => string.Concat(s.Remove(s.Length - 3, 3), "eet"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs5 = new List<string>()
			{
				"zoon"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs5, (string s) => string.Concat(s.Remove(s.Length - 3, 3), "oa"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs6 = new List<string>()
			{
				"cis",
				"sis",
				"xis"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs6, (string s) => string.Concat(s.Remove(s.Length - 2, 2), "es"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			if (this._assimilatedClassicalInflectionPluralizationService.ExistsInFirst(suffixWord))
			{
				return string.Concat(str, this._assimilatedClassicalInflectionPluralizationService.GetSecondValue(suffixWord));
			}
			if (this._classicalInflectionPluralizationService.ExistsInFirst(suffixWord))
			{
				return string.Concat(str, this._classicalInflectionPluralizationService.GetSecondValue(suffixWord));
			}
			List<string> strs7 = new List<string>()
			{
				"trix"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs7, (string s) => string.Concat(s.Remove(s.Length - 1, 1), "ces"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs8 = new List<string>()
			{
				"eau",
				"ieu"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs8, (string s) => string.Concat(s, "x"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs9 = new List<string>()
			{
				"inx",
				"anx",
				"ynx"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs9, (string s) => string.Concat(s.Remove(s.Length - 1, 1), "ges"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs10 = new List<string>()
			{
				"ch",
				"sh",
				"ss"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs10, (string s) => string.Concat(s, "es"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs11 = new List<string>()
			{
				"alf",
				"elf",
				"olf",
				"eaf",
				"arf"
			};
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs11, (string s) => {
				if(s.EndsWith("deaf",StringComparison.CurrentCultureIgnoreCase))
				 	{return s;} 
				 return string.Concat(s.Remove(s.Length - 4, 4), "ice");}, base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs12 = new List<string>()
			{
				"nife",
				"life",
				"wife"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs12, (string s)=>string.Concat(s.Remove(s.Length - 2, 2), "ves"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			List<string> strs13 = new List<string>()
			{
				"ay",
				"ey",
				"iy",
				"oy",
				"uy"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs13, (string s)=>string.Concat(s, "s"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			if (suffixWord.EndsWith("y"))
			{
				return string.Concat(str, suffixWord.Remove(suffixWord.Length - 1, 1), "ies");
			}
			if (this._oSuffixPluralizationService.ExistsInFirst(suffixWord))
			{
				return string.Concat(str, this._oSuffixPluralizationService.GetSecondValue(suffixWord));
			}
			List<string> strs14 = new List<string>()
			{
				"ao",
				"eo",
				"io",
				"oo",
				"uo"
			};
			
			if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs14, (string s)=>string.Concat(s, "s"), base.Culture, out str1))
			{
				return string.Concat(str, str1);
			}
			if (suffixWord.EndsWith("o"))
			{
				return string.Concat(str, suffixWord, "es");
			}
			if (suffixWord.EndsWith("x"))
			{
				return string.Concat(str, suffixWord, "es");
			}
			return string.Concat(str, suffixWord, "s");
		}

		private string InternalSingularize(string word)
		{
			string str;
			string str1;
			if (this._userDictionary.ExistsInSecond(word))
			{
				return this._userDictionary.GetFirstValue(word);
			}
			if (this.IsNoOpWord(word))
			{
				return word;
			}
			string suffixWord = this.GetSuffixWord(word, out str);
			if (!this.IsNoOpWord(suffixWord))
			{
				if (this.IsUninflective(suffixWord))
				{
					return string.Concat(str, suffixWord);
				}
				if (this._knownSingluarWords.Contains(suffixWord.ToLowerInvariant()))
				{
					return string.Concat(str, suffixWord);
				}
				if (this._irregularVerbPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._irregularVerbPluralizationService.GetFirstValue(suffixWord));
				}
				if (this._irregularPluralsPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._irregularPluralsPluralizationService.GetFirstValue(suffixWord));
				}
				if (this._wordsEndingWithSisPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._wordsEndingWithSisPluralizationService.GetFirstValue(suffixWord));
				}
				if (this._wordsEndingWithSePluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._wordsEndingWithSePluralizationService.GetFirstValue(suffixWord));
				}
				List<string> strs = new List<string>()
				{
					"men"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs, (string s)=>string.Concat(s.Remove(s.Length - 2, 2), "an"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs1 = new List<string>()
				{
					"lice",
					"mice"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs1, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "ouse"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs2 = new List<string>()
				{
					"teeth"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs2, (string s)=>string.Concat(s.Remove(s.Length - 4, 4), "ooth"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs3 = new List<string>()
				{
					"geese"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs3, (string s)=>string.Concat(s.Remove(s.Length - 4, 4), "oose"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs4 = new List<string>()
				{
					"feet"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs4, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "oot"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs5 = new List<string>()
				{
					"zoa"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs5, (string s)=>string.Concat(s.Remove(s.Length - 2, 2), "oon"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs6 = new List<string>()
				{
					"ches",
					"shes",
					"sses"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs6, (string s)=>s.Remove(s.Length - 2, 2), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				if (this._assimilatedClassicalInflectionPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._assimilatedClassicalInflectionPluralizationService.GetFirstValue(suffixWord));
				}
				if (this._classicalInflectionPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._classicalInflectionPluralizationService.GetFirstValue(suffixWord));
				}
				List<string> strs7 = new List<string>()
				{
					"trices"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs7, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "x"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs8 = new List<string>()
				{
					"eaux",
					"ieux"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs8, (string s)=>s.Remove(s.Length - 1, 1), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs9 = new List<string>()
				{
					"inges",
					"anges",
					"ynges"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs9, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "x"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs10 = new List<string>()
				{
					"alves",
					"elves",
					"olves",
					"eaves",
					"arves"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs10, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "f"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs11 = new List<string>()
				{
					"nives",
					"lives",
					"wives"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs11, (string s)=>string.Concat(s.Remove(s.Length - 3, 3), "fe"), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs12 = new List<string>()
				{
					"ays",
					"eys",
					"iys",
					"oys",
					"uys"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs12, (string s)=>s.Remove(s.Length - 1, 1), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				if (suffixWord.EndsWith("ies"))
				{
					return string.Concat(str, suffixWord.Remove(suffixWord.Length - 3, 3), "y");
				}
				if (this._oSuffixPluralizationService.ExistsInSecond(suffixWord))
				{
					return string.Concat(str, this._oSuffixPluralizationService.GetFirstValue(suffixWord));
				}
				List<string> strs13 = new List<string>()
				{
					"aos",
					"eos",
					"ios",
					"oos",
					"uos"
				};
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs13, (string x) => x.Remove(x.Length - 1, 1), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs14 = new List<string>()
				{
					"ces"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs14, (string s)=>s.Remove(s.Length - 1, 1), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				List<string> strs15 = new List<string>()
				{
					"ces",
					"ses",
					"xes"
				};
				
				if (PluralizationServiceUtil.TryInflectOnSuffixInWord(suffixWord, strs15, (string s)=>s.Remove(s.Length - 2, 2), base.Culture, out str1))
				{
					return string.Concat(str, str1);
				}
				if (suffixWord.EndsWith("oes"))
				{
					return string.Concat(str, suffixWord.Remove(suffixWord.Length - 2, 2));
				}
				if (suffixWord.EndsWith("ss"))
				{
					return string.Concat(str, suffixWord);
				}
				if (suffixWord.EndsWith("s"))
				{
					return string.Concat(str, suffixWord.Remove(suffixWord.Length - 1, 1));
				}
			}
			return string.Concat(str, suffixWord);
		}

		private bool IsAlphabets(string word)
		{
			if (string.IsNullOrEmpty(word.Trim()) || !word.Equals(word.Trim()))
			{
				return false;
			}
			return !Regex.IsMatch(word, "[^a-zA-Z\\s]");
		}

		private bool IsCapitalized(string word)
		{
			if (string.IsNullOrEmpty(word))
			{
				return false;
			}
			return char.IsUpper(word, 0);
		}

		private bool IsNoOpWord(string word)
		{
			if (this.IsAlphabets(word) && word.Length > 1 && !this._pronounList.Contains(word.ToLowerInvariant()))
			{
				return false;
			}
			return true;
		}

		public override bool IsPlural(string word)
		{
			if (!this._userDictionary.ExistsInSecond(word))
			{
				if (this._userDictionary.ExistsInFirst(word))
				{
					return false;
				}
				if (this.IsUninflective(word) || this._knownPluralWords.Contains(word.ToLower()))
				{
					return true;
				}
				if (this.Singularize(word).Equals(word))
				{
					return false;
				}
			}
			return true;
		}

		public override bool IsSingular(string word)
		{
			if (this._userDictionary.ExistsInFirst(word))
			{
				return true;
			}
			if (this._userDictionary.ExistsInSecond(word))
			{
				return false;
			}
			if (this.IsUninflective(word) || this._knownSingluarWords.Contains(word.ToLower()))
			{
				return true;
			}
			if (this.IsNoOpWord(word))
			{
				return false;
			}
			return this.Singularize(word).Equals(word);
		}

		private bool IsUninflective(string word)
		{
			if (!PluralizationServiceUtil.DoesWordContainSuffix(word, this._uninflectiveSuffixes, base.Culture) && (word.ToLower().Equals(word) || !word.EndsWith("ese")) && !this._uninflectiveWords.Contains<string>(word.ToLowerInvariant()))
			{
				return false;
			}
			return true;
		}

		public override string Pluralize(string word)
		{
			return this.Capitalize(word, new Func<string, string>(this.InternalPluralize));
		}

		public override string Singularize(string word)
		{
			return this.Capitalize(word, new Func<string, string>(this.InternalSingularize));
		}
	}
}