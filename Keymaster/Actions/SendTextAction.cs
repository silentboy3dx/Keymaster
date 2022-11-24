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

namespace Keymaster
{
    [ActionUuid(Uuid = "io.silentboy.keymaster.Actions.SendText")]
    public class SendTextAction : BaseStreamDeckActionWithSettingsModel<Models.Settings.SendTextSettingsModel>
    {
        public override async Task OnKeyUp(StreamDeckEventPayload args)
        {
            if (SettingsModel.Text.Length == 0)
            {
                /*
                 * Show an alert if there is no text to send.
                 */
                await Manager.ShowAlertAsync(args.context);
            }
            else
            {
                
                var hWnd = NativeWin32.GetForegroundWindow();
                var strOut = new StringBuilder(256);
            
                NativeWin32.SetForegroundWindow(hWnd);
                NativeWin32.GetWindowText(hWnd, strOut, strOut.Capacity);
                Thread.Sleep(1000);
                
                TextCopy.ClipboardService.SetText(SettingsModel.Text);
		
                System.Windows.Forms.SendKeys.SendWait("^v");
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                
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