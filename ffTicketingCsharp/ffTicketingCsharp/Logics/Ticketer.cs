using System.Linq;
using System.Collections.Generic;

namespace ffTicketingCsharp
{
    internal class Ticketer : IActor
    {
        private readonly Size _screenSize;
        private readonly Point _screenLoc;
        private readonly Point _returnPoint;
        private readonly Point _submitPoint;
        private TicketerState _state;
        private Random _random;

        private static uint BLOCK_SIZE = 13;
        private static uint SEAT_SIZE = 10;
        private static int DELAY = 20;

        private enum TicketerState
        {
            Initialized = 0,
            SelectedBlock = 1,
            SelectedSeat = 2,
            Rejected = 3,
            Succeeded = 4
        }

        public Ticketer(Size screenSize, Point screenLoc, Point returnPoint, Point submitPoint)
        {
            _screenSize = screenSize;
            _screenLoc = screenLoc;
            _returnPoint = returnPoint;
            _submitPoint = submitPoint;
            _state = TicketerState.Initialized;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public async Task<bool> Loop()
        {
            var screen = ScreenReader.GetScreen(_screenSize, _screenLoc);

            switch (_state)
            {
                case TicketerState.Initialized:
                    await OnInitialized(screen);
                    break;
                case TicketerState.SelectedBlock:
                    await OnBlockSelect(screen);
                    break;
                case TicketerState.SelectedSeat:
                    await OnSeatSelect(screen);
                    break;
                case TicketerState.Rejected:
                    await OnRejected(screen);
                    break;
                case TicketerState.Succeeded:
                    System.Media.SystemSounds.Asterisk.Play();
                    return true;
            }
            return false;
        }

        private async Task OnInitialized(Bitmap screen)
        {
            var blocks = ScreenReader.FindColorblocks(screen, (int)BLOCK_SIZE);
            if (!blocks.Any())
            {
                _state = TicketerState.Rejected;
                return;
            }

            var selectedBlock = blocks.Skip(_random.Next(blocks.Count())).First();
            WindowsHelper.Click(selectedBlock.Center().Add(_screenLoc));
            await Task.Delay(DELAY);
            _state = TicketerState.SelectedBlock;
        }

        private async Task OnBlockSelect(Bitmap screen)
        {
            var blocks = ScreenReader.FindColorblocks(screen, (int)SEAT_SIZE);
            if (!blocks.Any())
            {
                _state = TicketerState.Rejected;
                return;
            }

            var selectedBlock = blocks.Skip(_random.Next(blocks.Count())).First();
            WindowsHelper.Click(selectedBlock.Center().Add(_screenLoc));
            //await Task.Delay(DELAY);
            _state = TicketerState.SelectedSeat;
        }

        private async Task OnSeatSelect(Bitmap screen)
        {
            var buttonScreen = ScreenReader.GetScreen(new Size(10, 10), _submitPoint);
            if (buttonScreen.GetPixel(0, 0).IsGraytone())
            {
                _state = TicketerState.Rejected;
                return;
            }
            WindowsHelper.Click(_submitPoint);
            //await Task.Delay(DELAY);
            // if 이선좌 make it failed
            _state = TicketerState.Succeeded;
        }

        private async Task OnRejected(Bitmap screen)
        {
            WindowsHelper.Click(_returnPoint);
            await Task.Delay(1000);
            _state = TicketerState.Initialized;
        }
    }
}
