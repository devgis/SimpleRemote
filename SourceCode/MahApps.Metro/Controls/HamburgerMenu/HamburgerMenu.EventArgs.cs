using System;
using System.Windows;

namespace MahApps.Metro.Controls
{
    /// <summary>
    /// EventArgs used for the <see cref="HamburgerMenu"/> ItemClick and OptionsItemClick event.
    /// </summary>
    public class ItemClickEventArgs : RoutedEventArgs
    {
        public ItemClickEventArgs(object clickedObject)
        {
            ClickedItem = clickedObject;
        }

        /// <summary>
        /// Gets the clicked item
        /// </summary>
        public object ClickedItem { get; internal set; }
    }

    public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

    /// <summary>
    /// EventArgs used for the <see cref="HamburgerMenu"/> ItemInvoked event.
    /// </summary>
    public class HamburgerMenuItemInvokedEventArgs : EventArgs
    {
        /// <summary>
        /// ��ȡ���õ���Ŀ
        /// </summary>
        public object InvokedItem { get; internal set; }

        /// <summary>
        /// ��ȡһ��ֵ����ֵָʾ�����õ���Ŀ�Ƿ�Ϊѡ��
        /// </summary>
        public bool IsItemOptions { get; internal set; }
    }
}
