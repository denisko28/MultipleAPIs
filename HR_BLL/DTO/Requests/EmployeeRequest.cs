﻿using System;

namespace MultipleAPIs.HR_BLL.DTO.Requests
{
    public class EmployeeRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }

        public int EmployeeStatusId { get; set; }

        public string? Adress { get; set; }

        public string? PassportImgPath { get; set; }

        public DateTime Birthday { get; set; }
    }
}