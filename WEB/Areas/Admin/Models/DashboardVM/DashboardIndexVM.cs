namespace WEB.Areas.Admin.Models.DashboardVM
{
    public class DashboardIndexVM
    {
        public int TotalRequests { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }

        public List<WEB.Areas.Request.Models.RequestVM.GetRequestsVM> Requests { get; set; } = new();
    }
}
