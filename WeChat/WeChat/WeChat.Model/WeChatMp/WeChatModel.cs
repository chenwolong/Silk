﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Model
{
    public class TokenModel
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }
}