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
        private static int DELAY = 50;

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

        public bool Loop()
        {

            switch (_state)
            {
                case TicketerState.Initialized:
                    OnInitialized();
                    break;
                case TicketerState.SelectedBlock:
                    OnSeatSelect();
                    break;
                case TicketerState.Rejected:
                    OnRejected();
                    break;
                case TicketerState.Succeeded:
                    System.Media.SystemSounds.Asterisk.Play();
                    return true;
            }
            return false;
        }

        private void OnInitialized()
        {
            var screen = ScreenReader.GetScreen(_screenSize, _screenLoc);
            var blocks = ScreenReader.FindColorblocks(screen, (int)SEAT_SIZE);
            if (!blocks.Any())
            {
                _state = TicketerState.Rejected;
                return;
            }

            var selectedBlock = blocks.Skip(_random.Next(blocks.Count())).First();
            WindowsHelper.Click(selectedBlock.Center().Add(_screenLoc));
            Thread.Sleep(50);
            _state = TicketerState.SelectedBlock;
        }

        private void OnSeatSelect()
        {
            var buttonScreen = ScreenReader.GetScreen(new Size(10, 10), _submitPoint);
            if (buttonScreen.GetPixel(0, 0).IsGraytone())
            {
                OnInitialized();
                return;
            }
            WindowsHelper.Click(_submitPoint);
            //await Task.Delay(DELAY);
            // if 이선좌 make it failed
            _state = TicketerState.Succeeded;
        }

        private void OnRejected()
        {
            WindowsHelper.Click(_returnPoint);
            Thread.Sleep(500);
            _state = TicketerState.Initialized;
        }
    }
}
