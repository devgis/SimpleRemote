using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleRemote.Controls.WpfDragDrop
{
    public static partial class DragDrop
    {
        /// <summary>
        /// 将用于拖放操作的默认数据格式。
        /// </summary>
        public static DataFormat DataFormat { get; } = DataFormats.GetDataFormat("GongSolutions.Wpf.DragDrop");

        /// <summary>
        /// 获取或设置将用于拖放操作的数据格式。
        /// </summary>
        public static readonly DependencyProperty DataFormatProperty
            = DependencyProperty.RegisterAttached("DataFormat",
                                                  typeof(DataFormat),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(DragDrop.DataFormat));

        /// <summary>
        /// 获取将用于拖放操作的数据格式。
        /// </summary>
        public static DataFormat GetDataFormat(UIElement source)
        {
            return (DataFormat)source.GetValue(DataFormatProperty);
        }

        /// <summary>
        /// 设置将用于拖放操作的数据格式。
        /// </summary>
        public static void SetDataFormat(UIElement source, DataFormat value)
        {
            source.SetValue(DataFormatProperty, value);
        }

        /// <summary>
        /// 获取或设置控件是否可用作拖动源。
        /// </summary>
        public static readonly DependencyProperty IsDragSourceProperty
            = DependencyProperty.RegisterAttached("IsDragSource",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new UIPropertyMetadata(false, IsDragSourceChanged));

        /// <summary>
        /// 获取控件是否可用作拖动源。
        /// </summary>
        public static bool GetIsDragSource(UIElement target)
        {
            return (bool)target.GetValue(IsDragSourceProperty);
        }

        /// <summary>
        /// 设置控件是否可用作拖动源。
        /// </summary>
        public static void SetIsDragSource(UIElement target, bool value)
        {
            target.SetValue(IsDragSourceProperty, value);
        }

        private static void IsDragSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;

            if ((bool)e.NewValue)
            {
                uiElement.PreviewMouseLeftButtonDown += DragSourceOnMouseLeftButtonDown;
                uiElement.PreviewMouseLeftButtonUp += DragSourceOnMouseLeftButtonUp;
                uiElement.PreviewMouseMove += DragSourceOnMouseMove;
                uiElement.QueryContinueDrag += DragSourceOnQueryContinueDrag;
            }
            else
            {
                uiElement.PreviewMouseLeftButtonDown -= DragSourceOnMouseLeftButtonDown;
                uiElement.PreviewMouseLeftButtonUp -= DragSourceOnMouseLeftButtonUp;
                uiElement.PreviewMouseMove -= DragSourceOnMouseMove;
                uiElement.QueryContinueDrag -= DragSourceOnQueryContinueDrag;
            }
        }

        /// <summary>
        /// 获取或设置控件是否可用作放置目标。
        /// </summary>
        public static readonly DependencyProperty IsDropTargetProperty
            = DependencyProperty.RegisterAttached("IsDropTarget",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new UIPropertyMetadata(false, IsDropTargetChanged));

        /// <summary>
        /// 获取控件是否可用作放置目标。
        /// </summary>
        public static bool GetIsDropTarget(UIElement target)
        {
            return (bool)target.GetValue(IsDropTargetProperty);
        }

        /// <summary>
        /// 设置控件是否可用作放置目标。
        /// </summary>
        public static void SetIsDropTarget(UIElement target, bool value)
        {
            target.SetValue(IsDropTargetProperty, value);
        }

        private static void IsDropTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;

            if ((bool)e.NewValue)
            {
                uiElement.AllowDrop = true;

                if (uiElement is ItemsControl)
                {
                    // use normal events for ItemsControls
                    uiElement.DragEnter += DropTargetOnDragEnter;
                    uiElement.DragLeave += DropTargetOnDragLeave;
                    uiElement.DragOver += DropTargetOnDragOver;
                    uiElement.Drop += DropTargetOnDrop;
                    uiElement.GiveFeedback += DropTargetOnGiveFeedback;
                }
                else
                {
                    // issue #85: try using preview events for all other elements than ItemsControls
                    uiElement.PreviewDragEnter += DropTargetOnDragEnter;
                    uiElement.PreviewDragLeave += DropTargetOnDragLeave;
                    uiElement.PreviewDragOver += DropTargetOnDragOver;
                    uiElement.PreviewDrop += DropTargetOnDrop;
                    uiElement.PreviewGiveFeedback += DropTargetOnGiveFeedback;
                }
            }
            else
            {
                uiElement.AllowDrop = false;

                if (uiElement is ItemsControl)
                {
                    uiElement.DragEnter -= DropTargetOnDragEnter;
                    uiElement.DragLeave -= DropTargetOnDragLeave;
                    uiElement.DragOver -= DropTargetOnDragOver;
                    uiElement.Drop -= DropTargetOnDrop;
                    uiElement.GiveFeedback -= DropTargetOnGiveFeedback;
                }
                else
                {
                    uiElement.PreviewDragEnter -= DropTargetOnDragEnter;
                    uiElement.PreviewDragLeave -= DropTargetOnDragLeave;
                    uiElement.PreviewDragOver -= DropTargetOnDragOver;
                    uiElement.PreviewDrop -= DropTargetOnDrop;
                    uiElement.PreviewGiveFeedback -= DropTargetOnGiveFeedback;
                }

                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// 获取或设置控件是否可以与鼠标右键一起用作拖动源。
        /// </summary>
        public static readonly DependencyProperty CanDragWithMouseRightButtonProperty
            = DependencyProperty.RegisterAttached("CanDragWithMouseRightButton",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new UIPropertyMetadata(false, CanDragWithMouseRightButtonChanged));

        /// <summary>
        /// 获取控件是否可以与鼠标右键一起用作拖动源。
        /// </summary>
        public static bool GetCanDragWithMouseRightButton(UIElement target)
        {
            return (bool)target.GetValue(CanDragWithMouseRightButtonProperty);
        }

        /// <summary>
        /// 设置控件是否可以与鼠标右键一起用作拖动源。
        /// </summary>
        public static void SetCanDragWithMouseRightButton(UIElement target, bool value)
        {
            target.SetValue(CanDragWithMouseRightButtonProperty, value);
        }

        private static void CanDragWithMouseRightButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;

            if ((bool)e.NewValue)
            {
                uiElement.PreviewMouseRightButtonDown += DragSourceOnMouseRightButtonDown;
                uiElement.PreviewMouseRightButtonUp += DragSourceOnMouseRightButtonUp;
            }
            else
            {
                uiElement.PreviewMouseRightButtonDown -= DragSourceOnMouseRightButtonDown;
                uiElement.PreviewMouseRightButtonUp -= DragSourceOnMouseRightButtonUp;
            }
        }

        /// <summary>
        /// 获取默认的拖格处理程序。
        /// </summary>
        public static IDragSource DefaultDragHandler { get; } = new DefaultDragHandler();

        /// <summary>
        ///获取默认的 DropHandler。
        /// </summary>
        public static IDropTarget DefaultDropHandler { get; } = new DefaultDropHandler();

        /// <summary>
        /// 获取或设置拖动操作的处理程序。
        /// </summary>
        public static readonly DependencyProperty DragHandlerProperty
            = DependencyProperty.RegisterAttached("DragHandler",
                                                  typeof(IDragSource),
                                                  typeof(DragDrop));

        /// <summary>
        /// 获取拖动操作的处理程序。
        /// </summary>
        public static IDragSource GetDragHandler(UIElement target)
        {
            return (IDragSource)target.GetValue(DragHandlerProperty);
        }

        /// <summary>
        /// 设置拖动操作的处理程序。
        /// </summary>
        public static void SetDragHandler(UIElement target, IDragSource value)
        {
            target.SetValue(DragHandlerProperty, value);
        }

        /// <summary>
        /// 获取或设置放置操作的处理程序。
        /// </summary>
        public static readonly DependencyProperty DropHandlerProperty
            = DependencyProperty.RegisterAttached("DropHandler",
                                                  typeof(IDropTarget),
                                                  typeof(DragDrop));

        /// <summary>
        /// 获取放置操作的处理程序。
        /// </summary>
        public static IDropTarget GetDropHandler(UIElement target)
        {
            return (IDropTarget)target.GetValue(DropHandlerProperty);
        }

        /// <summary>
        /// 设置放置操作的处理程序。
        /// </summary>
        public static void SetDropHandler(UIElement target, IDropTarget value)
        {
            target.SetValue(DropHandlerProperty, value);
        }

        /// <summary>
        /// 获取或设置放置操作的滚动模式。
        /// </summary>
        public static readonly DependencyProperty DropScrollingModeProperty
            = DependencyProperty.RegisterAttached("DropScrollingMode",
                                                  typeof(ScrollingMode),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(ScrollingMode.Both));

        /// <summary>
        /// 获取放置操作的滚动模式。
        /// </summary>
        public static ScrollingMode GetDropScrollingMode(UIElement target)
        {
            return (ScrollingMode)target.GetValue(DropScrollingModeProperty);
        }

        /// <summary>
        /// 设置放置操作的滚动模式。
        /// </summary>
        public static void SetDropScrollingMode(UIElement target, ScrollingMode value)
        {
            target.SetValue(DropScrollingModeProperty, value);
        }

        /// <summary>
        /// 获取或设置是否也在空目标上显示 DropTargetAdorner (DropTargetInsertionAdorner)。
        /// </summary>
        public static readonly DependencyProperty ShowAlwaysDropTargetAdornerProperty
            = DependencyProperty.RegisterAttached("ShowAlwaysDropTargetAdorner",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// 获取是否也在空目标上显示 DropTargetAdorner (DropTargetInsertionAdorner)。
        /// </summary>
        public static bool GetShowAlwaysDropTargetAdorner(UIElement target)
        {
            return (bool)target.GetValue(ShowAlwaysDropTargetAdornerProperty);
        }

        /// <summary>
        /// 获取是否也在空目标上设置 DropTargetAdorner (DropTargetInsertionAdorner)。
        /// </summary>
        public static void SetShowAlwaysDropTargetAdorner(UIElement target, bool value)
        {
            target.SetValue(ShowAlwaysDropTargetAdornerProperty, value);
        }

        /// <summary>
        /// 获取或设置 DropTargetAdorner 的画笔。
        /// </summary>
        public static readonly DependencyProperty DropTargetAdornerBrushProperty
            = DependencyProperty.RegisterAttached("DropTargetAdornerBrush",
                                                  typeof(Brush),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((Brush)null));

        /// <summary>
        /// 获取 DropTargetAdorner 的画笔。
        /// </summary>
        public static Brush GetDropTargetAdornerBrush(UIElement target)
        {
            return (Brush)target.GetValue(DropTargetAdornerBrushProperty);
        }

        /// <summary>
        /// 设置 DropTargetAdorner 的画笔。
        /// </summary>
        public static void SetDropTargetAdornerBrush(UIElement target, Brush value)
        {
            target.SetValue(DropTargetAdornerBrushProperty, value);
        }

        /// <summary>
        /// 获取或设置控件的Context。只有具有相同Context的控件才允许用于拖放操作。
        /// </summary>
        public static readonly DependencyProperty DragDropContextProperty
            = DependencyProperty.RegisterAttached("DragDropContext",
                                                  typeof(string),
                                                  typeof(DragDrop),
                                                  new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// 获取控件的Context。只有具有相同Context的控件才允许用于拖放操作。
        /// </summary>
        public static string GetDragDropContext(UIElement target)
        {
            return (string)target.GetValue(DragDropContextProperty);
        }

        /// <summary>
        /// 设置控件的Context。只有具有相同Context的控件才允许用于拖放操作。
        /// </summary>
        public static void SetDragDropContext(UIElement target, string value)
        {
            target.SetValue(DragDropContextProperty, value);
        }

        /// <summary>
        /// 获取或设置是否应忽略鼠标下的元素以执行拖动操作。
        /// </summary>
        public static readonly DependencyProperty DragSourceIgnoreProperty
            = DependencyProperty.RegisterAttached("DragSourceIgnore",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 获取是否应忽略鼠标下的元素进行拖动操作。
        /// </summary>
        public static bool GetDragSourceIgnore(UIElement source)
        {
            return (bool)source.GetValue(DragSourceIgnoreProperty);
        }

        /// <summary>
        /// 设置是否应忽略鼠标下的元素进行拖动操作。
        /// </summary>
        public static void SetDragSourceIgnore(UIElement source, bool value)
        {
            source.SetValue(DragSourceIgnoreProperty, value);
        }

        /// <summary>
        /// Gets or Sets wheter the drag action should be started only directly on a selected item
        /// or also on the free control space (e.g. in a ListBox).
        /// </summary>
        public static readonly DependencyProperty DragDirectlySelectedOnlyProperty
            = DependencyProperty.RegisterAttached("DragDirectlySelectedOnly",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// Gets wheter the drag action should be started only directly on a selected item.
        /// </summary>
        public static bool GetDragDirectlySelectedOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(DragDirectlySelectedOnlyProperty);
        }

        /// <summary>
        /// Sets wheter the drag action should be started only directly on a selected item.
        /// </summary>
        public static void SetDragDirectlySelectedOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(DragDirectlySelectedOnlyProperty, value);
        }

        /// <summary>
        /// The drag drop copy key state property (default None).
        /// So the drag drop action is
        /// - Move, within the same control or from one to another, if the drag drop key state is None
        /// - Copy, from one to another control with the given drag drop copy key state
        /// </summary>
        public static readonly DependencyProperty DragDropCopyKeyStateProperty
            = DependencyProperty.RegisterAttached("DragDropCopyKeyState",
                                                  typeof(DragDropKeyStates),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(default(DragDropKeyStates)));

        /// <summary>
        /// Gets the copy key state which indicates the effect of the drag drop operation.
        /// </summary>
        public static DragDropKeyStates GetDragDropCopyKeyState(UIElement target)
        {
            return (DragDropKeyStates)target.GetValue(DragDropCopyKeyStateProperty);
        }

        /// <summary>
        /// Sets the copy key state which indicates the effect of the drag drop operation.
        /// </summary>
        public static void SetDragDropCopyKeyState(UIElement target, DragDropKeyStates value)
        {
            target.SetValue(DragDropCopyKeyStateProperty, value);
        }

        /// <summary>
        /// 获取或设置是否应在拖动时显示图像.
        /// </summary>
        public static readonly DependencyProperty UseDefaultDragAdornerProperty
            = DependencyProperty.RegisterAttached("UseDefaultDragAdorner",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// Gets whether if the default DragAdorner is used.
        /// </summary>
        public static bool GetUseDefaultDragAdorner(UIElement target)
        {
            return (bool)target.GetValue(UseDefaultDragAdornerProperty);
        }

        /// <summary>
        /// Sets whether if the default DragAdorner should be use.
        /// </summary>
        public static void SetUseDefaultDragAdorner(UIElement target, bool value)
        {
            target.SetValue(UseDefaultDragAdornerProperty, value);
        }

        /// <summary>
        /// 获取或设置默认的 DragAdorner 的不透明度。
        /// </summary>
        public static readonly DependencyProperty DefaultDragAdornerOpacityProperty
            = DependencyProperty.RegisterAttached("DefaultDragAdornerOpacity",
                                                  typeof(double),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(0.8));

        /// <summary>
        /// 获取默认的 DragAdorner 的不透明度。
        /// </summary>
        public static double GetDefaultDragAdornerOpacity(UIElement target)
        {
            return (double)target.GetValue(DefaultDragAdornerOpacityProperty);
        }

        /// <summary>
        /// 设置默认的 DragAdorner 的不透明度。
        /// </summary>
        public static void SetDefaultDragAdornerOpacity(UIElement target, double value)
        {
            target.SetValue(DefaultDragAdornerOpacityProperty, value);
        }

        /// <summary>
        /// 获取或设置指针将锚定在 DragAdorner 上的水平和垂直比例。
        /// </summary>
        public static readonly DependencyProperty DragMouseAnchorPointProperty
            = DependencyProperty.RegisterAttached("DragMouseAnchorPoint",
                                                  typeof(Point),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(new Point(0, 1)));

        /// <summary>
        /// 获取指针将锚定在 DragAdorner 上的水平和垂直比例。
        /// </summary>
        public static Point GetDragMouseAnchorPoint(UIElement target)
        {
            return (Point)target.GetValue(DragMouseAnchorPointProperty);
        }

        /// <summary>
        /// 设置指针将锚定在 DragAdorner 上的水平和垂直比例。
        /// </summary>
        public static void SetDragMouseAnchorPoint(UIElement target, Point value)
        {
            target.SetValue(DragMouseAnchorPointProperty, value);
        }

        /// <summary>
        /// Gets or Sets the translation transform which will be used for the DragAdorner.
        /// </summary>
        public static readonly DependencyProperty DragAdornerTranslationProperty
            = DependencyProperty.RegisterAttached("DragAdornerTranslation",
                                                  typeof(Point),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(new Point(-4, -4)));

        /// <summary>
        /// Gets the translation transform which will be used for the DragAdorner.
        /// </summary>
        public static Point GetDragAdornerTranslation(UIElement element)
        {
            return (Point)element.GetValue(DragAdornerTranslationProperty);
        }

        /// <summary>
        /// Sets the translation transform which will be used for the DragAdorner.
        /// </summary>
        public static void SetDragAdornerTranslation(UIElement element, Point value)
        {
            element.SetValue(DragAdornerTranslationProperty, value);
        }

        /// <summary>
        /// Gets or Sets the translation transform which will be used for the EffectAdorner.
        /// </summary>
        public static readonly DependencyProperty EffectAdornerTranslationProperty
            = DependencyProperty.RegisterAttached("EffectAdornerTranslation",
                                                  typeof(Point),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(new Point(16, 16)));

        /// <summary>
        /// Gets the translation transform which will be used for the EffectAdorner.
        /// </summary>
        public static Point GetEffectAdornerTranslation(UIElement element)
        {
            return (Point)element.GetValue(EffectAdornerTranslationProperty);
        }

        /// <summary>
        /// Sets the translation transform which will be used for the EffectAdorner.
        /// </summary>
        public static void SetEffectAdornerTranslation(UIElement element, Point value)
        {
            element.SetValue(EffectAdornerTranslationProperty, value);
        }

        /// <summary>
        /// Gets or Sets a DataTemplate for the DragAdorner.
        /// </summary>
        public static readonly DependencyProperty DragAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("DragAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop));

        /// <summary>
        /// Gets the DataTemplate for the DragAdorner.
        /// </summary>
        public static DataTemplate GetDragAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(DragAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets the DataTemplate for the DragAdorner.
        /// </summary>
        public static void SetDragAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(DragAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a DataTemplateSelector for the DragAdorner.
        /// </summary>
        public static readonly DependencyProperty DragAdornerTemplateSelectorProperty
            = DependencyProperty.RegisterAttached("DragAdornerTemplateSelector",
                                                  typeof(DataTemplateSelector),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(default(DataTemplateSelector)));

        /// <summary>
        /// Gets the DataTemplateSelector for the DragAdorner.
        /// </summary>
        public static void SetDragAdornerTemplateSelector(DependencyObject element, DataTemplateSelector value)
        {
            element.SetValue(DragAdornerTemplateSelectorProperty, value);
        }

        /// <summary>
        /// Gets the DataTemplateSelector for the DragAdorner.
        /// </summary>
        public static DataTemplateSelector GetDragAdornerTemplateSelector(DependencyObject element)
        {
            return (DataTemplateSelector)element.GetValue(DragAdornerTemplateSelectorProperty);
        }

        /// <summary>
        /// Use descendant bounds of the VisualSourceItem as MinWidth for the DragAdorner.
        /// </summary>
        public static readonly DependencyProperty UseVisualSourceItemSizeForDragAdornerProperty
            = DependencyProperty.RegisterAttached("UseVisualSourceItemSizeForDragAdorner",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// Get the flag which indicates if the DragAdorner use the descendant bounds of the VisualSourceItem as MinWidth.
        /// </summary>
        public static bool GetUseVisualSourceItemSizeForDragAdorner(UIElement target)
        {
            return (bool)target.GetValue(UseVisualSourceItemSizeForDragAdornerProperty);
        }

        /// <summary>
        /// Set the flag which indicates if the DragAdorner use the descendant bounds of the VisualSourceItem as MinWidth.
        /// </summary>
        public static void SetUseVisualSourceItemSizeForDragAdorner(UIElement target, bool value)
        {
            target.SetValue(UseVisualSourceItemSizeForDragAdornerProperty, value);
        }

        /// <summary>
        /// 获取或设置是否应使用效果的默认数据模板。
        /// </summary>
        public static readonly DependencyProperty UseDefaultEffectDataTemplateProperty
            = DependencyProperty.RegisterAttached("UseDefaultEffectDataTemplate",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// Gets whether if the default DataTemplate for the effects should be use.
        /// </summary>
        public static bool GetUseDefaultEffectDataTemplate(UIElement target)
        {
            return (bool)target.GetValue(UseDefaultEffectDataTemplateProperty);
        }

        /// <summary>
        /// Sets whether if the default DataTemplate for the effects should be use.
        /// </summary>
        public static void SetUseDefaultEffectDataTemplate(UIElement target, bool value)
        {
            target.SetValue(UseDefaultEffectDataTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type None.
        /// </summary>
        public static readonly DependencyProperty EffectNoneAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectNoneAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type None.
        /// </summary>
        public static DataTemplate GetEffectNoneAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectNoneAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type None.
        /// </summary>
        public static void SetEffectNoneAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectNoneAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type Copy.
        /// </summary>
        public static readonly DependencyProperty EffectCopyAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectCopyAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type Copy.
        /// </summary>
        public static DataTemplate GetEffectCopyAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectCopyAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type Copy.
        /// </summary>
        public static void SetEffectCopyAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectCopyAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type Move.
        /// </summary>
        public static readonly DependencyProperty EffectMoveAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectMoveAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type Move.
        /// </summary>
        public static DataTemplate GetEffectMoveAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectMoveAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type Move.
        /// </summary>
        public static void SetEffectMoveAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectMoveAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type Link.
        /// </summary>
        public static readonly DependencyProperty EffectLinkAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectLinkAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type Link.
        /// </summary>
        public static DataTemplate GetEffectLinkAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectLinkAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type Link.
        /// </summary>
        public static void SetEffectLinkAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectLinkAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type All.
        /// </summary>
        public static readonly DependencyProperty EffectAllAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectAllAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type All.
        /// </summary>
        public static DataTemplate GetEffectAllAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectAllAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type All.
        /// </summary>
        public static void SetEffectAllAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectAllAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets a EffectAdorner DataTemplate for effect type Scroll.
        /// </summary>
        public static readonly DependencyProperty EffectScrollAdornerTemplateProperty
            = DependencyProperty.RegisterAttached("EffectScrollAdornerTemplate",
                                                  typeof(DataTemplate),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata((DataTemplate)null));

        /// <summary>
        /// Gets a EffectAdorner DataTemplate for effect type Scroll.
        /// </summary>
        public static DataTemplate GetEffectScrollAdornerTemplate(UIElement target)
        {
            return (DataTemplate)target.GetValue(EffectScrollAdornerTemplateProperty);
        }

        /// <summary>
        /// Sets a EffectAdorner DataTemplate for effect type Scroll.
        /// </summary>
        public static void SetEffectScrollAdornerTemplate(UIElement target, DataTemplate value)
        {
            target.SetValue(EffectScrollAdornerTemplateProperty, value);
        }

        /// <summary>
        /// Gets or Sets the Orientation which should be used for the drag drop action (default null).
        /// Normally it will be look up to find the correct orientaion of the inner ItemsPanel,
        /// but sometimes it's necessary to force the oreintation, if the look up is wrong.
        /// </summary>
        public static readonly DependencyProperty ItemsPanelOrientationProperty
            = DependencyProperty.RegisterAttached("ItemsPanelOrientation",
                                                  typeof(Orientation?),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(null));

        /// <summary>
        /// Gets the Orientation which should be used for the drag drop action (default null).
        /// Normally it will be look up to find the correct orientaion of the inner ItemsPanel,
        /// but sometimes it's necessary to force the oreintation, if the look up is wrong.
        /// </summary>
        public static Orientation? GetItemsPanelOrientation(UIElement source)
        {
            return (Orientation?)source.GetValue(ItemsPanelOrientationProperty);
        }

        /// <summary>
        /// Sets the Orientation which should be used for the drag drop action (default null).
        /// Normally it will be look up to find the correct orientaion of the inner ItemsPanel,
        /// but sometimes it's necessary to force the oreintation, if the look up is wrong.
        /// </summary>
        public static void SetItemsPanelOrientation(UIElement source, Orientation? value)
        {
            source.SetValue(ItemsPanelOrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum horizontal drag distance to allow for limited movement of the mouse pointer before a drag operation begins.
        /// Default is SystemParameters.MinimumHorizontalDragDistance.
        /// </summary>
        public static readonly DependencyProperty MinimumHorizontalDragDistanceProperty
            = DependencyProperty.RegisterAttached("MinimumHorizontalDragDistance",
                                                  typeof(double),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(SystemParameters.MinimumHorizontalDragDistance));

        /// <summary>
        /// Sets the minimum horizontal drag distance.
        /// </summary>
        public static double GetMinimumHorizontalDragDistance(UIElement source)
        {
            return (double)source.GetValue(MinimumHorizontalDragDistanceProperty);
        }

        /// <summary>
        /// Sets the minimum horizontal drag distance.
        /// </summary>
        public static void SetMinimumHorizontalDragDistance(UIElement source, double value)
        {
            source.SetValue(MinimumHorizontalDragDistanceProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum vertical drag distance to allow for limited movement of the mouse pointer before a drag operation begins.
        /// Default is SystemParameters.MinimumVerticalDragDistance.
        /// </summary>
        public static readonly DependencyProperty MinimumVerticalDragDistanceProperty
            = DependencyProperty.RegisterAttached("MinimumVerticalDragDistance",
                                                  typeof(double),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(SystemParameters.MinimumVerticalDragDistance));

        /// <summary>
        /// Gets the minimum vertical drag distance.
        /// </summary>
        public static double GetMinimumVerticalDragDistance(UIElement source)
        {
            return (double)source.GetValue(MinimumVerticalDragDistanceProperty);
        }

        /// <summary>
        /// Sets the minimum vertical drag distance.
        /// </summary>
        public static void SetMinimumVerticalDragDistance(UIElement source, double value)
        {
            source.SetValue(MinimumVerticalDragDistanceProperty, value);
        }

        /// <summary>
        /// Gets or sets whether if the dropped items should be select again (should keep the selection).
        /// Default is false.
        /// </summary>
        public static readonly DependencyProperty SelectDroppedItemsProperty
            = DependencyProperty.RegisterAttached("SelectDroppedItems",
                                                  typeof(bool),
                                                  typeof(DragDrop),
                                                  new PropertyMetadata(false));

        /// <summary>
        /// Gets whether if the dropped items should be select again (should keep the selection).
        /// </summary>
        public static bool GetSelectDroppedItems(UIElement target)
        {
            return (bool)target.GetValue(SelectDroppedItemsProperty);
        }

        /// <summary>
        /// Sets whether if the dropped items should be select again (should keep the selection).
        /// </summary>
        public static void SetSelectDroppedItems(UIElement target, bool value)
        {
            target.SetValue(SelectDroppedItemsProperty, value);
        }
    }
}