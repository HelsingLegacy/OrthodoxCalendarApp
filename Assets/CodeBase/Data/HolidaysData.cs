using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeBase.Data
{
    public class HolidayInfo
    {
        [JsonProperty("postId")]
        public int PostId;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("holidayName")]
        public string HolidayName;

        [JsonProperty("holidayDate")]
        public DateTime HolidayDate;

        [JsonProperty("weekName")]
        public string WeekName;

        [JsonProperty("holidayColor")]
        public string HolidayColor;

        [JsonProperty("liturgyRecommendations")]
        public string LiturgyRecommendations;

        [JsonProperty("content")]
        public string Content;

        [JsonProperty("mainImage")]
        public string MainImage;

        [JsonProperty("holidayCategory")]
        public List<HolidayCategory> HolidayCategory;

        [JsonProperty("holidayFast")]
        public HolidayFast HolidayFast;

        [JsonProperty("holidayFastName")]
        public HolidayFastName HolidayFastName;

        [JsonProperty("holidaySpecial")]
        public List<HolidaySpecial> HolidaySpecial;

        [JsonProperty("holidayDress")]
        public List<HolidayDress> HolidayDress;

        [JsonProperty("dayIcons")]
        public Dictionary<string, string> DayIcons;
    }

    public class HolidayCategory
    {
        [JsonProperty("term_id")]
        public int TermId;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("term_group")]
        public int TermGroup;

        [JsonProperty("term_taxonomy_id")]
        public int TermTaxonomyId;

        [JsonProperty("taxonomy")]
        public string Taxonomy;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("parent")]
        public int Parent;

        [JsonProperty("count")]
        public int Count;

        [JsonProperty("filter")]
        public string Filter;
    }

    public class HolidayFast
    {
        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("value")]
        public string Value;
    }

    public class HolidayFastName
    {
        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("value")]
        public string Value;
    }

    public class HolidaySpecial
    {
        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("value")]
        public string Value;
    }

    public class HolidayDress
    {
        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("value")]
        public string Value;
    }
}