using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FogBugzApi
{
    public class Case : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Area;
        public string Number;
        public string Category;
        public string PriorityInteger;
        public string PriorityName;
        public string ProjectName;
        public string Status;
        public string Title;

        public List<String> Changeset;
        public Case(XElement inputXml)
        {
            Area = getCaseTagValue(inputXml, "sArea");
            Number = getCaseTagValue(inputXml, "ixBug");
            Category = getCaseTagValue(inputXml, "sCategory");
            PriorityInteger = getCaseTagValue(inputXml, "ixPriority");
            PriorityName = getCaseTagValue(inputXml, "sPriority");
            ProjectName = getCaseTagValue(inputXml, "sProject");
            Status = getCaseTagValue(inputXml, "sStatus");
            Title = getCaseTagValue(inputXml, "sTitle");
        }

        /// <summary>
        /// Stores the case variable as a partial http string (ex. &sArea="Area String")
        /// </summary>
        /// <param name="inputXml"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string getCaseTagValue(XElement inputXml, string tag)
        {
            var sb = new StringBuilder();
            sb.Append('&');
            sb.Append(tag + '=');
            sb.Append(inputXml.Elements().Where(x => x.Name.LocalName == tag).FirstOrDefault()?.Value);
            return sb.ToString();
        }

        /// <summary>
        /// Intercept Fody inject OnPropertyChanged so we can create a changeset.
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnPropertyChanged(string propertyName, object before, object after)
        {
            Changeset ??= new List<string>();
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
            if (after?.ToString() != null) Changeset.Add(after.ToString());
        }
    }
}