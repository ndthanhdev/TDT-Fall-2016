﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Quiz.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool BrowserRemembered { get; set; }
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }
    }
}