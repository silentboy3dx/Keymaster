/*
 * Copyright 2022 SilentBoy
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace Keymaster.Models.Settings
{
    public partial class SendTextSettingsModel
    {
        public string File { get; set; } = "";
        public string Delimiter { get; set; } = ",";
        public int Header { get; set; } = 0;

        private List<string> jokes = new List<string>();
        private static Random rng = new Random();

        public string GetRandomJoke()
        {
            using (var reader = new StreamReader(@File))
            {
                int cnt = 0;

                if (jokes.Count == 0)
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(char.Parse(Delimiter));
                        
                        if (Header == 1 && cnt == 0)
                        {
                            cnt++;
                            continue;
                        }
                        
                        jokes.Add(values[0]);
                        cnt++;
                    }
                }
                else
                {
                    return jokes[rng.Next(jokes.Count)];    
                }

                return "";
            }
        }
    }
}