namespace HR_BLL.Helpers
{
    public static class EmployeeStatusHelper
    {
        public static string GetStringStatus(int statusId)
        {
            return statusId switch
            {
                1 => "Ген. директор",
                2 => "Директор філії",
                3 => "Барбер",
                _ => "???"
            };
        }

        public static int GetIntStatus(string status)
        {
            return status switch
            {
                "Ген. директор" => 1,
                "Директор філії" => 2,
                "Барбер" => 3,
                _ => 0
            };
        }
    }
}
