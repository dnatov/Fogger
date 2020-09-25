using System;
using System.Collections.Generic;
using System.Text;

namespace Fogger.Models
{
    public class Filter
    {
        private FilterType _FilterType;
        private string _sFilter;
        private string _Description;
        private bool _Current;

        public FilterType FilterType { get => _FilterType; }
        public string sFilter { get => _sFilter; set => _sFilter = value; }
        public string Description { get => _Description; set => _Description = value; }
        public bool Current { get => _Current; }

        public Filter(FilterType filterType, string filterString, string description)
        {
            _FilterType = filterType;
            _sFilter = filterString;
            _Description = description;
        }

        public Filter(FilterType filterType, string filterString, string description, bool current)
        {
            _FilterType = filterType;
            _sFilter = filterString;
            _Description = description;
            _Current = current;
        }

        /// <summary>
        /// Sets the current filter as the selected filter
        /// </summary>
        public void SetCurrent()
        {
            _Current = true;
        }

        /// <summary>
        /// Sets the current filter as the selected filter
        /// </summary>
        public void SetNotCurrent()
        {
            _Current = false;
        }

        /// <summary>
        /// Converts a string from an attribute into a FilterType
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static FilterType StringToFilterType(string attribute)
        {
            switch (attribute)
            {
                case "builtin":
                    return FilterType.BuiltIn;
                case "saved":
                    return FilterType.Saved;
                case "shared":
                    return FilterType.Shared;
                default:
                    throw new Exception("Invalid attribute on filter");
            }
        }
    }

    public enum FilterType
    {
        BuiltIn,    //Default filters
        Saved,      //User-defined filters
        Shared      //Org-defined filters
    }
}
