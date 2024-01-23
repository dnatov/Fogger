using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Fogger.Models
{
    public class Case : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CaseProperty Area;
        public CaseProperty AreaId;
        public CaseProperty Category;
        public CaseProperty CategoryId;
        public CaseProperty Children;
        public CaseProperty Computer;
        public CaseProperty CustomerEmail;
        public CaseProperty DateClosed;
        public CaseProperty DateDue;
        public CaseProperty DateLastOccurrence;
        public CaseProperty DateLastViewed;
        public CaseProperty DateOpened;
        public CaseProperty DateResolved;
        public CaseProperty DiscussionTopicId;
        public CaseProperty DuplicateCaseIds;
        public CaseProperty DuplicateOriginal;
        public CaseProperty EmailAssignedTo;
        public CaseProperty EstimateHoursCurrent;
        public CaseProperty EstimateHoursElapsed;
        public CaseProperty EstimateHoursOriginal;
        public CaseProperty EventLatestText;
        public CaseProperty FixForId;
        public CaseProperty FixForName;
        public CaseProperty FixForOrder;
        public CaseProperty FixForDateTime;
        public CaseProperty GroupId;
        public CaseProperty PersonAssignedId;
        public CaseProperty PersonAssignedName;
        public CaseProperty PersonClosedId;
        public CaseProperty PersonLastEditedId;
        public CaseProperty PersonOpenedId;
        public CaseProperty PersonResolvedId;
        public CaseProperty IsOpen;
        public CaseProperty IsForwarded;
        public CaseProperty IsRepliedTo;
        public CaseProperty IsScoutReportingStopped;
        public CaseProperty IsSubscribed;
        public CaseProperty LatestTextSummary;
        public CaseProperty LatestBugEvent;
        public CaseProperty LatestBugEventWhenLastViewed;
        public CaseProperty LastUpdated;
        public CaseProperty Mailbox;
        public CaseProperty Number;
        public CaseProperty OriginalTitle;
        public CaseProperty Occurances;
        public CaseProperty Parent;
        public CaseProperty PriorityInteger;
        public CaseProperty PriorityName;
        public CaseProperty ProjectId;
        public CaseProperty ProjectName;
        public CaseProperty RelatedBugIds;
        public CaseProperty ReleaseNotes;
        public CaseProperty ScoutDescription;
        public CaseProperty ScoutMessage;
        public CaseProperty Status;
        public CaseProperty StatusId;
        public CaseProperty StoryPoints;
        public CaseProperty Tags;
        public CaseProperty Ticket;
        public CaseProperty Title;
        public CaseProperty Version;
        public List<String> Changeset;
        public Case(XElement inputXml)
        {
            Area =                          setCaseTagValueString(inputXml, CaseHtmlValue.Area);                     
            AreaId =                        setCaseTagValueString(inputXml, CaseHtmlValue.AreaId);                   
            Category =                      setCaseTagValueString(inputXml, CaseHtmlValue.Category);                 
            CategoryId =                    setCaseTagValueString(inputXml, CaseHtmlValue.CategoryId);               
            Children =                      setCaseTagValueString(inputXml, CaseHtmlValue.Children);                 
            Computer =                      setCaseTagValueString(inputXml, CaseHtmlValue.Computer);                 
            CustomerEmail =                 setCaseTagValueString(inputXml, CaseHtmlValue.CustomerEmail);            
            DateClosed =                    setCaseTagValueString(inputXml, CaseHtmlValue.DateClosed);               
            DateDue =                       setCaseTagValueString(inputXml, CaseHtmlValue.DateDue);                  
            DateLastOccurrence =            setCaseTagValueString(inputXml, CaseHtmlValue.DateLastOccurrence);       
            DateLastViewed =                setCaseTagValueString(inputXml, CaseHtmlValue.DateLastViewed);           
            DateOpened =                    setCaseTagValueString(inputXml, CaseHtmlValue.DateOpened);               
            DateResolved =                  setCaseTagValueString(inputXml, CaseHtmlValue.DateResolved);             
            DiscussionTopicId =             setCaseTagValueString(inputXml, CaseHtmlValue.DiscussionTopicId);        
            DuplicateCaseIds =              setCaseTagValueString(inputXml, CaseHtmlValue.DuplicateCaseIds);         
            DuplicateOriginal =             setCaseTagValueString(inputXml, CaseHtmlValue.DuplicateOriginal);        
            EmailAssignedTo =               setCaseTagValueString(inputXml, CaseHtmlValue.EmailAssignedTo);          
            EstimateHoursCurrent =          setCaseTagValueString(inputXml, CaseHtmlValue.EstimateHoursCurrent);     
            EstimateHoursElapsed =          setCaseTagValueString(inputXml, CaseHtmlValue.EstimateHoursElapsed);     
            EstimateHoursOriginal =         setCaseTagValueString(inputXml, CaseHtmlValue.EstimateHoursOriginal);    
            EventLatestText =               setCaseTagValueString(inputXml, CaseHtmlValue.EventLatestText);          
            FixForDateTime =                setCaseTagValueString(inputXml, CaseHtmlValue.FixForDateTime);           
            FixForId =                      setCaseTagValueString(inputXml, CaseHtmlValue.FixForId);                 
            FixForName =                    setCaseTagValueString(inputXml, CaseHtmlValue.FixForName);               
            FixForOrder =                   setCaseTagValueString(inputXml, CaseHtmlValue.FixForOrder);              
            GroupId =                       setCaseTagValueString(inputXml, CaseHtmlValue.GroupId);                  
            IsOpen =                        setCaseTagValueString(inputXml, CaseHtmlValue.IsOpen);                   
            IsForwarded =                   setCaseTagValueString(inputXml, CaseHtmlValue.IsForwarded);              
            IsRepliedTo =                   setCaseTagValueString(inputXml, CaseHtmlValue.IsRepliedTo);              
            IsScoutReportingStopped =       setCaseTagValueString(inputXml, CaseHtmlValue.IsScoutReportingStopped);  
            IsSubscribed =                  setCaseTagValueString(inputXml, CaseHtmlValue.IsSubscribed);             
            LatestTextSummary =             setCaseTagValueString(inputXml, CaseHtmlValue.LatestTextSummary);        
            LatestBugEvent =                setCaseTagValueString(inputXml, CaseHtmlValue.LatestBugEvent);
            LatestBugEventWhenLastViewed =  setCaseTagValueString(inputXml, CaseHtmlValue.LatestBugEventWhenLastViewed);
            LastUpdated =                   setCaseTagValueString(inputXml, CaseHtmlValue.LastUpdated);              
            Mailbox =                       setCaseTagValueString(inputXml, CaseHtmlValue.Mailbox);                  
            Number =                        setCaseTagValueString(inputXml, CaseHtmlValue.Number);                   
            OriginalTitle =                 setCaseTagValueString(inputXml, CaseHtmlValue.OriginalTitle);            
            Occurances =                    setCaseTagValueString(inputXml, CaseHtmlValue.Occurances);               
            Parent =                        setCaseTagValueString(inputXml, CaseHtmlValue.Parent);                   
            PersonAssignedId =              setCaseTagValueString(inputXml, CaseHtmlValue.PersonAssignedId);         
            PersonAssignedName =            setCaseTagValueString(inputXml, CaseHtmlValue.PersonAssignedName);       
            PersonClosedId =                setCaseTagValueString(inputXml, CaseHtmlValue.PersonClosedId);           
            PersonLastEditedId =            setCaseTagValueString(inputXml, CaseHtmlValue.PersonLastEditedId);       
            PersonOpenedId =                setCaseTagValueString(inputXml, CaseHtmlValue.PersonOpenedId);           
            PersonResolvedId =              setCaseTagValueString(inputXml, CaseHtmlValue.PersonResolvedId);         
            PriorityInteger =               setCaseTagValueString(inputXml, CaseHtmlValue.PriorityInteger);          
            PriorityName =                  setCaseTagValueString(inputXml, CaseHtmlValue.PriorityName);             
            ProjectId =                     setCaseTagValueString(inputXml, CaseHtmlValue.ProjectId);                
            ProjectName =                   setCaseTagValueString(inputXml, CaseHtmlValue.ProjectName);              
            RelatedBugIds =                 setCaseTagValueString(inputXml, CaseHtmlValue.RelatedBugIds);            
            ReleaseNotes =                  setCaseTagValueString(inputXml, CaseHtmlValue.ReleaseNotes);             
            ScoutDescription =              setCaseTagValueString(inputXml, CaseHtmlValue.ScoutDescription);         
            ScoutMessage =                  setCaseTagValueString(inputXml, CaseHtmlValue.ScoutMessage);             
            Status =                        setCaseTagValueString(inputXml, CaseHtmlValue.Status);                   
            StatusId =                      setCaseTagValueString(inputXml, CaseHtmlValue.StatusId);                 
            StoryPoints =                   setCaseTagValueString(inputXml, CaseHtmlValue.StoryPoints);              
            Tags =                          setCaseTagValueString(inputXml, CaseHtmlValue.Tags);                     
            Ticket =                        setCaseTagValueString(inputXml, CaseHtmlValue.Ticket);                   
            Title =                         setCaseTagValueString(inputXml, CaseHtmlValue.Title);
            Version =                       setCaseTagValueString(inputXml, CaseHtmlValue.Version);
        }

        /// <summary>
        /// Stores the case variable as a friendly string and the html command to change it (Ex. "Area1", &sArea=)
        /// </summary>
        /// <param name="inputXml"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private CaseProperty setCaseTagValueString(XElement inputXml, string tag)
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
                return new CaseProperty();
            }
        }

        /// <summary>
        /// Intercept Fody inject OnPropertyChanged so we can create a changeset.
        /// </summary>
        protected void OnPropertyChanged(string propertyName, object before, object after)
        {
            //Intercept property, store it in the changeset, overwrite change if already present
            if (before is CaseProperty)
            {
                if (Changeset == null)
                {
                    Changeset = new List<string>();
                }

                var cProp = after as CaseProperty;
                var cPropBefore = before as CaseProperty;
                Changeset.Remove(cPropBefore?.HtmlHeader + HttpUtility.HtmlEncode(cPropBefore?.Value));
                if (cProp?.Value != null) Changeset.Add(cProp?.HtmlHeader + HttpUtility.HtmlEncode(cProp?.Value));
            }
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}