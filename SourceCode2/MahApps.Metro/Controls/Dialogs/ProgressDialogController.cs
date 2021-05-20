namespace MahApps.Metro.Controls.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// ���ڲ����򿪵�ProgressDialog����.
    /// </summary>
    public class ProgressDialogController
    {
        private ProgressDialog WrappedDialog { get; }

        private Func<Task> CloseCallback { get; }

        /// <summary>
        /// �رնԻ���ʱ,�������¼�.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// ���û�����ȡ����ťʱ���������¼�
        /// </summary>
        public event EventHandler Canceled;
        /// <summary>
        /// ���û�����ȷ����ťʱ���������¼�
        /// </summary>
        public event EventHandler Determine;

        /// <summary>
        /// ��ȡ�Ƿ��Ѱ��¡�ȡ������ť.
        /// </summary>        
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// ��ȡ�Ƿ��Ѱ��¡�ȷ������ť.
        /// </summary>        
        public bool IsDetermine { get; private set; }

        /// <summary>
        /// ��ȡ��װ��ProgressDialog�Ƿ��Ѵ�.
        /// </summary>        
        public bool IsOpen { get; private set; }

        internal ProgressDialogController(ProgressDialog dialog, Func<Task> closeCallBack)
        {
            this.WrappedDialog = dialog;
            this.CloseCallback = closeCallBack;

            this.IsOpen = dialog.IsVisible;

            this.WrappedDialog.Invoke(() => { this.WrappedDialog.PART_NegativeButton.Click += this.PART_NegativeButton_Click; });
            this.WrappedDialog.Invoke(() => { this.WrappedDialog.PART_DetermineButton.Click += this.PART_DetermineButton_Click; });

            dialog.CancellationToken.Register(() => { this.PART_NegativeButton_Click(null, new RoutedEventArgs()); });
            dialog.CancellationToken.Register(() => { this.PART_DetermineButton_Click(null, new RoutedEventArgs()); });
        }

        private void PART_NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            Action action = () =>
                {
                    this.IsCanceled = true;
                    this.Canceled?.Invoke(this, EventArgs.Empty);
                    this.WrappedDialog.PART_NegativeButton.IsEnabled = false;
                };
            this.WrappedDialog.Invoke(action);
        }

        private void PART_DetermineButton_Click(object sender, RoutedEventArgs e)
        {
            Action action = () =>
            {
                this.IsDetermine = true;
                this.Determine?.Invoke(this, EventArgs.Empty);
                this.WrappedDialog.PART_DetermineButton.IsEnabled = false;
            };
            this.WrappedDialog.Invoke(action);
        }

        /// <summary>
        /// ��ProgressBar��IsIndeterminate����Ϊtrue�� Ҫ��������Ϊfalse�������SetProgress.
        /// </summary>
        public void SetIndeterminate()
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.SetIndeterminate());
        }

        /// <summary>
        /// ���á�ȡ������ť�Ƿ�ɼ�.
        /// </summary>
        /// <param name="value"></param>
        public void SetCancelable(bool value)
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.IsCancelable = value);
        }
        /// <summary>
        /// ���á�ȷ������ť�Ƿ�ɼ�.
        /// </summary>
        /// <param name="value"></param>
        public void SetDetermine(bool value)
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.IsDetermine = value);
        }

        /// <summary>
        /// ���öԻ���Ľ�����ֵ����IsIndeterminate����Ϊfalse.
        /// </summary>
        /// <param name="value">The percentage to set as the value.</param>
        public void SetProgress(double value)
        {
            Action action = () =>
                {
                    if (value < this.WrappedDialog.Minimum || value > this.WrappedDialog.Maximum)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }
                    this.WrappedDialog.ProgressValue = value;
                };
            this.WrappedDialog.Invoke(action);
        }

        /// <summary>
        ///  ��ȡ/���ý���Value���Ե���С����
        /// </summary>
        public double Minimum
        {
            get { return this.WrappedDialog.Invoke(() => this.WrappedDialog.Minimum); }
            set { this.WrappedDialog.Invoke(() => this.WrappedDialog.Minimum = value); }
        }

        /// <summary>
        ///  ��ȡ/���ý���Value���Ե��������
        /// </summary>
        public double Maximum
        {
            get { return this.WrappedDialog.Invoke(() => this.WrappedDialog.Maximum); }
            set { this.WrappedDialog.Invoke(() => this.WrappedDialog.Maximum = value); }
        }

        /// <summary>
        /// ���öԻ������Ϣ����.
        /// </summary>
        /// <param name="message">The message to be set.</param>
        public void SetMessage(string message)
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.Message = message);
        }

        /// <summary>
        /// ���öԻ���ı���.
        /// </summary>
        /// <param name="title">The title to be set.</param>
        public void SetTitle(string title)
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.Title = title);
        }

        /// <summary>
        /// ���öԻ���Ľ���������
        /// </summary>
        /// <param name="brush">The brush to use for the progress bar's foreground</param>
        public void SetProgressBarForegroundBrush(Brush brush)
        {
            this.WrappedDialog.Invoke(() => this.WrappedDialog.ProgressBarForeground = brush);
        }

        /// <summary>
        /// ��ʼ�ر�ProgressDialog�Ĳ���.
        /// </summary>
        /// <returns>A task representing the operation.</returns>
        public Task CloseAsync()
        {
            Action action = () =>
                {
                    if (!this.WrappedDialog.IsVisible)
                    {
                        throw new InvalidOperationException("Dialog isn't visible to close");
                    }
                    this.WrappedDialog.Dispatcher.VerifyAccess();
                    this.WrappedDialog.PART_NegativeButton.Click -= this.PART_NegativeButton_Click;
                };

            this.WrappedDialog.Invoke(action);

            return this.CloseCallback()
                       .ContinueWith(_ => this.WrappedDialog.Invoke(() =>
                                                                        {
                                                                            this.IsOpen = false;
                                                                            this.Closed?.Invoke(this, EventArgs.Empty);
                                                                        }));
        }
    }
}