
namespace ffTicketingCsharp
{
    public partial class Form1 : Form
    {
        private readonly SubmitButton _submitButton;
        private readonly ReturnButton _returnButton;
        private CancellationTokenSource _cts;
        public Form1()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            KeyDown += OnKeyDown;
            _submitButton = new SubmitButton();
            _submitButton.Owner = this;
            _submitButton.Show();
            _returnButton = new ReturnButton();
            _returnButton.Owner = this;
            _returnButton.Show();
            _cts = new CancellationTokenSource();
        }

        private const int WM_HOTKEY = 0x0312;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowsHelper.RegisterHotKey(this.Handle, 1004, 0, (int)Keys.Escape);
            _submitButton.Location = new Point(this.Right, this.Top);
            _returnButton.Location = new Point(this.Right, _submitButton.Bottom);
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            switch (message.Msg)
            {
                case WM_HOTKEY:
                    _cts.Cancel();
                    WindowState = FormWindowState.Normal;
                    return;
            }
        }
    
        private void OnSizeChanged(object? sender, EventArgs e)
        {
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            var key = e.KeyCode;
            if (key == Keys.Escape)
            {
                _cts.Cancel();
                WindowState = FormWindowState.Normal;
            }
            if (key == Keys.Enter)
            {
                _cts.Dispose();
                _cts = new CancellationTokenSource();
                var looper = new Looper(new Ticketer(this.Size, this.Location, _returnButton.Center(), _submitButton.Center()), _cts.Token);
                WindowState = FormWindowState.Minimized;
                _ = Task.Run(() => looper.Loop());
            }
        }
    }
}
