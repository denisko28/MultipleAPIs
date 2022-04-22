namespace HR_BLL.Helpers
{
    public static class AppointmentStatusHelper
    {
        public static string GetStringStatus(int statusId)
        {
            return statusId switch
            {
                1 => "Очікується підтвердження",
                2 => "Заплановано",
                3 => "Виконано",
                4 => "Відхилено",
                _ => "???"
            };
        }

        public static int GetIntStatus(string status)
        {
            return status switch
            {
                "Очікується підтвердження" => 1,
                "Заплановано" => 2,
                "Виконано" => 3,
                "Відхилено" => 4,
                _ => 0
            };
        }
    }
}
