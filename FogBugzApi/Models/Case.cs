using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace FogBugzApi
{
    public class Case : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CaseProperty? Area;
        public CaseProperty? Number;
        public CaseProperty? Category;
        public CaseProperty? PriorityInteger;
        public CaseProperty? PriorityName;
        public CaseProperty? ProjectName;
        public CaseProperty? Status;
        public CaseProperty? Title;

        public List<String> Changeset;
        public Case(XElement inputXml)
        {
            Area = setCaseTagValue(inputXml, "sArea");
            Number = setCaseTagValue(inputXml, "ixBug");
            Category = setCaseTagValue(inputXml, "sCategory");
            PriorityInteger = setCaseTagValue(inputXml, "ixPriority");
            PriorityName = setCaseTagValue(inputXml, "sPriority");
            ProjectName = setCaseTagValue(inputXml, "sProject");
            Status = setCaseTagValue(inputXml, "sStatus");
            Title = setCaseTagValue(inputXml, "sTitle");
        }

        /// <summary>
        /// Stores the case variable as a partial http string (ex. &sArea="Area String")
        /// </summary>
        /// <param name="inputXml"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private CaseProperty? setCaseTagValue(XElement inputXml, string tag)
        {
            if (inputXml.Elements().Where(x => x.Name.LocalName == tag).FirstOrDefault()?.Value != null)
            {
                var cp = new CaseProperty();
                cp.HtmlHeader = '&' + tag + '=';
                cp.Value = inputXml.Elements().Where(x => x.Name.LocalName == tag).FirstOrDefault()?.Value;
                return cp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Intercept Fody inject OnPropertyChanged so we can create a changeset.
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnPropertyChanged(string propertyName, object before, object after)
        {
            if (before is CaseProperty)
            {
                Changeset ??= new List<string>();
                var cp = after as CaseProperty?;
                if (cp?.Value != null) Changeset.Add(cp?.HtmlHeader + HttpUtility.HtmlEncode(cp?.Value));
            }
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}