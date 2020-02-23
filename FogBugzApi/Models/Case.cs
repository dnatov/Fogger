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
        //TODO: Store html friendly values here
        private string area;
        private string number;
        private string category;
        private string priorityInteger;
        private string priorityName;
        private string projectName;
        private string status;
        private string title;

        public event PropertyChangedEventHandler PropertyChanged;

        //TODO: Store human friendly values here
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
            area = getCaseTagValue(inputXml, "sArea");
            number = getCaseTagValue(inputXml, "ixBug");
            category = getCaseTagValue(inputXml, "sCategory");
            priorityInteger = getCaseTagValue(inputXml, "ixPriority");
            priorityName = getCaseTagValue(inputXml, "sPriority");
            projectName = getCaseTagValue(inputXml, "sProject");
            status = getCaseTagValue(inputXml, "sStatus");
            title = getCaseTagValue(inputXml, "sTitle");
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