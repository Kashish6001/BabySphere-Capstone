namespace BabySphere.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalBabysitters { get; set; }
        public int TotalProducts { get; set; }
        public int TotalBookings { get; set; }
        public int TotalParentProfiles { get; set; }
        public int PendingSupportRequests { get; set; }

        public List<string> RecentActivities { get; set; } = new List<string>();
    }
}