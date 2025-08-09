using WEB.Areas.Request.Models.RequestVM;

namespace WEB.Areas.Admin.Models.DashboardVM
{
    public class DashboardVM
    {
        public int TotalRequests { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }
        public List<GetRequestsVM> Requests { get; set; } = new();
    }
}
