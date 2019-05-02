using Sitecore.Analytics.Reporting;
using Sitecore.ContentTesting.Intelligence.Pipelines.ForecastTestDuration;
using Sitecore.Support.ContentTesting.Intelligence.Analytics.Reporting;
using System;

namespace Sitecore.Support.ContentTesting.Intelligence.Pipelines.ForecastTestDuration
{
    public class Duration
    {
        protected readonly ReportDataProviderBase ReportProvider;

        public Duration()
            : this(null)
        {
        }

        public Duration(ReportDataProviderBase reportingProvider = null)
        {
            ReportProvider = reportingProvider;
        }

        public void Process(ForecastTestDurationPipelineArgs args)
        {
            CalculateDuration(args);
        }

        protected void CalculateDuration(ForecastTestDurationPipelineArgs args)
        {
            args.VisitsPerDay = -1.0;
            args.DurationDays = -1;
            if (args.Item != null)
            {
                AveragePageViewsQuery averagePageViewsQuery = new AveragePageViewsQuery(ReportProvider)
                {
                    Item = args.Item,
                    ReportStart = DateTime.UtcNow.AddDays(-30.0),
                    ReportEnd = DateTime.UtcNow,
                    Language = args.Language,
                    DeviceName = args.DeviceName
                };
                averagePageViewsQuery.Execute();
                if (averagePageViewsQuery.AveragePageViews > 0.0)
                {
                    args.VisitsPerDay = averagePageViewsQuery.AveragePageViews;
                    args.DurationDays = (int)System.Math.Ceiling((double)(args.SessionCountRequired - args.VisitsInTest) / (args.VisitsPerDay * args.TrafficAllocation));
                }
            }
        }
    }
}
