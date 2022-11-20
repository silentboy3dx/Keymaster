using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SendKeys.Core;
using StreamDeckLib;
using StreamDeckLib.Messages;

namespace SendKeys
{
    [ActionUuid(Uuid = "io.silentboy.sendkeys.send.SendText")]
    public class SendTextAction : BaseStreamDeckActionWithSettingsModel<Models.SendTextSettingsModel>
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