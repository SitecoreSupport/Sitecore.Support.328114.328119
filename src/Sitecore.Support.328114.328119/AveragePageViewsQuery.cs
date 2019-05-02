using Sitecore.Analytics.Reporting;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Support.ContentTesting.Intelligence.Analytics.Reporting
{
    public class AveragePageViewsQuery : Sitecore.ContentTesting.Intelligence.Analytics.Reporting.AveragePageViewsQuery
    {
        public AveragePageViewsQuery(ReportDataProviderBase reportProvider = null) : this(reportProvider, null)
        {
        }
        public AveragePageViewsQuery(ReportDataProviderBase reportProvider = null, CachingPolicy cachingPolicy = null) : base(reportProvider, cachingPolicy)
        {
        }
        public override void Execute()
        {
            DateTime reportStart = this.ReportStart;
            if ((this.Item != null) && (this.Item.Created > this.ReportStart))
            {
                reportStart = this.Item.Created;
            }

            ID id = this.ItemId;
            Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
            dictionary1.Add("@ItemId", id.ToString());
            dictionary1.Add("@Start", reportStart);
            dictionary1.Add("@End", this.ReportEnd);
            dictionary1.Add("@LanguageName", (this.Language != null) ? this.Language.Trim() : string.Empty);
            Dictionary<string, object> local1 = dictionary1;
            Dictionary<string, object> local2 = dictionary1;
            local2.Add("@DeviceName", (this.DeviceName != null) ? this.DeviceName.Trim() : string.Empty);
            Dictionary<string, object> dictionary = local2;
            object obj2 = base.ExecuteQuery(dictionary).Rows[0]["Visits"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                this.AveragePageViews = ((double)((long)obj2)) / ((double)((this.ReportEnd - reportStart).Days + 1));
            }
        }
    }
}
