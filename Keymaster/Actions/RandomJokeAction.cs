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

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Keymaster.Core;
using StreamDeckLib;
using StreamDeckLib.Messages;
using System;

namespace Keymaster
{
    [ActionUuid(Uuid = "io.silentboy.keymaster.Actions.RandomJoke")]
    public class RandomJokeAction : BaseStreamDeckActionWithSettingsModel<Models.Settings.SendTextSettingsModel>
    {
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            if (SettingsModel.File.Length == 0)
            {
                /*
                 * Show an alert if there is no text to send.
                 */
                await Manager.ShowAlertAsync(args.context);
            }
            else
            {
                try
                {
                    await Manager.LogMessageAsync(args.context, $"The file is {SettingsModel.File}");
                    await Manager.LogMessageAsync(args.context, $"The delimiter is {SettingsModel.Delimiter}");
                    await Manager.LogMessageAsync(args.context, $"The header is {SettingsModel.Header}");
                
                    var joke = SettingsModel.GetRandomJoke();
                    await Manager.LogMessageAsync(args.context, $"The joke is {joke}");

                    if (joke.Length == 0)
                    {
                        await Manager.ShowAlertAsync(args.context);
                    }
                    else
                    {
                        var hWnd = NativeWin32.GetForegroundWindow();
                        var strOut = new StringBuilder(256);
                        
                        NativeWin32.SetForegroundWindow(hWnd);
                        NativeWin32.GetWindowText(hWnd, strOut, strOut.Capacity);
                        Thread.Sleep(1500);
                        
                        TextCopy.ClipboardService.SetText($"/me " + joke);
		
                        System.Windows.Forms.SendKeys.SendWait("^v");
                        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                    }
                    
                }  catch (Exception e)
                {
                    await Manager.LogMessageAsync(args.context, e.ToString());
                    throw;
                }
               
               
              
                
            }
            
            await Manager.SetSettingsAsync(args.context, SettingsModel);
        }
        
        public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
        {
            await base.OnDidReceiveSettings(args);
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            await base.OnWillAppear(args);
        }
    }
}