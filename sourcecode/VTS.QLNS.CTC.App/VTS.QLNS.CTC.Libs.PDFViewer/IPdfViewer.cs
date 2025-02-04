using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.Libs.PDFViewer.Enum;
using VTS.QLNS.CTC.Libs.PDFViewer.MuPdf;

namespace VTS.QLNS.CTC.Libs.PDFViewer
{
	/// <summary>
	/// Common interface for the two different display types, single pages (SinglePagePdfViewer) and continuous pages (ContinuousPdfViewer)
	/// </summary>
	internal interface IPdfViewer
	{
		ScrollViewer ScrollViewer { get; }
		UserControl Instance { get; }
		float CurrentZoom { get; }
		void Load(IPdfSource source, string password = null);
		void Unload();
		void Zoom(double zoomFactor);
		void ZoomIn();
		void ZoomOut();
		void ZoomToWidth();
		void ZoomToHeight();
		void GotoPage(int pageNumber);
		void GotoPreviousPage();
		void GotoNextPage();
		int GetCurrentPageIndex(ViewType viewType);
	}
}
