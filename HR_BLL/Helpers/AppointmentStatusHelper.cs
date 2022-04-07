namespace MultipleAPIs.HR_BLL.Helpers
{
    public static class AppointmentStatusHelper
    {
        public static string GetStringStatus(int statusId)
        {
            switch (statusId)
            { 
                case 1:
                    return "Очікується підтвердження";
                case 2:
                    return "Заплановано";
                case 3:
                    return "Виконано";
                case 4:
                    return "Відхилено";
                default:
                    return "???";
            }
        }

        public static int GetIntStatus(string status)
        {
            switch (status)
            {
                case "Очікується підтвердження":
                    return 1;
                case "Заплановано":
                    return 2;
                case "Виконано":
                    return 3;
                case "Відхилено":
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
