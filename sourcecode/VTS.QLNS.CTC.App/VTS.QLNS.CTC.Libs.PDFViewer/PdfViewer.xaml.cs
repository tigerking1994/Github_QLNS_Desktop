using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using VTS.QLNS.CTC.Libs.PDFViewer.Enum;
using VTS.QLNS.CTC.Libs.PDFViewer.Helper;
using VTS.QLNS.CTC.Libs.PDFViewer.MuPdf;

namespace VTS.QLNS.CTC.Libs.PDFViewer
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewer : UserControl
	{
		public event EventHandler PdfLoaded;
		public event EventHandler ZoomTypeChanged;
		public event EventHandler ZoomChanged;
		public event EventHandler ScrollChanged;
		public event EventHandler ViewTypeChanged;
		public event EventHandler PageRowDisplayChanged;
		public event EventHandler<PasswordRequiredEventArgs> PasswordRequired;

		private ZoomType zoomType = ZoomType.Fixed;
		private IPdfViewer innerPanel;
		private PdfViewerInputHandler inputHandler;
		private PageRowBound[] pageRowBounds;
		private DispatcherTimer resizeTimer;

		#region Dependency properties
		public static readonly DependencyProperty PdfPathProperty = DependencyProperty.Register("PdfPath", typeof(string),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty PageMarginProperty = DependencyProperty.Register("PageMargin", typeof(Thickness),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(new Thickness(4)));

		public static readonly DependencyProperty ZoomStepProperty = DependencyProperty.Register("ZoomStep", typeof(double),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(0.25));

		public static readonly DependencyProperty MinZoomFactorProperty = DependencyProperty.Register("MinZoomFactor", typeof(double),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(0.15));

		public static readonly DependencyProperty MaxZoomFactorProperty = DependencyProperty.Register("MaxZoomFactor", typeof(double),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(6.0));

		public static readonly DependencyProperty ViewTypeProperty = DependencyProperty.Register("ViewType", typeof(ViewType),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(ViewType.SinglePage));

		public static readonly DependencyProperty RotationProperty = DependencyProperty.Register("Rotation", typeof(ImageRotation),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(ImageRotation.None));

		public static readonly DependencyProperty PageRowDisplayProperty = DependencyProperty.Register("PageRowDisplay", typeof(PageRowDisplayType),
																			typeof(PdfViewer), new FrameworkPropertyMetadata(PageRowDisplayType.SinglePageRow));

		public string PdfPath
		{
			get { return (string)GetValue(PdfPathProperty); }
			set { SetValue(PdfPathProperty, value); }
		}

		public Thickness PageMargin
		{
			get { return (Thickness)GetValue(PageMarginProperty); }
			set { SetValue(PageMarginProperty, value); }
		}

		public double ZoomStep
		{
			get { return (double)GetValue(ZoomStepProperty); }
			set { SetValue(ZoomStepProperty, value); }
		}

		public double MinZoomFactor
		{
			get { return (double)GetValue(MinZoomFactorProperty); }
			set { SetValue(MinZoomFactorProperty, value); }
		}

		public double MaxZoomFactor
		{
			get { return (double)GetValue(MaxZoomFactorProperty); }
			set { SetValue(MaxZoomFactorProperty, value); }
		}

		public ViewType ViewType
		{
			get { return (ViewType)GetValue(ViewTypeProperty); }
			set { SetValue(ViewTypeProperty, value); }
		}

		public ImageRotation Rotation
		{
			get { return (ImageRotation)GetValue(RotationProperty); }
			set { SetValue(RotationProperty, value); }
		}

		public PageRowDisplayType PageRowDisplay
		{
			get { return (PageRowDisplayType)GetValue(PageRowDisplayProperty); }
			set { SetValue(PageRowDisplayProperty, value); }
		}
		#endregion

		public double HorizontalMargin { get { return this.PageMargin.Right; } }
		public IPdfSource CurrentSource { get; private set; }
		public string CurrentPassword { get; private set; }
		public int TotalPages { get; private set; }
		internal PageRowBound[] PageRowBounds => this.pageRowBounds;
		public ScrollViewer ScrollViewer => this.innerPanel.ScrollViewer;
		public float CurrentZoom => this.innerPanel.CurrentZoom;

		public ZoomType ZoomType
		{
			get { return this.zoomType; }
			private set
			{
				if (this.zoomType != value)
				{
					this.zoomType = value;

					if (ZoomTypeChanged != null)
						ZoomTypeChanged(this, EventArgs.Empty);
				}
			}
		}

		public PdfViewer()
		{
			InitializeComponent();

			this.ChangeDisplayType(this.PageRowDisplay);
			this.inputHandler = new PdfViewerInputHandler(this);

			this.SizeChanged += PdfViewerPanel_SizeChanged;

			resizeTimer = new DispatcherTimer();
			resizeTimer.Interval = TimeSpan.FromMilliseconds(150);
			resizeTimer.Tick += resizeTimer_Tick;
		}

        void PdfViewerPanel_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.CurrentSource == null)
				return;

			resizeTimer.Stop();
			resizeTimer.Start();
		}

		void resizeTimer_Tick(object sender, EventArgs e)
		{
			resizeTimer.Stop();

			if (this.CurrentSource == null)
				return;

			if (this.ZoomType == ZoomType.FitToWidth)
				ZoomToWidth();
			else if (this.ZoomType == ZoomType.FitToHeight)
				ZoomToHeight();
		}

		public void OpenFile(string pdfFilename, string password = null)
		{
			if (!File.Exists(pdfFilename))
				throw new FileNotFoundException(string.Empty, pdfFilename);

			this.Open(new FileSource(pdfFilename), password);
		}

		public void Open(IPdfSource source, string password = null)
		{
			var pw = password;

			if (this.PasswordRequired != null && MuPdfWrapper.NeedsPassword(source) && pw == null)
			{
				var e = new PasswordRequiredEventArgs();
				this.PasswordRequired(this, e);

				if (e.Cancel)
					return;

				pw = e.Password;
			}

			this.LoadPdf(source, pw);
			this.CurrentSource = source;
			this.CurrentPassword = pw;

			if (this.PdfLoaded != null)
				this.PdfLoaded(this, EventArgs.Empty);
		}

		public void Unload()
		{
			this.CurrentSource = null;
			this.CurrentPassword = null;
			this.TotalPages = 0;

			this.innerPanel.Unload();

			if (this.PdfLoaded != null)
				this.PdfLoaded(this, EventArgs.Empty);
		}

		private void LoadPdf(IPdfSource source, string password)
		{
			var pageBounds = MuPdfWrapper.GetPageBounds(source, this.Rotation, password);
			this.pageRowBounds = CalculatePageRowBounds(pageBounds, this.ViewType);
			this.TotalPages = pageBounds.Length;
			this.innerPanel.Load(source, password);
            this.ScrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
		}

        private PageRowBound[] CalculatePageRowBounds(Size[] singlePageBounds, ViewType viewType)
		{
			var pagesPerRow = Math.Min(GetPagesPerRow(), singlePageBounds.Length); // if multiple page-view, but pdf contains less pages than the pages per row
			var finalBounds = new List<PageRowBound>();
			var verticalBorderOffset = (this.PageMargin.Top + this.PageMargin.Bottom);

			if (viewType == ViewType.SinglePage)
			{
				finalBounds.AddRange(singlePageBounds.Select(p => new PageRowBound(p, verticalBorderOffset, 0)));
			}
			else
			{
				var horizontalBorderOffset = this.HorizontalMargin;

				for (int i = 0; i < singlePageBounds.Length; i++)
				{
					if (i == 0 && viewType == ViewType.BookView)
					{
						finalBounds.Add(new PageRowBound(singlePageBounds[0], verticalBorderOffset, 0));
						continue;
					}

					var subset = singlePageBounds.Take(i, pagesPerRow).ToArray();

					// we get the max page-height from all pages in the subset and the sum of all page widths of the subset plus the offset between the pages
					finalBounds.Add(new PageRowBound(new Size(subset.Sum(f => f.Width), subset.Max(f => f.Height)), verticalBorderOffset, horizontalBorderOffset * (subset.Length - 1)));
					i += (pagesPerRow - 1);
				}
			}

			return finalBounds.ToArray();
		}

		internal int GetPagesPerRow()
		{
			return this.ViewType == ViewType.SinglePage ? 1 : 2;
		}

		public int GetCurrentPageNumber()
		{
			if (this.innerPanel == null)
				return -1;

			return this.innerPanel.GetCurrentPageIndex(this.ViewType) + 1;
		}

		public void ZoomToWidth()
		{
			this.innerPanel.ZoomToWidth();
			this.ZoomType = ZoomType.FitToWidth;
		}

		public void ZoomToHeight()
		{
			this.innerPanel.ZoomToHeight();
			this.ZoomType = ZoomType.FitToHeight;
		}

		public void ZoomIn()
		{
			this.innerPanel.ZoomIn();
			this.ZoomType = ZoomType.Fixed;
			if (ZoomChanged != null)
				ZoomChanged(this, EventArgs.Empty);
		}

		public void ZoomOut()
		{
			this.innerPanel.ZoomOut();
			this.ZoomType = ZoomType.Fixed;
			if (ZoomChanged != null)
				ZoomChanged(this, EventArgs.Empty);
		}

		public void Zoom(double zoomFactor)
		{
			this.innerPanel.Zoom(zoomFactor);
			this.ZoomType = ZoomType.Fixed;
		}

		private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (ScrollChanged != null)
				ScrollChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Sets the ZoomType back to Fixed
		/// </summary>
		public void SetFixedZoom()
		{
			this.ZoomType = ZoomType.Fixed;
		}

		public void GotoPreviousPage()
		{
			this.innerPanel.GotoPreviousPage();
		}

		public void GotoNextPage()
		{
			this.innerPanel.GotoNextPage();
		}

		public void GotoPage(int pageNumber)
		{
			this.innerPanel.GotoPage(pageNumber);
		}

		public void GotoFirstPage()
		{
			this.GotoPage(1);
		}

		public void GotoLastPage()
		{
			this.GotoPage(this.TotalPages);
		}

		public void RotateRight()
		{
			if (this.Rotation != ImageRotation.Rotate270)
				this.Rotation = (ImageRotation)this.Rotation + 1;
			else
				this.Rotation = ImageRotation.None;
		}

		public void RotateLeft()
		{
			if ((int)this.Rotation > 0)
				this.Rotation = (ImageRotation)this.Rotation - 1;
			else
				this.Rotation = ImageRotation.Rotate270;
		}

		public void Rotate(ImageRotation rotation)
		{
			var currentPage = this.innerPanel.GetCurrentPageIndex(this.ViewType) + 1;
			this.LoadPdf(this.CurrentSource, this.CurrentPassword);
			this.innerPanel.GotoPage(currentPage);
		}

		public void TogglePageDisplay()
		{
			this.PageRowDisplay = (this.PageRowDisplay == PageRowDisplayType.SinglePageRow) ? PageRowDisplayType.ContinuousPageRows : PageRowDisplayType.SinglePageRow;
		}

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);

			if (e.Property.Name.Equals("PageRowDisplay"))
				ChangeDisplayType((PageRowDisplayType)e.NewValue);
			else if (e.Property.Name.Equals("Rotation"))
				this.Rotate((ImageRotation)e.NewValue);
			else if (e.Property.Name.Equals("ViewType"))
				this.ApplyChangedViewType((ViewType)e.OldValue);
		}

		private void ApplyChangedViewType(ViewType oldViewType)
		{
			UpdateAndReload(() => { }, oldViewType);

			if (this.ViewTypeChanged != null)
				this.ViewTypeChanged(this, EventArgs.Empty);
		}

		private void ChangeDisplayType(PageRowDisplayType pageRowDisplayType)
		{
			UpdateAndReload(() =>
			{
				// we need to remove the current innerPanel
				this.pnlMain.Children.Clear();

				if (pageRowDisplayType == PageRowDisplayType.SinglePageRow)
					this.innerPanel = new SinglePagePdfViewer(this);
				else
					this.innerPanel = new ContinuousPdfViewer(this);

				this.pnlMain.Children.Add(this.innerPanel.Instance);
			}, this.ViewType);

			if (this.PageRowDisplayChanged != null)
				this.PageRowDisplayChanged(this, EventArgs.Empty);
		}

		private void UpdateAndReload(Action updateAction, ViewType viewType)
		{
			var currentPage = -1;
			var zoom = 1.0f;

			if (this.CurrentSource != null)
			{
				currentPage = this.innerPanel.GetCurrentPageIndex(viewType) + 1;
				zoom = this.innerPanel.CurrentZoom;
			}

			updateAction();

			if (currentPage > -1)
			{
				Action reloadAction = () =>
				{
					this.LoadPdf(this.CurrentSource, this.CurrentPassword);
					this.innerPanel.Zoom(zoom);
					this.innerPanel.GotoPage(currentPage);
				};

				if (this.innerPanel.Instance.IsLoaded)
					reloadAction();
				else
				{
					// we need to wait until the controls are loaded and then reload the pdf
					this.innerPanel.Instance.Loaded += (s, e) => { reloadAction(); };
				}
			}
		}

		/// <summary>
		/// Will only be triggered if the AllowDrop-Property is set to true
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrop(DragEventArgs e)
		{
			base.OnDrop(e);

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
				var filename = filenames.FirstOrDefault();

				if (filename != null && File.Exists(filename))
				{
					string pw = null;

					if (MuPdfWrapper.NeedsPassword(new FileSource(filename)))
					{
						if (this.PasswordRequired == null)
							return;

						var args = new PasswordRequiredEventArgs();
						this.PasswordRequired(this, args);

						if (args.Cancel)
							return;

						pw = args.Password;
					}

					try
					{
						this.OpenFile(filename, pw);
					}
					catch (Exception ex)
					{
						MessageBox.Show(string.Format("An error occured: " + ex.Message));
					}
				}
			}
		}
	}

	public class PasswordRequiredEventArgs : EventArgs
	{
		public string Password { get; set; }
		public bool Cancel { get; set; }
	}
}
