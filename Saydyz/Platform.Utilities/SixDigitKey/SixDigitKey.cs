﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Utilities.SixDigitKey
{
    public class SixDigitKey : ISixDigitKey
    {
        public string GenerateNewKey()
        {

            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewKey();
            }
            return r;

        }
    }
}