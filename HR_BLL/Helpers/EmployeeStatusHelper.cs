namespace MultipleAPIs.HR_BLL.Helpers
{
    public static class EmployeeStatusHelper
    {
        public static string GetStringStatus(int statusId)
        {
            switch (statusId)
            { 
                case 1:
                    return "Ген. директор";
                case 2:
                    return "Директор філії";
                case 3:
                    return "Барбер";
                default:
                    return "???";
            }
        }

        public static int GetIntStatus(string status)
        {
            switch (status)
            {
                case "Ген. директор":
                    return 1;
                case "Директор філії":
                    return 2;
                case "Барбер":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
