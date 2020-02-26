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
        public CaseProperty? AreaId;
        public CaseProperty? Category;
        public CaseProperty? Children;
        public CaseProperty? DuplicateCaseIds;
        public CaseProperty? DuplicateOriginal;
        public CaseProperty? EmailAssignedTo;
        public CaseProperty? EventLatestText;
        public CaseProperty? FixForId;
        public CaseProperty? FixForName;
        public CaseProperty? FixForDateTime;
        public CaseProperty? GroupId;
        public CaseProperty? PersonAssignedId;
        public CaseProperty? PersonAssignedName;
        public CaseProperty? PersonClosedId;
        public CaseProperty? PersonLastEditedId;
        public CaseProperty? PersonOpenedId;
        public CaseProperty? PersonResolvedId;
        public CaseProperty? IsOpen;
        public CaseProperty? LatestTextSummary;
        public CaseProperty? Number;
        public CaseProperty? OriginalTitle;
        public CaseProperty? Parent;
        public CaseProperty? PriorityInteger;
        public CaseProperty? PriorityName;
        public CaseProperty? ProjectId;
        public CaseProperty? ProjectName;
        public CaseProperty? Status;

        public CaseProperty? StatusId { get; }

        public CaseProperty? Tags;
        public CaseProperty? Title;

        public List<String> Changeset;
        public Case(XElement inputXml)
        {
            Area = setCaseTagValueString(inputXml, "sArea");
            AreaId = setCaseTagValueString(inputXml, "ixArea");
            Category = setCaseTagValueString(inputXml, "sCategory");
            Children = setCaseTagValueString(inputXml, "ixBugChildren");
            DuplicateCaseIds = setCaseTagValueString(inputXml, "ixBugDuplicates");
            DuplicateOriginal = setCaseTagValueString(inputXml, "ixBugOriginal");
            EmailAssignedTo = setCaseTagValueString(inputXml, "sEmailAssignedTo");
            EventLatestText = setCaseTagValueString(inputXml, "ixBugEventLatestText");
            FixForId = setCaseTagValueString(inputXml, "ixFixFor");
            FixForName = setCaseTagValueString(inputXml, "sFixFor");
            FixForDateTime = setCaseTagValueString(inputXml, "dtFixFor");
            GroupId = setCaseTagValueString(inputXml, "ixGroup");
            IsOpen = setCaseTagValueString(inputXml, "fOpen");
            LatestTextSummary = setCaseTagValueString(inputXml, "sLatestTextSummary");
            Number = setCaseTagValueString(inputXml, "ixBug");
            OriginalTitle = setCaseTagValueString(inputXml, "sOriginalTitle");
            Parent = setCaseTagValueString(inputXml, "ixBugParent");
            PersonAssignedId = setCaseTagValueString(inputXml, "ixPersonAssignedTo");
            PersonAssignedName = setCaseTagValueString(inputXml, "sPersonAssignedTo");
            PersonClosedId = setCaseTagValueString(inputXml, "ixPersonClosedBy");
            PersonLastEditedId = setCaseTagValueString(inputXml, "ixPersonLastEditedBy");
            PersonOpenedId = setCaseTagValueString(inputXml, "ixPersonOpenedBy");
            PersonResolvedId = setCaseTagValueString(inputXml, "ixPersonResolvedBy");
            PriorityInteger = setCaseTagValueString(inputXml, "ixPriority");
            PriorityName = setCaseTagValueString(inputXml, "sPriority");
            ProjectId = setCaseTagValueString(inputXml, "ixProject");
            ProjectName = setCaseTagValueString(inputXml, "sProject");
            Status = setCaseTagValueString(inputXml, "sStatus");
            StatusId = setCaseTagValueString(inputXml, "ixStatus");
            Tags = setCaseTagValueString(inputXml, "tags");
            Title = setCaseTagValueString(inputXml, "sTitle");
        }

        /// <summary>
        /// Stores the case variable as a friendly string and the html command to change it (Ex. "Area1", &sArea=)
        /// </summary>
        /// <param name="inputXml"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private CaseProperty? setCaseTagValueString(XElement inputXml, string tag)
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
            //Intercept property, store it in the changeset, overwrite change if already present
            if (before is CaseProperty)
            {
                Changeset ??= new List<string>();
                var cp = after as CaseProperty?;
                var cpBefore = before as CaseProperty?;
                Changeset.Remove(cpBefore?.HtmlHeader + HttpUtility.HtmlEncode(cpBefore?.Value));
                if (cp?.Value != null) Changeset.Add(cp?.HtmlHeader + HttpUtility.HtmlEncode(cp?.Value));
            }
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}