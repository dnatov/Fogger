using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzApi
{
    public class CaseProperty
    {
        public string Value { get; set; }
        public string HtmlHeader { get; set; }
    }

    public static class CaseHtmlValue
    {
        public const string Area =                           "sArea";
        public const string AreaId =                         "ixArea";
        public const string Category =                       "sCategory";
        public const string CategoryId =                     "ixCategory";
        public const string Children =                       "ixBugChildren";
        public const string Computer =                       "sComputer";
        public const string CustomerEmail =                  "sCustomerEmail";
        public const string DateClosed =                     "dtClosed";
        public const string DateDue =                        "dtDue";
        public const string DateLastOccurrence =             "dtLastOccurrence";
        public const string DateLastViewed =                 "dtLastView";
        public const string DateOpened =                     "dtOpened";
        public const string DateResolved =                   "dtResolved";
        public const string DiscussionTopicId =              "ixDiscussTopic";
        public const string DuplicateCaseIds =               "ixBugDuplicates";
        public const string DuplicateOriginal =              "ixBugOriginal";
        public const string EmailAssignedTo =                "sEmailAssignedTo";
        public const string EstimateHoursCurrent =           "hrsCurrEst";
        public const string EstimateHoursElapsed =           "hrsElapsed";
        public const string EstimateHoursOriginal =          "hrsOrigEst";
        public const string EventLatestText =                "ixBugEventLatestText";
        public const string FixForId =                       "ixFixFor";
        public const string FixForName =                     "sFixFor";
        public const string FixForOrder =                    "nFixForOrder";
        public const string FixForDateTime =                 "dtFixFor";
        public const string GroupId =                        "ixGroup";
        public const string PersonAssignedId =               "ixPersonAssignedTo";
        public const string PersonAssignedName =             "sPersonAssignedTo";
        public const string PersonClosedId =                 "ixPersonClosedBy";
        public const string PersonLastEditedId =             "ixPersonLastEditedBy";
        public const string PersonOpenedId =                 "ixPersonOpenedBy";
        public const string PersonResolvedId =               "ixPersonResolvedBy";
        public const string IsOpen =                         "fOpen";
        public const string IsForwarded =                    "fForwared";
        public const string IsRepliedTo =                    "fReplied";
        public const string IsScoutReportingStopped =        "fScoutStopReporting";
        public const string IsSubscribed =                   "fSubscribed";
        public const string LatestTextSummary =              "sLatestTextSummary";
        public const string LatestBugEvent =                 "ixBugEventLatest";
        public const string LatestBugEventWhenLastViewed =   "ixBugEventLastView";
        public const string LastUpdated =                    "dtLastUpdated";
        public const string Mailbox =                        "ixMailbox";
        public const string Number =                         "ixBug";
        public const string OriginalTitle =                  "sOriginalTitle";
        public const string Occurances =                     "c";
        public const string Parent =                         "ixBugParent";
        public const string PriorityInteger =                "ixPriority";
        public const string PriorityName =                   "sPriority";
        public const string ProjectId =                      "ixProject";
        public const string ProjectName =                    "sProject";
        public const string RelatedBugIds =                  "ixRelatedBugs";
        public const string ReleaseNotes =                   "sReleaseNotes";
        public const string ScoutDescription =               "sScoutDescription";
        public const string ScoutMessage =                   "sScoutMessage";
        public const string Status =                         "sStatus";
        public const string StatusId =                       "ixStatus";
        public const string StoryPoints =                    "dblStoryPts";
        public const string Tags =                           "tags";
        public const string Ticket =                         "sTicket";
        public const string Title =                          "sTitle";
        public const string Version =                        "sVersion";
    }
}
