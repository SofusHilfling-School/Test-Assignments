﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Models;

namespace BookingSystem.Services
{
    public interface ISmsService
    {
        bool SendSms(SmsMessage message);
    }
}
